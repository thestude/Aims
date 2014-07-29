using NHibernate.Cfg;

namespace AIMS.Infrastructure.NHibernate
{
    public class FluentConfigurator
    {
        private static Configuration _nhConfig;

        public Configuration Configuration
        {
            get
            {
                return _nhConfig;
            }
            
        }

        public FluentConfigurator BuildNhConfiguration(string appDataPath = null, string connectionString = null)
        {
            var configBuilder = new ConfigurationBuilder(appDataPath, connectionString);
            _nhConfig = configBuilder.Build();

            return this;
        }
    }
}