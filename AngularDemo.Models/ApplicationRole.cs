using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace AngularDemo.Models
{
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid();
        }
        public ApplicationRole(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
