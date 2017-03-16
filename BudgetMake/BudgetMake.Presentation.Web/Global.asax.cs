using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BudgetMake.Presentation.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Logging
            log4net.Config.XmlConfigurator.Configure();
            // Plugins
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BudgetMakeDB"].ConnectionString;
            Plugins.InitDomainMapping(connectionString, "BudgetMake.Shared.DomainModel");
            Plugins.InitHistoryService(connectionString);

            // Get the domain model mapping
            var domainMapping = GeneralServices.Services.EntityMapper.Instance.LoadDomainModelEntityMapping();
            // Get application-context cache
            System.Web.Caching.Cache _domainMappinerCache = Context.Cache;
            // Add domain model mapping to application cache
            _domainMappinerCache.Insert("_domainMapping", domainMapping);
        }
    }
}
