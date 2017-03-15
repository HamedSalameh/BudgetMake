using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using GeneralServices.Components;
using GeneralServices.Helpers;
using GeneralServices.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using static GeneralServices.Enums;

namespace BudgetMake.Infra.Repository
{
    public class GenericRepository<T, DbCtx> : GenericRepositoryBase<T, DbCtx> where T : Entity where DbCtx : DbContext, new()
    {
        protected static ILocalLogger _log { get; set; }
        protected HistoryService historyService;

        public GenericRepository()
        {
            historyService = HistoryService.Instance;
        }

        public override IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IList<T> list = null;

            try
            {
                // Do Logging
                list = base.GetAll(navigationProperties);
                // Do History logging (if needed)
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("{0} : Unable to get entites from data base.\r\n{1}", Reflection.GetCurrentMethodName(), Ex.Message);
                throw;
            }

            return list;
        }

        public override IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IList<T> list = null;

            try
            {
                // Do Logging
                list = base.GetList(where, navigationProperties);
                // Do History logging (if needed)
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("{0} : Unable to get entites from data base.\r\n{1}", Reflection.GetCurrentMethodName(), Ex.Message);
                throw;
            }

            return list;
        }

        public override T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T result = null;

            try
            {
                // Do Logging
                result = base.GetSingle(where, navigationProperties);
                // Do History logging (if needed)
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("Exception in GetSingle for {0} : \r\n {1}", typeof(T).Name, Ex);
                throw;
            }

            return result;
        }

        public override void Add(params T[] items)
        {
            try
            {
                // Before saving, iterate each item and update it's creation date
                foreach (var item in items)
                {
                    item.CreationDate = DateTime.Now;
                }

                base.Add(items);
                // when creating new item, just create a new entry in the history log and new entries
                // in the EPC (EntityPropertyChanges) table. no need to check for changes in props since they are all new
                foreach (var item in items)
                {
                    HistoryService.Instance.CreateHistoryEntry(item.Id, item, 0, CRUDType.Create);
                }
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("{0} : Unable to add item(s) to database. {1}", Reflection.GetCurrentMethodName(), Environment.NewLine + Ex.Message);
                throw;
            }
        }

        public override void Remove(params T[] items)
        {
            try
            {
                // before loading all items, load them from db to compare property changes
                var _ids = items.Select(i => i.Id).ToList();
                var _oldItems = GetList(e => _ids.Contains(e.Id));

                base.Remove(items);

                foreach (var UpdatedItem in items)
                {
                    T OldItem = _oldItems.FirstOrDefault(i => i.Id == UpdatedItem.Id);
                    if (OldItem != null)
                    {
                        HistoryService.Instance.CreateHistoryEntry(UpdatedItem.Id, OldItem, UpdatedItem, 0, CRUDType.Delete);
                    }
                }
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("{0} : Unable to remove item(s) from database. {1}", Reflection.GetCurrentMethodName(), Environment.NewLine + Ex.Message); 
                throw;
            }
        }

        public override void Update(params T[] items)
        {
            try
            {
                // before loading all items, load them from db to compare property changes
                var _ids = items.Select(i => i.Id).ToList();
                var _oldItems = GetList(e => _ids.Contains(e.Id));
                // try update the items
                base.Update(items);

                foreach (var UpdatedItem in items)
                {
                    T OldItem = _oldItems.FirstOrDefault(i => i.Id == UpdatedItem.Id);
                    if (OldItem != null)
                    {
                        HistoryService.Instance.CreateHistoryEntry(UpdatedItem.Id, OldItem, UpdatedItem, 0, CRUDType.Update);
                    }
                }
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("{0} : Update item(s) failed. {1}", Reflection.GetCurrentMethodName(), Environment.NewLine + Ex.Message);
                throw;
            }
        }        
    }
}
