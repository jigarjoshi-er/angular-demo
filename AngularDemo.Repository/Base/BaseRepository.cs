using AngularDemo.DataContext;
using AngularDemo.Models.Base;
using AngularDemo.Utility;
using AngularDemo.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AngularDemo.Repository.Base
{
    public class BaseRepository : IBaseRepository
    {
        #region Initialization

        public string ConnectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public ApplicationDbContext Context { get; set; }

        #endregion

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        #region EntityMethods

        internal protected async Task<DataTableResult<T>> GetDataTableResult<T>(string procedureName, DataTableSearch search, List<SqlParameter> filters)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection sql = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand(procedureName, sql)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(CreateSqlParameter("@Start", search.Start, SqlDbType.Int));
            cmd.Parameters.Add(CreateSqlParameter("@Length", search.Length == -1 ? int.MaxValue : search.Length, SqlDbType.Int));

            cmd.Parameters.AddRange(filters.ToArray());

            sql.Open();
            var reader = await cmd.ExecuteReaderAsync();

            //Getting records
            var result = ((IObjectContextAdapter)Context).ObjectContext.Translate<T>(reader).ToList();
            await reader.NextResultAsync();

            var count = ((IObjectContextAdapter)Context).ObjectContext.Translate<int>(reader).FirstOrDefault();

            sql.Close();

            return new DataTableResult<T>
            {
                Draw = search.Draw,
                RecordsFiltered = count,
                Data = result
            };
        }

        internal protected T GetSPResult<T>(string procedureName, List<SqlParameter> filters = null)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection sql = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand(procedureName, sql)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (filters != null)
            {
                foreach (var param in filters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            sql.Open();
            var reader = cmd.ExecuteReader();

            //Getting records
            var result = ((IObjectContextAdapter)Context).ObjectContext.Translate<T>(reader).FirstOrDefault();

            sql.Close();

            return result;
        }

        internal protected T GetFunctionResult<T>(string functionName, List<SqlParameter> filters = null)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection sql = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand($"SELECT dbo.{functionName}({(filters == null ? "" : string.Join(", ", filters.Select(x => x.ParameterName)))})", sql);

            if (filters != null)
            {
                foreach (var param in filters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            sql.Open();
            var reader = cmd.ExecuteReader();

            //Getting records
            var result = ((IObjectContextAdapter)Context).ObjectContext.Translate<T>(reader).FirstOrDefault();

            sql.Close();

            return result;
        }

        internal protected async Task<List<T>> GetSPResultList<T>(string procedureName, List<SqlParameter> filters)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection sql = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand(procedureName, sql)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (var param in filters)
            {
                cmd.Parameters.Add(param);
            }

            sql.Open();
            var reader = await cmd.ExecuteReaderAsync();

            //Getting records
            var result = ((IObjectContextAdapter)Context).ObjectContext.Translate<T>(reader).ToList();

            sql.Close();

            return result;
        }

        internal protected SqlParameter CreateSqlParameter(string name, object value, SqlDbType type)
        {
            SqlParameter sqlParameter = new SqlParameter(name, type);
            if (value == null)
            {
                sqlParameter.Value = DBNull.Value;
            }
            else
            {
                sqlParameter.Value = value;
            }
            return sqlParameter;
        }

        internal protected object Add(object entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        internal protected object Update(object entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        internal protected object Remove(object entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        internal protected T Save<T>(T entity, Guid userId) where T : BaseEntity
        {
            entity.CreatedBy = entity.UpdatedBy = userId;

            if (entity.Id == Guid.Empty)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                Context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
            }
            return entity;
        }

        internal protected T Save<T>(T entity, Guid userId, string[] excludeProperties) where T : BaseEntity
        {
            entity.CreatedBy = entity.UpdatedBy = userId;

            if (entity.Id == Guid.Empty)
            {
                Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                Context.Entry(entity).State = EntityState.Modified;
                Context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                Context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                foreach (var property in excludeProperties)
                {
                    try
                    {
                        Context.Entry(entity).Property(property).IsModified = false;
                    }
                    catch { }
                }
            }
            return entity;
        }

        internal protected ICollection<T> SaveList<T>(ICollection<T> entities, Guid userId) where T : BaseEntity
        {
            foreach (var entity in entities)
            {
                entity.CreatedBy = entity.UpdatedBy = userId;

                if (entity.Id == Guid.Empty)
                {
                    Context.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    Context.Entry(entity).State = EntityState.Modified;
                    Context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    Context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }
            }
            return entities;
        }

        //internal protected async Task<T> Get<T>(Guid Id, string[] includes = null) where T : BaseEntity
        //{
        //    var entity = Context.Set<T>().Where(x => x.Id == Id && !x.Deleted);
        //    if (includes != null)
        //    {
        //        foreach (var include in includes)
        //        {
        //            entity.Include(include);
        //        }
        //    }
        //    return await entity.FirstOrDefaultAsync();
        //}

        //internal protected async Task<List<T>> GetList<T>(string[] includes = null) where T : BaseEntity
        //{
        //    var entity = Context.Set<T>().Where(x => !x.Deleted);
        //    if (includes != null)
        //    {
        //        foreach (var include in includes)
        //        {
        //            entity.Include(include);
        //        }
        //    }
        //    return await entity.ToListAsync();
        //}

        //internal protected IQueryable<T> GetQueryable<T>(bool showDeleted) where T : BaseEntity
        //{
        //    var entity = Context.Set<T>().Where(x => !x.Deleted || showDeleted);
        //    return entity;
        //}

        //internal protected IQueryable<T> GetQueryable<T>(string[] includes = null) where T : BaseEntity
        //{
        //    var entity = Context.Set<T>().Where(x => !x.Deleted);
        //    if (includes != null)
        //    {
        //        foreach (var include in includes)
        //        {
        //            entity.Include(include);
        //        }
        //    }
        //    return entity;
        //}

        //internal protected IQueryable<T> GetQueryable<T>(bool showDeleted, string[] includes = null) where T : BaseEntity
        //{
        //    var entity = Context.Set<T>().Where(x => !x.Deleted || showDeleted);
        //    if (includes != null)
        //    {
        //        foreach (var include in includes)
        //        {
        //            entity.Include(include);
        //        }
        //    }
        //    return entity;
        //}

        //internal protected async Task Delete<T>(Guid Id, Guid userId) where T : BaseEntity
        //{
        //    var entity = await Context.Set<T>().Where(x => x.Id == Id).FirstOrDefaultAsync();

        //    if (entity == null)
        //    {
        //        throw new CustomException("Data cannot found.");
        //    }

        //    entity.Deleted = true;
        //    entity.UpdatedBy = userId;
        //    entity.UpdatedDate = DateTime.Now;
        //    Context.Entry(entity).State = EntityState.Modified;
        //}

        //internal protected void Delete<T>(List<T> entities, Guid userId) where T : BaseEntity
        //{
        //    foreach (var entity in entities)
        //    {
        //        entity.Deleted = true;
        //        entity.UpdatedBy = userId;
        //        //entity.UpdatedDate = DateTime.Now;
        //        Context.Entry(entity).State = EntityState.Modified;
        //    }
        //}

        internal protected async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }

        #endregion

        #region SQL Methods

        public async Task<IEnumerable<TResult>> QuerySP<TResult>(string sql, DynamicParameters parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    return await connection.QueryAsync<TResult>(sql, param: parameters, commandType: CommandType.StoredProcedure);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public async Task<Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>>> QuerySP<TResult1, TResult2>(string sql, DynamicParameters parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var multi = await connection.QueryMultipleAsync(sql, param: parameters, commandType: CommandType.StoredProcedure))
                {
                    var result1 = multi.Read<TResult1>().AsEnumerable();
                    var result2 = multi.Read<TResult2>().AsEnumerable();
                    return new Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>>(result1, result2);
                }
            }
        }

        public async Task<Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>, IEnumerable<TResult3>, IEnumerable<TResult4>, IEnumerable<TResult5>, IEnumerable<TResult6>, IEnumerable<TResult7>, Tuple<IEnumerable<TResult8>>>> QuerySP<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(string sql, DynamicParameters parameters)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (var multi = await connection.QueryMultipleAsync(sql, param: parameters, commandType: CommandType.StoredProcedure))
                    {
                        var result1 = multi.Read<TResult1>().AsEnumerable();
                        var result2 = multi.Read<TResult2>().AsEnumerable();
                        var result3 = multi.Read<TResult3>().AsEnumerable();
                        var result4 = multi.Read<TResult4>().AsEnumerable();
                        var result5 = multi.Read<TResult5>().AsEnumerable();
                        var result6 = multi.Read<TResult6>().AsEnumerable();
                        var result7 = multi.Read<TResult7>().AsEnumerable();
                        var result8 = multi.Read<TResult8>().AsEnumerable();
                        multi.Dispose();

                        return new Tuple<IEnumerable<TResult1>, IEnumerable<TResult2>, IEnumerable<TResult3>, IEnumerable<TResult4>, IEnumerable<TResult5>, IEnumerable<TResult6>, IEnumerable<TResult7>, Tuple<IEnumerable<TResult8>>>(result1, result2, result3, result4, result5, result6, result7, new Tuple<IEnumerable<TResult8>>(result8));
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        #endregion

        #region Helpers


        #endregion

        public void Dispose()
        {

        }
    }
}