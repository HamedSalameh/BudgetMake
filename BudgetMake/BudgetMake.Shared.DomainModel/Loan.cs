using GeneralServices.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMake.Shared.DomainModel
{
    public class Loan : IEntity
    {
        [Key]
        public virtual int Id { get; set; }

        [MaxLength(50)]
        public virtual string Description { get; set; }

        [Required]
        public virtual double Amount { get; set; }

        [MaxLength(250)]
        public virtual string Comments { get; set; }

        [Required]
        public virtual int NumberOfPayments { get; set; }

        public virtual double Interest { get; set; }

        [Required]
        public virtual double PrimeInterest { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Bank { get; set; }

        [Required]
        public ICollection<LoanPayment> LoanPayments { get; set; }

        // Interfaces
        [NotMapped]
        public EntityState EntityState { get; set; }
    }

    public class LoanPayment : BudgetItemBase
    {
        [Required]
        public virtual DateTime PaymentDate { get; set; }

        public virtual int LoanId { get; set; }

        // FK
        [ForeignKey("LoanId")]
        public virtual Loan Loan { get; set; }
    }
}
