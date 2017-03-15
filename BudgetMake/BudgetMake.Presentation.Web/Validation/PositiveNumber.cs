using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.Validation
{
    /// <summary>
    /// Validation for numerical, non-null positive number
    /// </summary>
    public class PositiveNumber : ValidationAttribute
    {        
        public string errorMessageNegative { get; set; }
        public string errorMessageNan { get; set; }

        public PositiveNumber(string resourceNameNegative, string resourceNameNan, Type resourceType)
        {
            errorMessageNegative = GeneralServices.Helpers.ResourceHelper.GetResourceLookup(resourceType, resourceNameNegative);
            errorMessageNan = GeneralServices.Helpers.ResourceHelper.GetResourceLookup(resourceType, resourceNameNan);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = null;
            if (value != null)
            {
                double val = 0;
                bool isNumeric = true;
                isNumeric = double.TryParse(value.ToString(), out val);

                if (isNumeric)
                {
                    if (val < 0)
                    {
                        validationResult = new ValidationResult(errorMessageNegative);
                    }
                }
                else
                {
                    validationResult = new ValidationResult(errorMessageNan);
                }

            }

            return validationResult;
        }
    }
}