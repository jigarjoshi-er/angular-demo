using AngularDemo.DataContext;
using AngularDemo.Repository.Base;
using AngularDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AngularDemo.Repository
{
    public class LookupRepository : BaseRepository
    {
        public LookupRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<DropDown>> GetListByType(List<string> lookUpType)
        {
            try
            {
                return await (from L in Context.Lookup
                              join LT in Context.LookupType on L.TypeId equals LT.Id
                              where !L.Deleted && lookUpType.Contains(LT.Name) /*&& lookUpType.Contains(L.Type.Value)*/
                              orderby LT.Name, L.Order.HasValue descending, L.Order, L.Name
                              select new DropDown
                              {
                                  Value = L.Id,
                                  Text = L.Name,
                                  Type = LT.Name
                              }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
                //LogServices.Insert(ex);
                //return new List<DropDown>();
            }
        }
    }
}
