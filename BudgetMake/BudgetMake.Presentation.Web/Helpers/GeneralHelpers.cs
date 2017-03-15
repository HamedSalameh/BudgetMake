using System;
using System.Collections.Generic;
using System.Web;

namespace BudgetMake.Presentation.Web.Helpers
{
    public static class GeneralHelpers
    {
        /// <summary>
        /// Will attemps to look for domain model mapping the application cache, then translate from property ID to property Name
        /// If no mapping exists in the cache, will attempt to use DomainMapper
        /// As fallback, will return property ID as string
        /// </summary>
        /// <param name="PropertyID">A unique property ID to be looked up to property name</param>
        /// <returns></returns>
        public static string GetEntityPropertyNameByID(int PropertyID)
        {
            string name = "";

            Dictionary<int, string> mapping = HttpContext.Current.Cache["_domainMapping"] as Dictionary<int, string>;
            if(mapping != null)
            {
                mapping.TryGetValue(PropertyID, out name);

                if (string.IsNullOrEmpty(name))
                {
                    // Propety name could not be looked up using the cached-dictionary
                    try
                    {
                        name = GeneralServices.Services.EntityMapper.Instance.GetEntityPropertyNameByID(PropertyID);
                    }
                    catch (Exception)
                    {
                        // Don't crach the app in this case
                        // insteat, fallback to property id
                        name = PropertyID.ToString();
                    }
                }
            }

            return name;
        }
    }
}