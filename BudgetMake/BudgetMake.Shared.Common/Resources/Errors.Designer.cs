﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BudgetMake.Shared.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Errors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BudgetMake.Shared.Common.Resources.Errors", typeof(Errors).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Annual plan name is a required field and must not be empty.
        /// </summary>
        public static string AnnualPlan_InvalidName {
            get {
                return ResourceManager.GetString("AnnualPlan_InvalidName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only numbers or digits are allowed..
        /// </summary>
        public static string General_NaN {
            get {
                return ResourceManager.GetString("General_NaN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only positive numbers are allowed..
        /// </summary>
        public static string General_NegativeValue {
            get {
                return ResourceManager.GetString("General_NegativeValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to map view model to domain entity.
        /// </summary>
        public static string General_UnableToMapToModel {
            get {
                return ResourceManager.GetString("General_UnableToMapToModel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to map entity to it the requested view model.
        /// </summary>
        public static string General_UnableToMapToViewModel {
            get {
                return ResourceManager.GetString("General_UnableToMapToViewModel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bad request.
        /// </summary>
        public static string Http_400_BadRequest {
            get {
                return ResourceManager.GetString("Http_400_BadRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Resouce or page was not found..
        /// </summary>
        public static string Http_404_NotFound_404 {
            get {
                return ResourceManager.GetString("Http_404_NotFound_404", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Internal server error..
        /// </summary>
        public static string Http_500_InternalServerError {
            get {
                return ResourceManager.GetString("Http_500_InternalServerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Month plan name is a required field and must not be empty.
        /// </summary>
        public static string MonthlyPlan_InvalidName {
            get {
                return ResourceManager.GetString("MonthlyPlan_InvalidName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Monthly template plan name is a required field and must not be empty.
        /// </summary>
        public static string MonthlyPlanTemplate_InvalidName {
            get {
                return ResourceManager.GetString("MonthlyPlanTemplate_InvalidName", resourceCulture);
            }
        }
    }
}
