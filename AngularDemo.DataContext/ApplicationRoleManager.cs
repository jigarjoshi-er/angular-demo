using AngularDemo.Models;
using Microsoft.AspNet.Identity;
using System;

namespace AngularDemo.DataContext
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, Guid>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, Guid> store) : base(store)
        {
        }
    }
}
