using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using GeneralServices;
using GeneralServices.Helpers;
using System;
using static GeneralServices.Enums;

namespace BudgetMake.Domain.BusinessLogic
{
    public class MonthlyBudgetBL : BaseBL<MonthlyBudget>, IMonthlyBudgetBL
    {
        public MonthlyBudgetBL(IMonthlyBudgetRepo MonthlyBudgetRepo, ILocalLogger Log) : base(MonthlyBudgetRepo, Log)
        {
        }

        public BaseResult updateMonthlyPlanPerBudgetItemUpdates(dynamic budgetItem, int budgetItemId)
        {
            BaseResult result = null;
            MonthlyBudget monthly = null;
            if (budgetItem != null && budgetItemId != 0)
            {
                monthly = GetById((budgetItem as BudgetItemBase).MonthlyBudgetId,
                                m => m.Expenses,
                                m => m.Cheques,
                                m => m.CreditCards,
                                m => m.LoansPayments,
                                m => m.Salaries,
                                m => m.AdditionalIncome
                                );
                if (monthly != null)
                {
                    try
                    {
                        Update(monthly);
                        result = new OperationResult(ResultStatus.Success, Reflection.GetCurrentMethodName())
                        {
                            Value = budgetItemId
                        };
                    }
                    catch (Exception Ex)
                    {
                        _log.Error(String.Format("Unable to update monthly plan per latest changes. \r\n{0}", Ex.Message), Ex);
                        result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                        {
                            Message = "Unable to update monthly plan per latest changes",
                            Value = Ex.Message
                        };
                    }
                }
                else
                {
                    result = new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                    {
                        Message = "Unable to update monthly plan",
                        Value = monthly.Id
                    };
                } 
            }
            return result;
        }
    }
}
