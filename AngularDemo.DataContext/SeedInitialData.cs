using System.Data.Entity;
using Microsoft.AspNet.Identity;
using AngularDemo.Models;
using System.IO;
using System.Web.Hosting;

namespace AngularDemo.DataContext
{
    public class SeedInitialData : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new ApplicationUserStore(context));

            var user = new ApplicationUser
            {
                FirstName = "Admin",
                Email = "admin@test.com",
                EmailConfirmed = true,
                UserName = "Admin"
            };

            userManager.Create(user, "Admin@123");

            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

            roleManager.Create(new ApplicationRole { Name = "Admin" });
            roleManager.Create(new ApplicationRole { Name = "Sales Person" });
            roleManager.Create(new ApplicationRole { Name = "Office Staff" });

            userManager.AddToRole(user.Id, "Admin");

            string query = File.ReadAllText(HostingEnvironment.MapPath("~/App_Data/Initial-Data.sql"));
            context.Database.ExecuteSqlCommand(query);

            base.Seed(context);
        }
    }
}