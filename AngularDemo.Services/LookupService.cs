using AngularDemo.DataContext;
using AngularDemo.Repository;
using AngularDemo.Services.Base;
using AngularDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularDemo.Services
{
    public class LookupService : BaseService
    {
        public LookupService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<DropDown>> GetListByType(List<string> lookupTypes)
        {
            try
            {
                using (var lookupRepository = new LookupRepository(ApplicationDbContext.Create()))
                {
                    return await lookupRepository.GetListByType(lookupTypes);
                }
            }
            catch (Exception ex)
            {
                //LogServices.Insert(ex);
                return new List<DropDown>();
            }
        }
    }
}
