using AngularDemo.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace AngularDemo.DataContext
{
    class ApplicationRoleStore : RoleStore<ApplicationRole, Guid, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }
}