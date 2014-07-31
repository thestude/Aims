using System;
using System.IO;
using System.Web;
using System.Data;
using AIMS.Models;
using AIMS.Search;
using NHibernate;
using Rhino.Security;
using NHibernate.Cfg;
using System.Web.Mvc;
using NHibernate.Event;
using NHibernate.Driver;
using System.Reflection;
using System.Web.Hosting;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Mapping.ByCode;
using NHibernate.Caches.SysCache;
using AIMS.Infrastructure.Logging;
using BrockAllen.MembershipReboot.Nh.Extensions;
using System.Runtime.Serialization.Formatters.Binary;
using Configuration = NHibernate.Cfg.Configuration;

namespace AIMS.Infrastructure.NHibernate
{
    public class ConfigurationBuilder
    {
        private readonly string _dbSchemaPath;
        private readonly string _serializedCfgPath;
        private readonly string _connectionString;
        private const string SchemaFileName = "Aims_schema.sql";
        private const string NhConfigFileName = "configuration.bin";

        public ConfigurationBuilder(string appDataPath = null, string connectionString = null)
        {
            if (string.IsNullOrWhiteSpace(appDataPath))
            {
                if (HttpContext.Current == null)
                    throw new Exception("Application Data folder could not be inferred");

                var appDataFolder = HostingEnvironment.MapPath(@"\App_Data\");
                var physicalAppPath = HttpContext.Current.Request.PhysicalApplicationPath;

                if (physicalAppPath == null || appDataFolder == null) return;

                appDataPath = Path.Combine(physicalAppPath, appDataFolder);
            }

            // Create if directory doesnt exist
            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            _connectionString = connectionString;
            _dbSchemaPath = Path.Combine(appDataPath, SchemaFileName);
            _serializedCfgPath = Path.Combine(appDataPath, NhConfigFileName);
        }

        public Configuration Build()
        {

            // Load configuration from file if already serialized
            Configuration cfg = LoadConfigFromFile();

            if (cfg != null)
            {
                return cfg;
            }

            cfg = new Configuration();
            var mapper = new ModelMapper();

            cfg.DataBaseIntegration(c =>
                                    {
                                        if (string.IsNullOrWhiteSpace(_connectionString))
                                            c.ConnectionStringName = "DefaultConnection";
                                        else
                                            c.ConnectionString = _connectionString;

                                        c.Driver<NpgsqlDriver>();
                                        c.Dialect<PostgreSQL82Dialect>();
                                        c.IsolationLevel = IsolationLevel.ReadCommitted; // RepeatableRead;
                                        c.BatchSize = 50;
                                        c.LogSqlInConsole = true;
                                        c.LogFormattedSql = true;
                                        c.AutoCommentSql = true;
                                        c.ConnectionReleaseMode = ConnectionReleaseMode.AfterTransaction; //default
                                    });
            cfg.Cache(x =>
                      {
                          x.UseQueryCache = true;
                          x.Provider<SysCacheProvider>();
                          x.DefaultExpiration = 60;
                          x.UseMinimalPuts = true;
                      }
                );
            cfg.ConfigureMembershipReboot( mapper);
            cfg.CurrentSessionContext<LazySessionContext>();

            // Add/compile all the mappings
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            
            SetLuceneEventListeners(cfg);
            BuildSchema(cfg);

            // Persist configuration expensive to rebuild every time
            SaveConfigToFile(cfg);

            return cfg;
        }

        private void SetLuceneEventListeners(Configuration config)
        {
            config.SetListener(ListenerType.PostUpdate, new LuceneIndexEventListener());
            config.SetListener(ListenerType.PostInsert, new LuceneIndexEventListener());
            config.SetListener(ListenerType.PostDelete, new LuceneIndexEventListener());

        }

        private void BuildSchema(Configuration config)
        {
            //Tell nhibernate about Rhino-security
            Security.Configure<AimsUser>(config, SecurityTableStructure.Prefix);

            //var se = new SchemaUpdate(config);
            //se.Execute(true, true);

            var sc = new SchemaExport(config);

            if (_dbSchemaPath != null)
            {
#if DEBUG
                //Only execute against database during debug
                sc.SetOutputFile(_dbSchemaPath).Execute(true, true, false);
#else
                sc.SetOutputFile(_dbSchemaPath).Execute(true, false, false);
#endif
            }
            else
            {
                throw new InvalidPathException("Could not locate App_Data folder");
            }
        }

        private Configuration LoadConfigFromFile()
        {
            if (!IsConfigValid())
                return null;
            try
            {
                using (var file = File.Open(_serializedCfgPath, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    return bf.Deserialize(file) as Configuration;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool IsConfigValid()
        {
            try
            {
                //Does the file exist?
                if (!File.Exists(_serializedCfgPath))
                    return false;

                //Get the file info
                var configFileInfo = new FileInfo(_serializedCfgPath);
                var execAssembly = Assembly.GetExecutingAssembly();
                var assemblyInfo = new FileInfo(execAssembly.Location);

                //If executing assembly is fresher than the config file rebuild
                if (assemblyInfo.LastWriteTime > configFileInfo.LastWriteTime)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                DependencyResolver.Current.GetService<ILogger>().Error(e);
                return false;
            }
        }

        private void SaveConfigToFile(Configuration cfg)
        {
            using (var file = File.Open(_serializedCfgPath, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(file, cfg);
            }
        }
    }
}