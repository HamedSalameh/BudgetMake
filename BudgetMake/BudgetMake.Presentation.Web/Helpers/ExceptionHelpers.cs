using BudgetMake.Shared.Contracts.Infra;
using GeneralServices.Helpers;
using System;

namespace BudgetMake.Presentation.Web.Helpers
{
    public static class ExceptionHelpers
    {
        public static void handleException(Exception Ex, ILocalLogger logger)
        {
            logger.ErrorFormat("Exception at {0} : {1}\r\n{2}", Reflection.GetCallingMethodName(), Ex.Message, Ex.InnerException != null ? Ex.InnerException?.Message : "");
        }
    }
}