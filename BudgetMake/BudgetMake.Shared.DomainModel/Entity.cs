using GeneralServices.Attributes;
using GeneralServices.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetMake.Shared.DomainModel
{
    public class Entity : IEntity
    {
        public virtual int Id { get; set; }

        [IgnoreChanges]
        [Required]
        public virtual DateTime CreationDate { get; set; }

        [IgnoreChanges]
        [Required]
        public virtual DateTime LastModifited { get; set; }

        [IgnoreChanges]
        [NotMapped]
        public virtual EntityState EntityState { set; get; }
    }
}
