using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeShop.Application.Common.Interfaces
{
    public interface ISqlQueryService
    {
        Task<IEnumerable<T>> QuerySql<T>(string sql, object? parameters = null);
    }
}
