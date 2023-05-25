using System.Data;
using System.Data.SQLite;

namespace LinkConverterApplication.Helpers.Absractions
{
    public interface IConnectionHelper
    {
        IDbConnection GetDatabaseConnection();
    }
}