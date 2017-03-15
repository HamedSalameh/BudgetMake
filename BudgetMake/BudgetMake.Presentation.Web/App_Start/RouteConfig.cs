using System.Web.Mvc;
using System.Web.Routing;

namespace BudgetMake.Presentation.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Fallback",
                url: "",
                defaults: new { controller = "AnnualBudget", action = "GetAnnualPlans" }
                );

            routes.MapRoute(
                name: "MonthlyPlansPerAnnual",
                url: "Annual/{AnnualPlanID}",
                defaults: new { controller = "MonthlyPlans", action = "GetMonthlyPlans" }
            );

            routes.MapRoute(
                name: "BudgetPerMonth",
                url: "Monthly/{MonthlyPlanId}",
                defaults: new { controller = "BudgetItem", action = "GetBudgetItems" }
            );

            routes.MapRoute(
                name: "AnnualBudgetsFallback",
                url: "AnnualPlans",
                defaults: new { controller = "AnnualBudget", action = "GetAnnualPlans" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
