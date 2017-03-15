namespace BudgetMake.Infra.Repository.Migrations
{
    using Shared.DomainModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using static GeneralServices.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<BudgetMakeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BudgetMakeDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.AnnualBudgetPlans.AddOrUpdate(p => p.AnnualBudgetName,
                new AnnualBudget
                {
                    AnnualBudgetName = "2017",
                    Description = "Year of 2016",
                    CreationDate = DateTime.Now,
                    LastModifited = DateTime.Now,
                    MonthlyBudgets = new List<MonthlyBudget>
                    {
                        new MonthlyBudget
                        {
                            MonthName = MonthNames.November,
                            BaseBudget = 1000,
                            Comments = "No additional comments",
                            CreationDate = DateTime.Now,
                            LastModifited = DateTime.Now,
                            Expenses = new List<Expense>
                            {
                                new Expense
                                {
                                    Amount = 250,
                                    AmountUsed = 250,
                                    Description = "Budget 1",
                                    Comments = "none",
                                    CreationDate = DateTime.Now,
                                    LastModifited = DateTime.Now
                                },
                                new Expense
                                {
                                    Description = "Test over usage",
                                    Amount = 300,
                                    AmountUsed = 520,
                                    CreationDate = DateTime.Now,
                                    LastModifited = DateTime.Now
                                },
                                new Expense
                                {
                                    Description = "test under used",
                                    Amount = 400,
                                    AmountUsed = 200,
                                    CreationDate = DateTime.Now,
                                    LastModifited = DateTime.Now
                                }
                            },
                            Salaries = new List<Salary>
                            {
                                new Salary
                                {
                                    Amount = 5000,
                                    PaymentDate = DateTime.Now,
                                    CreationDate = DateTime.Now,
                                    LastModifited = DateTime.Now
                                }
                            },
                            Cheques = new List<Cheque>
                            {
                                new Cheque
                                {
                                    Amount = 500,
                                    AmountUsed = 500,
                                    Description = "Sample cheque",
                                    Payee = "Dummy Payee",
                                    PaymentDate = DateTime.Now,
                                    Comments = "For general stuff",
                                    CreationDate = DateTime.Now,
                                    LastModifited = DateTime.Now
                                }
                            },
                            CreditCards = new List<CreditCard>
                            {
                                new CreditCard
                                {
                                    Amount = 600,
                                    AmountUsed = 600,
                                    CardType = CreditCardType.MasterCard,
                                    PaymentDate = DateTime.Now,
                                    Description = "Bank card",
                                    CreationDate = DateTime.Now,
                                    LastModifited = DateTime.Now
                                }
                            }
                        }
                    }
                }
                );
        }
    }
}
