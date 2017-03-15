using System;


namespace BudgetMake.Presentation.Web
{
    public class Plugins
    {
        public static void InitHistoryService(string connectionString)
        {
            try
            {
                GeneralServices.Services.HistoryService.Instance.ConnectionString = connectionString;
                GeneralServices.Services.HistoryService.Instance.Initialize();
            }
            catch (Exception)
            {
                // don't crash the main application if this HistoryService failed to initiazlize
                //throw Ex;
            }
        }

        public static void InitDomainMapping(string connectionString, string domainModelAssemblyName)
        {
            try
            {
                GeneralServices.Services.EntityMapper.Instance.ConnectionString = connectionString;
                GeneralServices.Services.EntityMapper.Instance.CreateEntityMapping(domainModelAssemblyName);
            }
            catch (Exception)
            {
                // don't crash the main application if this EntityMapper failed to initiazlize
            }
        }

    }
}