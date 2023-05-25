using System;
using System.Data;
using System.Data.SQLite;
using LinkConverterApplication.Helpers.Absractions;
using Microsoft.Extensions.Configuration;

namespace LinkConverterApplication.Helpers.Helpers
{
    public class ConnectionHelper : IConnectionHelper
    {
        private readonly IConfiguration _configuration;

        public ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetDatabaseConnection()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var connectionString = _configuration["ConnectionString:Database"];
            var connection = new SQLiteConnection("Data Source=" + Environment.CurrentDirectory + connectionString);

            return connection;
        }
    }
}