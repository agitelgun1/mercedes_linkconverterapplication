using System.Threading.Tasks;
using Dapper;
using LinkConverterApplication.Entities;
using LinkConverterApplication.Helpers.Absractions;

namespace LinkConverterApplication.Repositories
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly IConnectionHelper _connectionHelper;

        public ErrorRepository(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }
        
        public async Task<bool> InsertAsync(Error error)
        {
            const string sql = @"INSERT INTO errors
                                (message,stack_trace,project_name,ip_address, created_on)
                                VALUES (@Message, @StackTrace,@ProjectName,@IpAddress, @CreatedOn);";
            
            using var dbConnection = _connectionHelper.GetDatabaseConnection();
            dbConnection.Open();

            var result = await dbConnection
                .ExecuteAsync(sql, new
                {
                    Message = error.Message,
                    StackTrace = error.Stacktrace,
                    ProjectName = error.ProjectName,
                    IpAddress = error.IpAddress,
                    CreatedOn = error.CreatedOn
                });

            return result > 0;
        }

        public Task<bool> DisableDataAsync(Error entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Error> GetAsync(Error entity)
        {
            throw new System.NotImplementedException();
        }
    }
}