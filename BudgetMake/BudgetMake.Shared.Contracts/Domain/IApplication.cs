﻿using BudgetMake.Shared.DomainModel;
using GeneralServices;
using GeneralServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BudgetMake.Shared.Contracts.Domain
{
    public interface IApplication
    {
        bool DefaultAddEntity(IEntity Entity);
        bool DefaultUpdateEntity(IEntity Entity);

        Model GetById<Model>(int modelId);
        IList<Model> GetEntities<Model>(Func<Model, bool> where, params Expression<Func<Model, object>>[] navigationProperties);
        IList<Model> GetEntities<Model>(params Expression<Func<Model, object>>[] navigationProperties);

        AnnualBudget GetAnnualBudget(int AnnualBudgetId);
        bool DeleteAnnualBudget(AnnualBudget monthlyBudget);

        MonthlyBudget GetMonthlyBudget(int monthlyBudgetId);
        BaseResult DeleteMonthlyBudget(MonthlyBudget monthlyBudget);
        BaseResult CreateMonthlyBudget(MonthlyBudget monthlyBudget);

        BaseResult DeleteBudget(Expense budgetItem);
        BaseResult CreateBudget(Expense budgetItem);
        BaseResult UpdateBudget(Expense budgetItem);

        MonthlyPlanTemplate GetTemplate(int MonthlyPlanTemplateId);
        bool DeleteTemplate(MonthlyPlanTemplate MonthlyPlanTemplate);

        BaseResult SaveMonthlyPlanAsTemplate(int? MonthlyPlanId, string TemplateName);
        BaseResult SaveMonthlyPlanAsTemplate(MonthlyPlanTemplateInfo templateInfo);

        BaseResult CreateFromTemplate(int MonthlyPlanTemplateId);
    }
}