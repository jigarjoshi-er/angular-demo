using AngularDemo.DataContext;
using AngularDemo.Models;
using AngularDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AngularDemo.Controllers
{
    public class LeadsController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Save(object model)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> GetLookups()
        {
            try
            {
                using (var lookUpServices = new LookupService(ApplicationDbContext.Create()))
                {
                    return Ok(await lookUpServices.GetListByType(new List<string>
                    {
                        LookupType.City,
                        LookupType.State,
                        LookupType.Country,
                        LookupType.Source,
                        LookupType.EnquiryStatus
                    }));
                }
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
