namespace OhSnapDAL
{
    using System.Configuration;
    using System.Data.Common;

    class DBConnectionSettings
    {
        public const string connectionName = "OhSnapConnection";

        public static DbProviderFactory factory;

        public static string connectionString;

        public static void GetFactorySettingsFromConfig()
        {
            var connectionStringItem = ConfigurationManager.ConnectionStrings[connectionName];
            connectionString = connectionStringItem.ConnectionString;
            var providerName = connectionStringItem.ProviderName;
            factory = DbProviderFactories.GetFactory(providerName);
        }
    }
}
