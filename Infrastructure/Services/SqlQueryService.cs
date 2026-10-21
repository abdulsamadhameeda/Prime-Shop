using PrimeShop.Application.Common.Interfaces;

using System.Data;
using Dapper;


namespace PrimeShop.Infrastructure.Services
{
    public class SqlQueryService : ISqlQueryService
    {
        private readonly IDbConnection _connection;

        public SqlQueryService(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> QuerySql<T>(string sql, object? parameters = null)
        {
            return await _connection.QueryAsync<T>(sql, parameters);
        }
    }
}
