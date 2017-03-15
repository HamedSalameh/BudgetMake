using GeneralServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static GeneralServices.Enums;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class HistoryLogViewModel
    {
        public virtual int HistoryLogID { get; set; }

        /// <summary>
        /// The type of the domain model entity that is connected to this event entry
        /// </summary>
        [Required]
        public virtual int EntityTypeID { get; set; }

        /// <summary>
        /// ID of the domain model entity that is connected to this event entry
        /// </summary>
        [Required]
        [Display(Name = "ID", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual int EntityID { get; set; }

        public virtual int? EntityOwnerID { get; set; }

        [Required]
        [Display(Name = "EventDate", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual DateTime Date { get; set; }

        [Required]
        [Display(Name = "CRUDType", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual CRUDType CRUDType { get; set; }

        [Display(Name = "User", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual int ActionUserID { get; set; }

        public List<EntityPropertyChangeViewModel> EntityPropertyChangeViewModel { get; set; }

        public virtual int HashID { get; set; }
    }

    public class EntityPropertyChangeViewModel
    {
        public int EntityPropertyChangeID { get; set; }

        [Required]
        public int HistoryLogID { get; set; }

        [Required]
        public int EntityPropertyID { get; set; }

        [Display(Name = "EntityPropertyName", ResourceType = typeof(Shared.Common.Resources.General))]
        public string EntityPropertyName { get; set; }

        [Display(Name = "CurrentValueAsText", ResourceType = typeof(Shared.Common.Resources.General))]
        public string CurrentValueAsText { get; set; }

        [Display(Name = "OriginalValueAsText", ResourceType = typeof(Shared.Common.Resources.General))]
        public string OriginalValueAsText { get; set; }

        [Required]
        [Display(Name = "EventDate", ResourceType = typeof(Shared.Common.Resources.General))]
        public DateTime Date { get; set; }

        public int HashID { get; set; }
    }

    public static class HistoryLogViewModelExtention
    {
        public static HistoryLogViewModel MapRecord(this HistoryLog log)
        {
            HistoryLogViewModel viewModel = null;

            if(log != null)
            {
                viewModel = new HistoryLogViewModel()
                {
                    ActionUserID = log.ActionUserID,
                    CRUDType = log.CRUDType,
                    Date = log.Date,
                    EntityID = log.EntityID,
                    EntityOwnerID = log.EntityOwnerID,
                    EntityTypeID = log.EntityTypeID,
                    HashID = log.HashID,
                    HistoryLogID = log.HistoryLogID
                };
            }

            return viewModel;
        }

        public static List<HistoryLogViewModel> MapHistoryLog(this List<HistoryLog> log)
        {
            List<HistoryLogViewModel> viewModel = new List<HistoryLogViewModel>();

            if(log != null && log.Count >0 )
            {
                foreach(var entry in log)
                {
                    HistoryLogViewModel _entry = entry.MapRecord();
                    if(_entry != null)
                    {
                        viewModel.Add(_entry);
                    }
                }
            }

            return viewModel;
        }

        public static EntityPropertyChangeViewModel MapEPCRecord(this EntityPropertyChange change)
        {
            EntityPropertyChangeViewModel viewModel = null;

            if(change != null)
            {
                viewModel = new EntityPropertyChangeViewModel()
                {
                    CurrentValueAsText = change.CurrentValueAsText,
                    Date = change.Date,
                    EntityPropertyChangeID = change.EntityPropertyChangeID,
                    EntityPropertyID = change.EntityPropertyID,
                    HashID = change.HashID,
                    HistoryLogID = change.HistoryLogID,
                    OriginalValueAsText = change.OriginalValueAsText,
                    EntityPropertyName = Helpers.GeneralHelpers.GetEntityPropertyNameByID(change.EntityPropertyID)
                };
            }

            return viewModel;
        }

        public static List<EntityPropertyChangeViewModel> MapEPCList(this List<EntityPropertyChange> list)
        {
            List<EntityPropertyChangeViewModel> viewModel = new List<EntityPropertyChangeViewModel>();

            if (list != null && list.Count > 0)
            {
                foreach (var entry in list)
                {
                    EntityPropertyChangeViewModel _entry = entry.MapEPCRecord();
                    if (_entry != null)
                    {
                        viewModel.Add(_entry);
                    }
                }
            }

            return viewModel;
        }
    }
}