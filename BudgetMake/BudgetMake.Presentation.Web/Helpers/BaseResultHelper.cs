using GeneralServices;
using System.Collections.Generic;
using System.Web.Mvc;
using static GeneralServices.Enums;

namespace BudgetMake.Presentation.Web.Helpers
{
    public static class BaseResultHelper
    {
        public static List<BaseResult> GetModelErrors(ModelStateDictionary ModelState)
        {
            List<BaseResult> results = new List<BaseResult>();
            var validationErrors = new List<ValidationResult>();
            foreach (var key in ModelState.Keys)
            {
                ModelState modelState = null;
                if (ModelState.TryGetValue(key, out modelState))
                {
                    foreach (var error in modelState.Errors)
                    {
                        validationErrors.Add(new ValidationResult()
                        {
                            PropertyName = key,
                            Message = error.ErrorMessage,
                            Status = ResultStatus.Failure
                        });
                    }
                }
            }

            results.AddRange(validationErrors);
            return results;
        }
    }
}