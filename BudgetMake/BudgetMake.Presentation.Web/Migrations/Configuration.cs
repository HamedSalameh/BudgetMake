namespace BudgetMake.Presentation.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<BudgetMake.Presentation.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BudgetMake.Presentation.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(BudgetMake.Presentation.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            AddUserAndRole(context);
        }

        private bool AddUserAndRole(ApplicationDbContext context)
        {
            IdentityResult iResult;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            iResult = roleManager.Create(new IdentityRole("canCreate"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser()
            {
                UserName = "admin@local.com"
            };

            iResult = userManager.Create(user, "Password01");
            if (!iResult.Succeeded)
            {
                return iResult.Succeeded;
            }

            iResult = userManager.AddToRole(user.Id, "canCreate");
            return iResult.Succeeded;
        }
    }
}
