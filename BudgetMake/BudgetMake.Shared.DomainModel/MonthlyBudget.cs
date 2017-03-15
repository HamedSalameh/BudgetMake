using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static GeneralServices.Enums;

namespace BudgetMake.Shared.DomainModel
{
    public class MonthlyBudget : Entity
    {
        [Required]
        public virtual MonthNames MonthName { get; set; }

        /// <summary>
        /// Indicates the real bank account banace at the first day of month
        /// Used to predict how the month will end and other calculations
        /// </summary>
        public virtual double OpeningBalance { get; set; }

        [Required]
        public virtual double BaseBudget { get; set; }

        [MaxLength(255)]
        public virtual string Comments { get; set; }

        public virtual ICollection<Salary> Salaries { get; set; }

        public virtual ICollection<Income> AdditionalIncome { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }

        public virtual ICollection<Cheque> Cheques { get; set; }

        public virtual ICollection<CreditCard> CreditCards { get; set; }

        public virtual ICollection<LoanPayment> LoansPayments { get; set; }

        // FK
        [Required]
        public virtual int AnnualBudgetId { get; set; }
    }
}
