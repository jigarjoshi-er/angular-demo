using AngularDemo.DataContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularDemo.Repository.Base
{
    interface IBaseRepository : IDisposable
    {
        ApplicationDbContext Context { get; set; }
        string ConnectionString { get; }

        Task<IEnumerable<TResult>> QuerySP<TResult>(string sql, DynamicParameters parameters);
        Task<Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>>> QuerySP<TResult1, TResult2>(string sql, DynamicParameters parameters);
        Task<Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>, IEnumerable<TResult3>, IEnumerable<TResult4>, IEnumerable<TResult5>, IEnumerable<TResult6>, IEnumerable<TResult7>, Tuple<IEnumerable<TResult8>>>> QuerySP<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(string sql, DynamicParameters parameters);
    }
}
