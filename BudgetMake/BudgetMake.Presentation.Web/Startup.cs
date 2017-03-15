using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetMake.Presentation.Web.Startup))]
namespace BudgetMake.Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
