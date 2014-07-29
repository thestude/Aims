//using System.Configuration;
//using System.Data;
//using FluentNHibernate.Cfg.Db;

//namespace AIMS.Infrastructure.NHibernate
//{
//    public static class FluentDatabaseHelper
//    {
//        public static IPersistenceConfigurer DbConfiguration(DbType db, string connectionKey)
//        {

//            if (string.IsNullOrEmpty(db.ToString()))
//                throw new ConfigurationErrorsException("You must specify the Database type!");

//            try
//            {
//                switch (db)
//                {
//                    case DbType.MsSql2008:
//                        return MsSqlConfigureDatabase2008(connectionKey);
//                    case DbType.MySql:
//                        return MySqlConfigureDatabase(connectionKey);
//                    case DbType.PostgreSqlStandard:
//                        return PostgreSqlStandardConfigureDatabase(connectionKey);
//                    default:
//                        throw new ConfigurationErrorsException("The specified database is not supported");
//                }
//            }
//            catch
//            {
//                throw new ConfigurationErrorsException("The specified database is not supported");
//            }
//        }

//        private static IPersistenceConfigurer MsSqlConfigureDatabase2008(string connectionKey)
//        {
//            MsSqlConfiguration cfg = MsSqlConfiguration.MsSql2008
//                .IsolationLevel(IsolationLevel.ReadCommitted)
//                .ConnectionString(c => c.FromConnectionStringWithKey(connectionKey))
//                .AdoNetBatchSize(100)
//                .UseReflectionOptimizer();
//#if DEBUG
//            cfg.ShowSql();
//#endif

//            return cfg;
//        }

//        private static IPersistenceConfigurer MySqlConfigureDatabase(string connectionKey)
//        {
//            MySQLConfiguration cfg = MySQLConfiguration.Standard
//                .IsolationLevel(IsolationLevel.ReadCommitted)
//                .ConnectionString(c => c.FromConnectionStringWithKey(connectionKey))
//                .AdoNetBatchSize(100)
//                .UseReflectionOptimizer();
//#if DEBUG
//            cfg.ShowSql();
//#endif

//            return cfg;
//        }

//        //           

//        private static IPersistenceConfigurer PostgreSqlStandardConfigureDatabase(string connectionKey)
//        {
//            PostgreSQLConfiguration cfg = PostgreSQLConfiguration.Standard
//                .IsolationLevel(IsolationLevel.ReadCommitted)
//                .ConnectionString(c => c.FromConnectionStringWithKey(connectionKey))
//                .AdoNetBatchSize(100)
//                .UseReflectionOptimizer();
//#if DEBUG
//            cfg.ShowSql();
//#endif

//            return cfg;
//        }

//        public enum DbType
//        {
//            MsSql2000,
//            MsSql2005,
//            MsSql2008,
//            MySql,
//            Oracle9,
//            Oracle10,
//            PostgreSqlStandard,
//            PostgreSql81,
//            PostgreSql82,
//            SqlLite,
//            SqlLiteInMemory
//        }

//    }
//}