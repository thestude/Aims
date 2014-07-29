using System;
using System.IO;
using System.Web;
using System.Linq;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System.Reflection;
using Lucene.Net.Search;
using System.Web.Hosting;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using System.Configuration;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.NGram;
using System.Collections.Generic;
using Lucene.Net.Analysis.Shingle;
using Lucene.Net.Analysis.Standard;
using SpellChecker.Net.Search.Spell;
using Directory = System.IO.Directory;
using Version = Lucene.Net.Util.Version;

namespace AIMS.Search
{
    using Helpers;

    /// <summary />
    public class LuceneFullTextSearch
    {
        private const int PrefixLength = 3;
        private const float MinimumSimilarity = 0.8f;

        private const string EntityIdName = "ID";
        private const string SpellingName = "word";
        private static Lucene.Net.Store.Directory _fullSearchDirectory;
        private static Lucene.Net.Store.Directory _spellIndexDirectory;

        private static Lucene.Net.Store.Directory FullSearchDirectory
        {
            get
            {

                if (_fullSearchDirectory != null && IndexReader.IndexExists(_fullSearchDirectory))
                    return _fullSearchDirectory;

                var indexDirectory = ConfigurationManager.AppSettings["LuceneIndexDirectory"];
                _fullSearchDirectory = SetupDirectory(_fullSearchDirectory, indexDirectory);

                return _fullSearchDirectory;
            }
        }

        private static Lucene.Net.Store.Directory SpellingSearchDirectory
        {
            get
            {
                if (_spellIndexDirectory != null && IndexReader.IndexExists(_spellIndexDirectory))
                    return _spellIndexDirectory;

                var indexDirectory = ConfigurationManager.AppSettings["SpellingIndexDirectory"];
                _spellIndexDirectory = SetupDirectory(_spellIndexDirectory, indexDirectory);

                return _spellIndexDirectory;
            }
        }

        private static Lucene.Net.Store.Directory SetupDirectory(Lucene.Net.Store.Directory indexDir, string dirName)
        {
            string dirPath;
            Analyzer tempAnalyzer = new SimpleAnalyzer();

            //Not a web app
            if (HttpContext.Current == null)
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                dirPath = Path.Combine(baseDir, string.Format(@"App_Data\{0}", dirName));
            }
            else
            {
                var appDataDirectory = HostingEnvironment.MapPath(@"\App_Data\");
                var physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;

                // set in configuration settings.
                if (!String.IsNullOrWhiteSpace(physicalApplicationPath) &&
                    !String.IsNullOrWhiteSpace(dirName) && !String.IsNullOrWhiteSpace(appDataDirectory))
                {
                    var appdatafolder = Path.Combine(physicalApplicationPath, appDataDirectory);
                    dirPath = Path.Combine(appdatafolder, dirName);
                }
                else
                {
                    dirPath = HttpContext.Current.Server.MapPath(string.Format(@"~\App_Data\{0}", dirName));
                }
            }

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            indexDir = FSDirectory.Open(new DirectoryInfo(dirPath));

            if (!IndexReader.IndexExists(indexDir))
            {
                // Create index structure
                new IndexWriter(indexDir, tempAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED).Dispose();
            }

            if (!IndexWriter.IsLocked(indexDir)) return indexDir;
            IndexWriter.Unlock(indexDir);

            if (indexDir.FileExists("write.lock"))
            {
                // Unlock doesn't always remove this file
                indexDir.DeleteFile("write.lock");
            }

            return indexDir;
        }


        /// <summary>
        /// Collects the list of PropertyInfo objects decorated with the 
        /// FullTextIndexedAttribute class for the supplied object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static IEnumerable<PropertyOptions> GetIndexableProperties(object obj)
        {
            return GetIndexableProperties(obj.GetType());
        }

        /// <summary>
        /// Collects the list of PropertyInfo objects decorated with the
        /// FullTextIndexedAttribute class for the supplied type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<PropertyOptions> GetIndexableProperties(Type type)
        {
            var props =
                type.GetProperties()
                    .Where(x => x.GetCustomAttributes<FullTextIndexedAttribute>().Any())
                    .Select(x => new PropertyOptions
                                 {
                                     PropInfo = x,
                                     Analyzed = x.GetCustomAttributes<FullTextIndexedAttribute>().First().Analyze,
                                     Priority = x.GetCustomAttributes<FullTextIndexedAttribute>().First().IndexPriority,
                                     IndexForSpelling = x.GetCustomAttributes<FullTextIndexedAttribute>().First().IndexForSpelling

                                 })
                    .OrderBy(x => x.Priority);
                    //.Select(x => x.PropInfo);

            //Get specified properties from inherited base class
            var namedProps = type.GetCustomAttributes(typeof (FullTextIndexedAttribute))
                .Where(ca => ((FullTextIndexedAttribute) ca).NamedProperties.Any())
                .SelectMany(ca => ((FullTextIndexedAttribute) ca).NamedProperties
                    .Select(np => new PropertyOptions
                                  {
                                      Priority = 0, //no priority property on base class
                                      Analyzed = np.Analyze,
                                      IndexForSpelling = np.IndexForSpelling,
                                      PropInfo = type.GetProperty(np.PropertyName),
                                  }))
                .OrderBy(x => x.Priority);
                //.Select(x => x.PropInfo);

            return props.Concat(namedProps);
        }

        /// <summary>
        /// Determines if the type has Full-Text indexable properties.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsIndexable(Type type)
        {
            return type.GetProperties()
                .Select(x => x.GetCustomAttributes<FullTextIndexedAttribute>())
                .Any();
        }

        /// <summary>
        /// Determines if the object has Full-Text indexable properties.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsIndexable(object obj)
        {
            return IsIndexable(obj.GetType());
        }

        /// <summary>
        /// Returns a list of object IDs that match the search criteria.
        /// </summary>
        /// <param name="searchText">The search term </param>
        /// <param name="type">The entity type to be searched</param>
        /// <param name="maxRows">Number of top hits to be returned</param>
        /// <param name="symbol"> Wildcard or fuzzy character to be used for the query</param>
        /// <param name="fuzzyminsim">Minimal similarity for fuzzy queries</param>
        /// <returns>IEnumerable Guid</returns>
        public static IEnumerable<Guid> Search(string searchText, Type type, int maxRows, string symbol = "", float fuzzyminsim = MinimumSimilarity)
        {
            if (string.IsNullOrWhiteSpace(searchText)) return new Guid[] { };

            using (var searcher = new IndexSearcher(FullSearchDirectory, true))
            using (var analyzer = new CustomPerFieldAnalyzerWrapper(Version.LUCENE_30).GetWraper())
            {
                var query = new BooleanQuery();
                var searchfields = GetIndexableProperties(type).Select(x => x.PropInfo.Name.ToLowerInvariant()).ToArray();// get searcheable fields
                var parser = new MultiFieldQueryParser(Version.LUCENE_30, searchfields, analyzer)
                             {
                                 DefaultOperator = QueryParser.Operator.OR,
                                 FuzzyMinSim = fuzzyminsim
                             };

                // TODO: Do not split quoted text
                // Split the search string into separate search terms by word
                var terms = searchText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var term in terms)
                {
                    //// Incase term mispellt. add top 3 similarly spelt terms
                    var suggestions = SuggestSimilar(term, 3, 0.7f);
                    foreach (var seggestion in suggestions)
                    {
                        AddTermToQuery(query, parser, seggestion, symbol);
                    }

                    // Include original search term incase we are wrong on spelling
                    AddTermToQuery(query, parser, term, symbol);
                }

                var hits = searcher.Search(query, null, maxRows, Sort.RELEVANCE).ScoreDocs;
                var results = hits.Select(x => Guid.Parse(searcher.Doc(x.Doc).Get(EntityIdName)))
                    .Distinct()
                    .ToArray();

                return results;
            }
        }

        /// <summary>
        /// Returns a list of object IDs that match the search criteria.
        /// </summary>
        /// <param name="searchText">The search term </param>
        /// <param name="type">The entity type to be searched</param>
        /// <param name="maxRows">Number of top hits to be returned</param>
        /// <param name="fuzzyminsim">Minimal similarity for fuzzy queries</param>
        /// <returns>IEnumerable Guid</returns>
        public static IEnumerable<Guid> FuzzySearch(string searchText, Type type, int maxRows, float fuzzyminsim = MinimumSimilarity)
        {
            return Search(searchText, type, maxRows, "~", fuzzyminsim);
        }

        /// <summary>
        /// Returns a list of strings similar to the search term provided.
        /// </summary>
        /// <param name="searchTerm">The term to serch for similar terms</param>
        /// <param name="maxSugestions">maximum number os sugestions to return</param>
        /// <param name="accuracy">minimum score accuracy must be between 0 and 1 </param>
        /// <returns>IEnumerable String</returns>
        public static IEnumerable<string> SuggestSimilar(string searchTerm, int maxSugestions, float accuracy = 0.5f)
        {
            string[] suggestions;

            using (var searcher = new IndexSearcher(SpellingSearchDirectory, true))
            {
                var query = new BooleanQuery();
                var searchfields = new string[] { SpellingName };// Searcheable fields
                AddTermToFuzzyQuery(query, searchTerm, searchfields, accuracy);

                var hits = searcher.Search(query, null, maxSugestions, Sort.RELEVANCE).ScoreDocs;
                suggestions = hits.Select(x => searcher.Doc(x.Doc).Get(SpellingName))
                    .Distinct()
                    .ToArray();
            }

            return suggestions;
        }

        /// <summary>
        /// Returns a list of strings similar to the search term provided.
        /// </summary>
        /// <param name="searchTerm">The term to serch for similar terms</param>
        /// <param name="maxSugestions">maximum number os sugestions to return</param>
        /// <param name="accuracy">minimum score accuracy must be between 0 and 1 </param>
        /// <returns>IEnumerable String</returns>
        public static IEnumerable<string> SuggestSimilarOrig(string searchTerm, int maxSugestions, float accuracy = 0.5f)
        {
            string[] suggestions;

            // Strip any wildcard charaters
            searchTerm = searchTerm.Trim().Replace("~", "").Replace("*", "");
            using (var spellchecker = new SpellChecker.Net.Search.Spell.SpellChecker(SpellingSearchDirectory))
            {
                spellchecker.SetAccuracy(accuracy);
                spellchecker.setStringDistance(new NGramDistance());//JaroWinklerDistance() NGramDistance() LevenshteinDistance()

                suggestions = spellchecker.SuggestSimilar(searchTerm, maxSugestions);
            }
            return suggestions;
        }

        /// <summary>
        /// Removes all indices from the Full Text index; used for regular maintenance.
        /// </summary>
        public static void ClearIndices()
        {
            //using (var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            using (var analyzer = new CustomPerFieldAnalyzerWrapper(Version.LUCENE_30).GetWraper())
            using (var speller = new SpellChecker.Net.Search.Spell.SpellChecker(SpellingSearchDirectory))
            using (var writer = new IndexWriter(FullSearchDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                speller.ClearIndex(); // Remove all spelling indices
                writer.DeleteAll(); // Remove all lucene indices
                writer.Optimize();
                analyzer.Close();
            }
        }

        /// <summary>
        /// Refreshes the index of the supplied entity, assuming that the entity is indexable.
        /// </summary>
        /// <param name="entity"></param>
        public static void UpdateIndexOnEntity(object entity)
        {
            var indexables = GetIndexableProperties(entity).ToList();
            if (!indexables.Any()) return;

            var term = new Term(EntityIdName);
            var entityType = entity.GetType();
            var entityIdValue = entityType.GetProperty(EntityIdName).GetValue(entity).ToString();

            using (var analyzer = new CustomPerFieldAnalyzerWrapper(Version.LUCENE_30).GetWraper())
            using (var writer = new IndexWriter(FullSearchDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var doc = CreateDocument(entity, EntityIdName, entityIdValue, indexables);
                writer.UpdateDocument(term, doc);

                // Must be after writer has docs
                IndexSpellingDocument(indexables, entity);

                writer.Optimize();
                analyzer.Close();
            }
        }

        /// <summary>
        /// Removes the index of the supplied entity.
        /// </summary>
        /// <param name="entity"></param>
        public static void DeleteIndexForEntity(object entity)
        {
            var indexables = GetIndexableProperties(entity);
            if (!indexables.Any()) return;

            var entityType = entity.GetType();
            var entityIdValue = entityType.GetProperty(EntityIdName).GetValue(entity).ToString();

            using (var analyzer = new CustomPerFieldAnalyzerWrapper(Version.LUCENE_30).GetWraper())
            using (var writer = new IndexWriter(FullSearchDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var searchQuery = new TermQuery(new Term(EntityIdName, entityIdValue));

                writer.DeleteDocuments(searchQuery);
                writer.Optimize();
                analyzer.Close();
            }
        }

        /// <summary>
        /// Creates a new index for the supplied entity.
        /// </summary>
        /// <param name="entity"></param>
        public static void InsertIndexOnEntity(object entity)
        {
            var indexables = GetIndexableProperties(entity).ToList();
            if (!indexables.Any()) return;

            var entityType = entity.GetType();
            var entityIdValue = entityType.GetProperty(EntityIdName).GetValue(entity).ToString();

            using (var analyzer = new CustomPerFieldAnalyzerWrapper(Version.LUCENE_30).GetWraper())
            using (var writer = new IndexWriter(FullSearchDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var searchQuery = new TermQuery(new Term(EntityIdName, entityIdValue));

                // Remove older index entry if one exists
                writer.DeleteDocuments(searchQuery);

                // Add new index entry
                var doc = CreateDocument(entity, EntityIdName, entityIdValue, indexables);
                writer.AddDocument(doc);

                // Must be after writer has docs
                IndexSpellingDocument(indexables, entity);

                writer.Optimize();
                analyzer.Close();
            }
        }

        /// <summary>
        /// Creates new indices for the collection of indexable objects supplied
        /// </summary>
        /// <typeparam name="T">The type of the entities to index</typeparam>
        /// <param name="entities">A collection indexable entities</param>
        public static void PopulateIndex<T>(IEnumerable<T> entities) where T : class
        {
            var entityType = typeof(T);
            if (!IsIndexable(entityType)) return;

            var indexables = GetIndexableProperties(entityType).ToList();

            using (var analyzer = new CustomPerFieldAnalyzerWrapper(Version.LUCENE_30).GetWraper())
            using (var writer = new IndexWriter(FullSearchDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var entity in entities)
                {
                    var entityIdValue = entityType.GetProperty(EntityIdName).GetValue(entity).ToStringOrEmpty();

                    // Remove older index entry
                    var searchQuery = new TermQuery(new Term(EntityIdName, entityIdValue));
                    writer.DeleteDocuments(searchQuery);

                    // Add new index entry
                    var doc = CreateDocument(entity, EntityIdName, entityIdValue, indexables);
                    writer.AddDocument(doc);

                    // Must be after writer has docs
                    IndexSpellingDocument(indexables, entity);
                }

                writer.Optimize();
                analyzer.Close();
            }
        }

        /// <summary>
        /// Creates a document with the indexable properties of the suplied entity.
        /// </summary>
        /// <param name="entity">The entity with indexable properties</param>
        /// <param name="entityIdName">The name of the unique identifier property e.g. ID</param>
        /// <param name="entityIdValue">The value of the unique identifier property</param>
        /// <param name="indexables">collection of indexable properties on the entity</param>
        private static Document CreateDocument(object entity, string entityIdName, string entityIdValue, IEnumerable<PropertyOptions> indexables)
        {
            var doc = new Document();
            doc.Add(new Field(entityIdName, entityIdValue, Field.Store.YES, Field.Index.NOT_ANALYZED));
            foreach (var propertyOpts in indexables)
            {
                var propertyName = propertyOpts.PropInfo.Name.ToLowerInvariant();
                var propertyValue = propertyOpts.PropInfo.GetValue(entity).ToStringOrEmpty();
                var analyzed = propertyOpts.Analyzed ? Field.Index.ANALYZED : Field.Index.NOT_ANALYZED;
                doc.Add(new Field(propertyName, propertyValue, Field.Store.YES, analyzed));// field specific index
            }
            return doc;
        }

        /// <summary>
        /// Indexes the properties for use in spell cheking.
        /// </summary>
        /// <param name="indexables">collection of indexable properties on the entity</param>
        /// <param name="entity">The entity containing the properties to be indexed</param>
        private static void IndexSpellingDocument(IEnumerable<PropertyOptions> indexables, object entity)
        {
            if (!indexables.Any()) return;
            
            using (var analyzer = new NGramWordAnalyzer(Version.LUCENE_30))
            using (var writer = new IndexWriter(SpellingSearchDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {

                foreach (var propertyOpts in indexables)
                {
                    if (propertyOpts.IndexForSpelling)
                    {
                        var doc = new Document();
                        var propertyValue = propertyOpts.PropInfo.GetValue(entity).ToStringOrEmpty();
                        doc.Add(new Field(SpellingName, propertyValue, Field.Store.YES, Field.Index.ANALYZED));// field specific index
                        writer.AddDocument(doc);
                    }
                }

                writer.Optimize();
                analyzer.Close();
            }
        }

        /// <summary>
        /// Indexes the properties for use in spell cheking.
        /// </summary>
        /// <param name="indexables">collection of indexable properties on the entity</param>
        /// <param name="doc">The document containing the properties to be indexed</param>
        private static void IndexSpellingDocumentOrig(IEnumerable<PropertyOptions> indexables, Document doc)
        {
            var directory = new RAMDirectory();
            using (var analyzer = new StandardAnalyzer(Version.LUCENE_30))
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            using (var speller = new SpellChecker.Net.Search.Spell.SpellChecker(SpellingSearchDirectory))
            {
                writer.AddDocument(doc);
                var reader = writer.GetReader();
                foreach (var propertyOpts in indexables)
                {
                    if (propertyOpts.IndexForSpelling)
                    {
                        speller.IndexDictionary(new LuceneDictionary(reader, propertyOpts.PropInfo.Name.ToLowerInvariant()));
                    }
                }
            }
        }

        /// <summary>
        /// Parses and adds the search term to the query.
        /// </summary>
        /// <param name="query">The query to add the search term to</param>
        /// <param name="segment">The search term</param>
        /// <param name="searchfields">The fields to be searched</param>
        /// <param name="acuracy"></param>
        private static void AddTermToFuzzyQuery(BooleanQuery query, string segment, IEnumerable<string> searchfields, float acuracy = MinimumSimilarity)
        {
            // Strip any wildcard charaters
            segment = segment.Trim().Replace("~", "").Replace("*", "");
            foreach (var field in searchfields)
            {
                try
                {
                    var term = new Term(field, segment);
                    var fuzzyQuery = new FuzzyQuery(term, acuracy, PrefixLength);
                    query.Add(fuzzyQuery, Occur.SHOULD);
                }
                catch (ParseException)
                {
                    // if a ParseException is thrown, it's likely due to extraneous odd symbols
                    // in the search text, so this will escape them.
                    var pSegment = QueryParser.Escape(segment);

                    var term = new Term(field, pSegment);
                    var fuzzyQuery = new FuzzyQuery(term, acuracy, PrefixLength);
                    query.Add(fuzzyQuery, Occur.SHOULD);
                }
            }

        }

        /// <summary>
        /// Parses and adds the search term to the query.
        /// </summary>
        /// <param name="query">The query to add the search term to</param>
        /// <param name="parser">The query parser used to parse the searchg term</param>
        /// <param name="segment">The search term</param>
        /// <param name="symbol">Wildcard or fuzzy character to be used for the query</param>
        private static void AddTermToQuery(BooleanQuery query, QueryParser parser, string segment, string symbol)
        {
            // Strip any wildcard charaters
            segment = segment.Trim().Replace("~", "").Replace("*", "");

            try
            {
                query.Add(parser.Parse(segment + symbol), Occur.SHOULD);
            }
            catch (ParseException)
            {
                // if a ParseException is thrown, it's likely due to extraneous odd symbols
                // in the search text, so this will escape them.
                var pSegment = QueryParser.Escape(segment);
                query.Add(parser.Parse(pSegment + symbol), Occur.SHOULD);
            }
        }
    }

    /// <summary>
    /// Use for analyzing phrases. Tokenizes the input into n-grams of the given sizes.
    ///     Example: for (minGram=2, maxGram=2) the phrase "the brown fox" becomes "'the brown' 'brown fox'"
    /// </summary>
    public class NGramPhraseAnalyzer : Analyzer
    {
        private readonly Version _version;
        private readonly int _minGram;
        private readonly int _maxGram;
        public NGramPhraseAnalyzer(Version version, int minGram = 2, int maxGram = 8)
        {
            _version = version;
            _minGram = minGram;
            _maxGram = maxGram;
        }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {

            // Splits words at punctuation characters, removing punctuation.
            // Splits words at hyphens, unless there's a number in the token...
            // Recognizes email addresses and internet hostnames as one token.
            var intput = new StandardTokenizer(_version, reader);

            // A ShingleMatrixFilter constructs shingles from a token stream.
            // "2010 Audi RS5 Quattro Coupe" => "2010 Audi", "Audi RS5", "RS5 Quattro", "Quattro Coupe"
            var shingleMatrixOutput = new ShingleMatrixFilter(
                // stream from which to construct the matrix
                                                intput,
                // minimum number of tokens in any shingle
                                                _minGram,
                // maximum number of tokens in any shingle.
                                                _maxGram,
                // character to use between texts of the token parts in a shingle.
                                                ' ');

            // Normalizes token text to lower case.
            var lowerCaseFilter = new LowerCaseFilter(shingleMatrixOutput);

            // Removes stop words from a token stream.
            return new StopFilter(true, lowerCaseFilter, StopAnalyzer.ENGLISH_STOP_WORDS_SET);
        }
    }

    /// <summary>
    /// Use for analyzing single words. Tokenizes the input into n-grams of the given sizes.
    ///     Example: for (minGram=2, maxGram=2) the word "camp" becomes "ca am mp"
    /// </summary>
    public class NGramWordAnalyzer : Analyzer
    {
        private readonly Version _version;
        private readonly int _minGram;
        private readonly int _maxGram;

        /// <summary>
        /// Initializes a new instance of the <see cref="NGramWordAnalyzer"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="minGram">The minimum gram.</param>
        /// <param name="maxGram">The maximum gram.</param>
        public NGramWordAnalyzer(Version version, int minGram = 2, int maxGram = 8)
        {
            _version = version;
            _minGram = minGram;
            _maxGram = maxGram;
        }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            // Splits words at punctuation characters, removing punctuation.
            // Splits words at hyphens, unless there's a number in the token...
            // Recognizes email addresses and internet hostnames as one token.
            var tokenizer = new StandardTokenizer(_version, reader);

            TokenStream filter = new StandardFilter(tokenizer);

            // Normalizes token text to lower case.
            filter = new LowerCaseFilter(filter);

            // Removes stop words from a token stream.
            filter = new StopFilter(true, filter, StopAnalyzer.ENGLISH_STOP_WORDS_SET);

            return new NGramTokenFilter(filter, _minGram, _maxGram);

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomPerFieldAnalyzerWrapper
    {
        private readonly Version _version;
        readonly PerFieldAnalyzerWrapper _customWrapper;

        // TODO: Inject analyzers depending on type of field being analyzed
        public CustomPerFieldAnalyzerWrapper(Version version)
        {
            _version = version;
            _customWrapper = new PerFieldAnalyzerWrapper(new NGramWordAnalyzer(_version));
        }

        public PerFieldAnalyzerWrapper GetWraper()
        {
            _customWrapper.AddAnalyzer("mobilephonenumber", new StandardAnalyzer(_version));
            _customWrapper.AddAnalyzer("email", new StandardAnalyzer(_version));
            return _customWrapper;
        }
    }

    public struct PropertyOptions
    {
        public PropertyInfo PropInfo;
        public bool Analyzed;
        public bool IndexForSpelling;
        public int Priority;
    }

    public struct IndexProperties
    {
        public string PropertyName;
        public bool Analyze;
        public bool IndexForSpelling;

    }
}