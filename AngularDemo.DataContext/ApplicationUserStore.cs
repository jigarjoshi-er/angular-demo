using AngularDemo.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace AngularDemo.DataContext
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {
        }
    }
}
