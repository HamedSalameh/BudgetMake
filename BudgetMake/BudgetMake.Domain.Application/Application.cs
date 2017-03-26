using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using GeneralServices;
using GeneralServices.Helpers;
using GeneralServices.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Script.Serialization;
using static GeneralServices.Enums;

namespace BudgetMake.Domain.Application
{
    public class Application : IApplication
    {
        // Business layers
        private Hashtable businessLayers;
        private readonly IAnnualBudgetBL annualBudgetBL;
        private readonly IMonthlyBudgetBL monthlyBudgetBL;
        private readonly IExpenseBL expenseBL;
        private readonly ISalaryBL salaryBL;
        private readonly IChequeBL chequeBL;
        private readonly ICreditCardBL creditCardBL;
        private readonly IMonthlyPlanTemplateBL monthlyTemplateBL;
        // Infra & Services
        private static ILocalLogger _log;

        public Application(
                IAnnualBudgetBL AnnualBudgetBL,
                IMonthlyBudgetBL MonthlyBudgetBL,

                IExpenseBL BudgetItemBL,
                ISalaryBL SalaryBL,
                IChequeBL ChequeBL,
                ICreditCardBL CreditCardBL,

                IMonthlyPlanTemplateBL MonthlyPlanTemplateBL,
                ILocalLogger LocalLogger)
        {
            // DI
            annualBudgetBL = AnnualBudgetBL;
            monthlyBudgetBL = MonthlyBudgetBL;
            expenseBL = BudgetItemBL;
            salaryBL = SalaryBL;
            chequeBL = ChequeBL;
            creditCardBL = CreditCardBL;
            monthlyTemplateBL = MonthlyPlanTemplateBL;
            _log = LocalLogger;
            // Init logger
            _log.SetType(typeof(Application));
            // BL hashtable
            buildBusinessLayersHashtable();
        }

        private void buildBusinessLayersHashtable()
        {
            businessLayers = new Hashtable();
            //var p = Activator.CreateInstance(typeof(IBudgetItemBL));
            try
            {
                businessLayers.Add(typeof(AnnualBudget), annualBudgetBL);
                businessLayers.Add(typeof(MonthlyBudget), monthlyBudgetBL);

                businessLayers.Add(typeof(Salary), salaryBL);
                businessLayers.Add(typeof(Expense), expenseBL);
                businessLayers.Add(typeof(Cheque), chequeBL);
                businessLayers.Add(typeof(CreditCard), creditCardBL);

                businessLayers.Add(typeof(MonthlyPlanTemplate), monthlyTemplateBL);
            }
            catch (Exception Ex)
            {
                string errMessage = "Business layers hashtable creation was failed.";
                _log.Error(string.Format("{0} : {1} {2}", this.GetType().Name, errMessage + Environment.NewLine, Ex.Message), Ex);

                
                throw;
            }
        }

        public bool DefaultAddEntity(IEntity Entity)
        {
            bool actionResult = true;
            if (Entity != null)
            {
                Entity.EntityState = EntityState.Added;
                try
                {
                    if (Entity is AnnualBudget)
                    {
                        AnnualBudget ab = Entity as AnnualBudget;
                        // set related entities to added
                        if (ab.MonthlyBudgets != null && ab.MonthlyBudgets.Count > 0)
                        {
                            ab.MonthlyBudgets.Select(b => { b.Id = 0; b.EntityState = EntityState.Added; return b; }).ToList();
                        }
                        annualBudgetBL.Add(Entity as AnnualBudget);
                    }
                    else if (Entity is MonthlyBudget)
                    {
                        MonthlyBudget mb = Entity as MonthlyBudget;
                        // set related entities to added
                        if (mb.Expenses != null && mb.Expenses.Count > 0)
                        {
                            mb.Expenses.Select(b => { b.Id = 0; b.EntityState = EntityState.Added; return b; }).ToList();
                        }
                        monthlyBudgetBL.Add(Entity as MonthlyBudget);
                    }
                    else if (Entity is Expense)
                    {
                        expenseBL.Add(Entity as Expense);
                    }
                    else if (Entity is MonthlyPlanTemplate)
                    {
                        monthlyTemplateBL.Add(Entity as MonthlyPlanTemplate);
                    }
                    else
                    {
                        actionResult = false;
                    }
                }
                catch (Exception Ex)
                {
                    string errMessage = "Save new entity failed.";
                    _log.Error(string.Format("{0} : {1} {2}", this.GetType().Name, errMessage + Environment.NewLine, Ex.Message), Ex);
                }
            }
            return actionResult;
        }

        public bool DefaultUpdateEntity(IEntity Entity)
        {
            bool actionResult = true;
            if (Entity != null)
            {
                Entity.EntityState = EntityState.Modified;
                try
                {
                    if (Entity is AnnualBudget)
                    {
                        annualBudgetBL.Update(Entity as AnnualBudget);
                    }
                    else if (Entity is MonthlyBudget)
                    {
                        monthlyBudgetBL.Update(Entity as MonthlyBudget);
                    }
                    else if (Entity is Expense)
                    {
                        expenseBL.Update(Entity as Expense);
                    }
                    else if (Entity is MonthlyPlanTemplate)
                    {
                        monthlyTemplateBL.Update(Entity as MonthlyPlanTemplate);
                    }
                    else
                    {
                        actionResult = false;
                    }
                }
                catch (Exception Ex)
                {
                    string errMessage = "Save new entity failed.";
                    _log.Error(string.Format("{0} : {1} {2}", this.GetType().Name, errMessage + Environment.NewLine, Ex.Message), Ex);
                }
            }
            return actionResult;
        }

        public Model GetById<Model>(int modelId)
        {
            Type modelType = typeof(Model);
            dynamic model = null, businessLayer = null;

            // Try get the relevant business later
            businessLayer = businessLayers[typeof(Model)];
            // If business layer is located, then continue
            if (businessLayer != null)
            {
                try
                {
                    model = businessLayer.GetById(modelId);
                }
                catch (Exception Ex)
                {
                    _log.Error(string.Format("{0} : GetById() failed to get item, item ID: {1}. {2}", this.GetType().Name, modelId, Ex.Message), Ex);
                    throw;
                }
            }

            return model;
        }

        public IList<Model> GetEntities<Model>(Func<Model, bool> where, params Expression<Func<Model, object>>[] navigationProperties)
        {
            Type modelType = typeof(Model);
            dynamic model = null, businessLayer = null;
            // Try get the relevant business later
            businessLayer = businessLayers[typeof(Model)];
            // If business layer is located, then continue
            if (businessLayer != null)
            {
                try
                {
                    model = businessLayer.GetList(where, navigationProperties);
                }
                catch (Exception Ex)
                {
                    _log.Error(string.Format("{0} : GetEntities failed. {1}", this.GetType().Name, Ex.Message), Ex);
                    throw;
                }
            }

            return model;
        }

        public IList<Model> GetEntities<Model>(params Expression<Func<Model, object>>[] navigationProperties)
        {
            Type modelType = typeof(Model);
            dynamic model = null, businessLayer = null;
            // Try get the relevant business later
            businessLayer = businessLayers[typeof(Model)];
            // If business layer is located, then continue
            if (businessLayer != null)
            {
                try
                {
                    model = businessLayer.GetAll(navigationProperties);
                }
                catch (Exception Ex)
                {
                    _log.Error(string.Format("{0} : GetEntities failed. {1}", this.GetType().Name, Ex.Message), Ex);
                    throw;
                }
            }

            return model;
        }

        #region Annual Budgets
        public AnnualBudget GetAnnualBudget(int AnnualBudgetId)
        {
            AnnualBudget annualBudget = null;

            if (AnnualBudgetId != 0)
            {
                try
                {
                    annualBudget = annualBudgetBL.GetById(AnnualBudgetId, ab => ab.MonthlyBudgets);
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("{0} : Failed to get annual budget by Id {1} : \r\n{2}", Reflection.GetCurrentMethodName(), AnnualBudgetId, Ex.Message);
                    throw;
                }
            }

            return annualBudget;
        }

        public bool DeleteAnnualBudget(AnnualBudget AnnualBudget)
        {
            bool actionResult = false;
            if (AnnualBudget != null)
            {
                AnnualBudget.EntityState = EntityState.Deleted;

                // Delete annual budget and all it's related child entities
                foreach (MonthlyBudget mb in AnnualBudget.MonthlyBudgets)
                {
                    // delete monthly budget
                    try
                    {
                        DeleteMonthlyBudget(mb);
                    }
                    catch (Exception Ex)
                    {
                        _log.ErrorFormat("{0} : Unable to delete related monthly plan (ID: {1}). {2}", Reflection.GetCurrentMethodName(), mb.Id, Ex.Message);
                        // Handle cases of unable to delete monthly plans
                        // TODO HERE

                        throw;
                    }
                }
                // remove the relation to the old entities
                AnnualBudget.MonthlyBudgets = null;
                // delete the main entity from the database
                try
                {
                    annualBudgetBL.Remove(AnnualBudget);
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("{0} : Unable to delete annual plan(s). {1}", Reflection.GetCurrentMethodName(), Ex.Message);
                    throw;
                }
                actionResult = true;
            }

            return actionResult;
        }

        #endregion

        #region Monthly Budgets

        public BaseResult DeleteMonthlyBudget(MonthlyBudget monthlyBudget)
        {
            BaseResult result = null;
            if (monthlyBudget != null)
            {
                try
                {
                    int id = monthlyBudget.Id;
                    monthlyBudget.EntityState = EntityState.Deleted;

                    // Delete annual budget and all it's related child entities
                    foreach (Expense bi in monthlyBudget.Expenses)
                    {
                        // Delete budget item
                        try
                        {
                            DeleteBudget(bi);
                        }
                        catch (Exception Ex)
                        {
                            _log.ErrorFormat("{0} : Unable to delete related budget item (ID: {1}). {2}", Reflection.GetCurrentMethodName(), bi.Id, Ex.Message);
                            // Handle cases of unable to delete budget items
                            // TODO HERE

                            // BaseResult object
                            result =
                                new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                                {
                                    Message = string.Format("Unable to delete related budget item (ID: {0})", bi.Id),
                                    Value = Ex.Message
                                };

                            throw;
                        }
                    }
                    // remove the relation to the old entities
                    monthlyBudget.Expenses = null;
                    // delete the main entity from the database
                    monthlyBudgetBL.Remove(monthlyBudget);

                    result =
                        new OperationResult(ResultStatus.Success, "DeleteMonthlyBudget")
                        {
                            Value = id
                        };
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("Cannot fully delete monthly plan.\r\n{0}", Ex.Message);
                    result =
                        new OperationResult(ResultStatus.Exception, "DeleteMonthlyBudget")
                        {
                            Message = "Cannot delete monthly plan.",
                            Value = Ex.Message
                        };
                }
            }
            return result;
        }

        public MonthlyBudget GetMonthlyBudget(int monthlyBudgetId)
        {
            MonthlyBudget monthlyBudget = null;

            if (monthlyBudgetId != 0)
            {
                try
                {
                    monthlyBudget = monthlyBudgetBL.GetById(monthlyBudgetId,
                        m => m.Expenses,
                        m => m.Cheques,
                        m => m.CreditCards,
                        m => m.LoansPayments,
                        m => m.Salaries,
                        m => m.AdditionalIncome
                        );
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("{0} : Get monthly plan failed. {1}", Reflection.GetCurrentMethodName(), Ex.Message);
                    throw;
                }
            }

            return monthlyBudget;
        }

        public BaseResult CreateMonthlyBudget(MonthlyBudget monthlyBudget)
        {
            BaseResult result = null;
            if (monthlyBudget != null)
            {
                monthlyBudget.EntityState = EntityState.Added;
                // set related entities to added
                if (monthlyBudget.Expenses != null && monthlyBudget.Expenses.Count > 0)
                {
                    monthlyBudget.Expenses.Select(b => { b.Id = 0; b.EntityState = EntityState.Added; return b; }).ToList();
                }
                try
                {
                    monthlyBudgetBL.Add(monthlyBudget);

                    result = new OperationResult(ResultStatus.Success, Reflection.GetCurrentMethodName())
                    {
                        Value = monthlyBudget.Id
                    };
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("Cannot save new monthly plan.\r\n{0}", Ex.Message);
                    result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                    {
                        Message = "Cannot save new monthly plan.",
                        Value = Ex.Message
                    };
                }
            }
            return result;
        }

        #endregion

        #region Budget Items

        public BaseResult DeleteBudget(dynamic budgetItem)
        {
            BaseResult result = null;
            Type type = budgetItem.GetType();
            dynamic businessLayer = businessLayers[type];
            if (businessLayer != null)
            {
                if (budgetItem != null)
                {
                    int id = budgetItem.Id;
                    budgetItem.EntityState = EntityState.Deleted;
                    // Delete the budget item
                    try
                    {
                        businessLayer.Remove(budgetItem);
                        // update the monthly plan relevant fields
                        result = monthlyBudgetBL.updateMonthlyPlanPerBudgetItemUpdates(budgetItem, id);
                    }
                    catch (Exception Ex)
                    {
                        _log.Error(string.Format("{0} : Delete budget item failed.\r\n{1}", this.GetType().Name, Ex.Message), Ex);
                        result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                        {
                            Message = "Unable to remove budget item",
                            Value = Ex.Message
                        };
                    }
                }
                else
                {
                    result = new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                    {
                        Message = "Budget item must not be null",
                        Value = null
                    };
                }
            }
            else
            {
                result = new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = "Unable to delete budget item due to missing business logic layer",
                    Value = budgetItem.Id
                };
            }
            return result;
        }

        public BaseResult CreateBudgetItem<Model>(dynamic budgetItem)
        {
            BaseResult result = null;
            Type type = typeof(Model);
            if (budgetItem != null && budgetItem is BudgetItemBase)
            {
                try
                {
                    // try load the relevant business layer
                    dynamic businessLayer = businessLayers[type];
                    // update budget item fields
                    budgetItem.EntityState = EntityState.Added;
                    // save the budget item entity
                    budgetItem.CreationDate = DateTime.Now;
                    budgetItem.LastModifited = DateTime.Now;

                    businessLayer.Add(budgetItem);
                    // update the monthly plan relevant fields
                    result = monthlyBudgetBL.updateMonthlyPlanPerBudgetItemUpdates(budgetItem, budgetItem.Id);
                }
                catch (Exception Ex)
                {
                    _log.Error(string.Format("{0} : Cannot save new budget item.\r\n{1}", this.GetType().Name, Ex.Message), Ex);
                    result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                    {
                        Message = "Cannot save new budget item.",
                        Value = Ex.Message
                    };
                }

            }
            else
            {
                result = new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = "Budget item was not created",
                    Value = null
                };
            }
            return result;
        }

        public BaseResult UpdateBudgetItem<Model>(dynamic budgetItem)
        {
            BaseResult result = null;
            Type type = typeof(Model);
            if (budgetItem != null && budgetItem is BudgetItemBase)
            {
                try
                {
                    dynamic businessLayer = businessLayers[type];
                    // Load entity from DB to get old values
                    BudgetItemBase _oldBudget = businessLayer.GetById(budgetItem.Id);
                    if (_oldBudget != null)
                    {
                        double budget = _oldBudget.Amount;

                        budgetItem.LastModifited = DateTime.Now;
                        budgetItem.EntityState = EntityState.Modified;
                        // save the budget item entity
                        businessLayer.Update(budgetItem);
                        // update the monthly plan relevant fields
                        result = monthlyBudgetBL.updateMonthlyPlanPerBudgetItemUpdates(budgetItem, budgetItem.Id);
                    }
                    else
                    {
                        result = new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                        {
                            Message = "Unable to load old budget item",
                            Value = budgetItem.Id
                        };
                    }
                }
                catch (Exception Ex)
                {
                    _log.Error(string.Format("{0} : Cannot update budget item.\r\n{1}", this.GetType().Name, Ex.Message), Ex);
                    result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                    {
                        Message = "Cannot update budget item.",
                        Value = Ex.Message
                    };
                }
            }
            else
            {
                result = new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = "Budget item was not updated",
                    Value = null
                };
            }
            return result;
        }

        #endregion

        #region Templates

        public MonthlyPlanTemplate GetTemplate(int MonthlyPlanTemplateId)
        {
            MonthlyPlanTemplate budget = null;

            if (MonthlyPlanTemplateId != 0)
            {
                try
                {
                    budget = monthlyTemplateBL.GetById(MonthlyPlanTemplateId);
                }
                catch (Exception Ex)
                {
                    _log.Error("Exception occured : \r\n", Ex);
                }
            }
            return budget;
        }

        public bool DeleteTemplate(MonthlyPlanTemplate MonthlyPlanTemplate)
        {
            bool actionResult = false;
            if (MonthlyPlanTemplate != null)
            {
                MonthlyPlanTemplate.EntityState = EntityState.Deleted;

                monthlyTemplateBL.Remove(MonthlyPlanTemplate);
                actionResult = true;
            }
            return actionResult;
        }

        public BaseResult SaveMonthlyPlanAsTemplate(int? MonthlyPlanId, string TemplateName)
        {
            BaseResult Result = new OperationResult();
            MonthlyBudget budget = null;

            budget = GetMonthlyBudget(MonthlyPlanId.Value);
            if (budget != null)
            {
                var serializer = new JavaScriptSerializer();
                string templateJson = serializer.Serialize(budget);

                if (string.IsNullOrEmpty(templateJson) == false)
                {
                    // info log
                    _log.InfoFormat("Generated Template: {0}", templateJson);
                    // save
                    MonthlyPlanTemplate template = new MonthlyPlanTemplate()
                    {
                        EntityState = EntityState.Added,
                        Id = 0,
                        TemplateName = TemplateName,
                        Template = templateJson
                    };
                    bool result = DefaultAddEntity(template);
                    if (result)
                    {
                        Result = new OperationResult(ResultStatus.Success, "SaveMonthlyPlanAsTemplate");
                    }
                    else
                    {
                        Result = new OperationResult(ResultStatus.Failure, "SaveMonthlyPlanAsTemplate");
                    }
                }
            }
            else
            {
                Result = new OperationResult(ResultStatus.Failure, "SaveMonthlyPlanAsTemplate")
                {
                    Message = string.Format("Unable to create template based on monthly pland id {0}, the template was not found in the database.", MonthlyPlanId),
                    Value = HttpStatusCode.NoContent
                };
            }

            return Result;
        }

        public BaseResult SaveMonthlyPlanAsTemplate(MonthlyPlanTemplateInfo templateInfo)
        {
            BaseResult result;

            if (templateInfo != null)
            {
                int? monthlyPlanId = templateInfo.PlanId;
                string templateName = templateInfo.TemplateName;

                if (monthlyPlanId.HasValue == true)
                {
                    if (string.IsNullOrEmpty(templateName) == false)
                    {
                        result = SaveMonthlyPlanAsTemplate(monthlyPlanId, templateName);
                        // handle res 
                    }
                    else
                    {
                        result = new ValidationResult(ResultStatus.Failure, "TemplateName")
                        {
                            Message = "Template name must not be an empty string",
                            Value = HttpStatusCode.BadRequest
                        };
                    }
                }
                else
                {
                    result = new ValidationResult(ResultStatus.Failure, "PlanId")
                    {
                        Message = "Plan Id must not be an empty string",
                        Value = HttpStatusCode.BadRequest
                    };
                }
            }
            else
            {
                result = new ValidationResult(ResultStatus.Failure, "TemplateInfo")
                {
                    Message = "Bad Request",
                    Value = HttpStatusCode.BadRequest
                };
            }

            return result;
        }

        public BaseResult CreateFromTemplate(int MonthlyPlanTemplateId)
        {
            BaseResult result = null;

            MonthlyPlanTemplate template = null;
            try
            {
                template = GetTemplate(MonthlyPlanTemplateId);
                if (template != null)
                {
                    // template loaded, try deserialize into object from JSON format
                    MonthlyBudget _tempBudget = Newtonsoft.Json.JsonConvert.DeserializeObject<MonthlyBudget>(template.Template);
                    // Add new monthly, then iterate and add all related entites
                    _tempBudget.Id = 0;

                    bool action = DefaultAddEntity(_tempBudget);
                    if (action)
                    {
                        result = new OperationResult(ResultStatus.Success, "CreateFromTemplate")
                        {
                            Value = _tempBudget.Id
                        };
                    }
                    else
                    {
                        result = new OperationResult(ResultStatus.Failure, "CreateFromTemplate")
                        {
                            Message = "Unable to create monthly budget from template"
                        };
                    }
                }
                else
                {
                    result = new OperationResult(ResultStatus.Failure, "CreateFromTemplate")
                    {
                        Message = "Template not found",
                        Value = HttpStatusCode.NotFound
                    };
                }
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("Unable to create monthly budget from template.\r\n{0}", Ex);

                result = new OperationResult(ResultStatus.Exception, "CreateFromTemplate")
                {
                    Message = "Could not load template from database.",
                    Value = HttpStatusCode.InternalServerError
                };
            }
            return result;
        }
        #endregion
    }
}
