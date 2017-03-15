using BudgetMake.Shared.DomainModel;
using System.Data.Entity;

namespace BudgetMake.Infra.Repository
{
    public class BudgetMakeDBContext : DbContext
    {
        public BudgetMakeDBContext() : base("BudgetMakeDB")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AnnualBudget> AnnualBudgetPlans { get; set; }

        public DbSet<MonthlyBudget> MonthlyBudgetPlans { get; set; }

        public DbSet<Expense> BudgetItems { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        public DbSet<Cheque> Cheques { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<LoanPayment> LoanPayments { get; set; }

        public DbSet<MonthlyPlanTemplate> MonthlyPlanTemplates { get; set; }

    }
}
