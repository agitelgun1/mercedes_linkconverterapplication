using System;
using System.Threading.Tasks;
using Dapper;
using LinkConverterApplication.Entities;
using LinkConverterApplication.Helpers.Absractions;

namespace LinkConverterApplication.Repositories
{
    public class WebUrlRepository : IWebUrlRepository
    {
        private readonly IConnectionHelper _connectionHelper;

        public WebUrlRepository(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<bool> InsertAsync(WebUrl webUrl)
        {
            const string sql = @"INSERT INTO web_url_requests
                                (link_id,is_active,request,response,created_on)
                                VALUES (@LinkId,@IsActive, @Request, @Response, @CreatedOn);";

            using var dbConnection = _connectionHelper.GetDatabaseConnection();
            dbConnection.Open();

            var result = await dbConnection
                .ExecuteAsync(sql, new
                {
                    Request = webUrl.Request,
                    Response = webUrl.Response,
                    CreatedOn = DateTime.Now,
                    LinkId = webUrl.LinkId,
                    IsActive = webUrl.IsActive
                });

            return result > 0;
        }

        public async Task<bool> DisableDataAsync(WebUrl webUrl)
        {
            const string sql = @"update web_url_requests
                                    set is_active = false
                                    where request = @Request";

            using var dbConnection = _connectionHelper.GetDatabaseConnection();
            dbConnection.Open();

            var result = await dbConnection
                .ExecuteAsync(sql, new
                {
                    Request = webUrl.Request
                });

            return result > 0;
        }

        public async Task<WebUrl> GetAsync(WebUrl webUrl)
        {
            const string sql = @"select request from web_url_requests
                                    where response = @DeepLinkUrl and is_active = true
                                    order by created_on desc
                                    ";

            using var dbConnection = _connectionHelper.GetDatabaseConnection();
            dbConnection.Open();

            var result = await dbConnection
                .QueryFirstOrDefaultAsync<WebUrl>(sql, new
                {
                    DeepLinkUrl = webUrl.Response
                });

            return result;
        }
    }
}