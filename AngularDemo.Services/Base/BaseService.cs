using AngularDemo.DataContext;
using AngularDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AngularDemo.Services.Base
{
    public class BaseService : IDisposable
    {
        protected readonly ApplicationDbContext context;

        public BaseService(ApplicationDbContext context)
        {
            this.context = context;
        }

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
            var result = ((IObjectContextAdapter)context).ObjectContext.Translate<T>(reader).ToList();
            await reader.NextResultAsync();

            var count = ((IObjectContextAdapter)context).ObjectContext.Translate<int>(reader).FirstOrDefault();

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
            var result = ((IObjectContextAdapter)context).ObjectContext.Translate<T>(reader).FirstOrDefault();

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
            var result = ((IObjectContextAdapter)context).ObjectContext.Translate<T>(reader).FirstOrDefault();

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
            var result = ((IObjectContextAdapter)context).ObjectContext.Translate<T>(reader).ToList();

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

        //internal protected T Save<T>(T entity, Guid userId) where T : BaseEntity
        //{
        //    entity.CreatedBy = entity.UpdatedBy = userId;

        //    if (entity.Id == Guid.Empty)
        //    {
        //        context.Entry(entity).State = EntityState.Added;
        //    }
        //    else
        //    {
        //        context.Entry(entity).State = EntityState.Modified;
        //        context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
        //        context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
        //    }
        //    return entity;
        //}

        //internal protected T Save<T>(T entity, Guid userId, string[] excludeProperties) where T : BaseEntity
        //{
        //    entity.CreatedBy = entity.UpdatedBy = userId;

        //    if (entity.Id == Guid.Empty)
        //    {
        //        context.Entry(entity).State = EntityState.Added;
        //    }
        //    else
        //    {
        //        context.Entry(entity).State = EntityState.Modified;
        //        context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
        //        context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
        //        foreach (var property in excludeProperties)
        //        {
        //            try
        //            {
        //                context.Entry(entity).Property(property).IsModified = false;
        //            }
        //            catch { }
        //        }
        //    }
        //    return entity;
        //}

        //internal protected ICollection<T> SaveList<T>(ICollection<T> entities, Guid userId) where T : BaseEntity
        //{
        //    foreach (var entity in entities)
        //    {
        //        entity.CreatedBy = entity.UpdatedBy = userId;

        //        if (entity.Id == Guid.Empty)
        //        {
        //            context.Entry(entity).State = EntityState.Added;
        //        }
        //        else
        //        {
        //            context.Entry(entity).State = EntityState.Modified;
        //            context.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
        //            context.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
        //        }
        //    }
        //    return entities;
        //}

        //internal protected async Task<T> Get<T>(Guid Id, string[] includes = null) where T : BaseEntity
        //{
        //    var entity = context.Set<T>().Where(x => x.Id == Id && !x.Deleted);
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
        //    var entity = context.Set<T>().Where(x => !x.Deleted);
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
        //    var entity = context.Set<T>().Where(x => !x.Deleted || showDeleted);
        //    return entity;
        //}

        //internal protected IQueryable<T> GetQueryable<T>(string[] includes = null) where T : BaseEntity
        //{
        //    var entity = context.Set<T>().Where(x => !x.Deleted);
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
        //    var entity = context.Set<T>().Where(x => !x.Deleted || showDeleted);
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
        //    var entity = await context.Set<T>().Where(x => x.Id == Id).FirstOrDefaultAsync();

        //    if (entity == null)
        //    {
        //        throw new CustomException("Data cannot found.");
        //    }

        //    entity.Deleted = true;
        //    entity.UpdatedBy = userId;
        //    entity.UpdatedDate = DateTime.Now;
        //    context.Entry(entity).State = EntityState.Modified;
        //}

        //internal protected void Delete<T>(List<T> entities, Guid userId) where T : BaseEntity
        //{
        //    foreach (var entity in entities)
        //    {
        //        entity.Deleted = true;
        //        entity.UpdatedBy = userId;
        //        //entity.UpdatedDate = DateTime.Now;
        //        context.Entry(entity).State = EntityState.Modified;
        //    }
        //}

        internal protected async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
