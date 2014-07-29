using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace AIMS.Search
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class FullTextIndexedAttribute : Attribute
    {
        private readonly List<IndexProperties> _namedProperties;

        public int IndexPriority;
        /// <summary>
        /// Bool. If set to true, indicates the field should be analysed i.e use Field.Index.ANALYZED
        /// </summary>
        public bool Analyze;

        /// <summary>
        /// Bool. If set to true, indicates the field should be indexed by the SpellChecker. For use with spelling surgestions.
        /// </summary>
        public bool IndexForSpelling;

        public IEnumerable<IndexProperties> NamedProperties
        {
            get
            {
                return _namedProperties;
            }
        }

        public FullTextIndexedAttribute(string propertyNames = "")
        {
            // Default values
            Analyze = true;
            IndexPriority = 0;
            IndexForSpelling = true;

            _namedProperties = new List<IndexProperties>();

            var props = propertyNames.Trim().Split(',');
            foreach (var prop in props)
            {
                if (prop.Trim() == string.Empty)
                    continue;
                var indexprops = prop.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                var propertyName = indexprops[0];

                if (indexprops.Length > 1)
                {
                    Analyze = !indexprops.Contains("NoAnalyze", StringComparer.InvariantCultureIgnoreCase); //if NoAnalyze specified set Analyze to false
                    IndexForSpelling = !indexprops.Contains("NoSpelling", StringComparer.InvariantCultureIgnoreCase); //if NoSpelling specified set IndexForSpelling to false
                }

                _namedProperties.Add(new IndexProperties { PropertyName = propertyName, Analyze = Analyze, IndexForSpelling = IndexForSpelling });
            }
        }

    }
}