using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.Contracts;
using GeneralServices.Interfaces;
using GeneralServices.Helpers;

namespace BudgetMake.Domain.BusinessLogic
{
    public class BaseBL<Model> : ICoreActions<Model> where Model : class, IEntity
    {
        protected readonly IGenericRepository<Model> repository;
        protected static ILocalLogger _log { get; set; }

        protected string extractExceptionMessage(Exception Ex)
        {
            string message = "";

            if (Ex != null)
            {
                message = Environment.NewLine + Ex.Message + Environment.NewLine + Ex.InnerException?.Message;
            }

            return message;
        }

        public BaseBL(IGenericRepository<Model> Repository, ILocalLogger Logger)
        {
            repository = Repository;
            _log = Logger;
        }

        public IList<Model> GetAll(params Expression<Func<Model, object>>[] navigationProperties)
        {
            IList<Model> list = null;

            try
            {
                list = repository.GetAll(navigationProperties);
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("{0} : Get all item(s) failed. {1}", Reflection.GetCurrentMethodName(), extractExceptionMessage(Ex));
                throw;
            }

            return list;
        }

        public IList<Model> GetList(Func<Model, bool> where, params Expression<Func<Model, object>>[] navigationProperties)
        {
            IList<Model> list = null;

            try
            {
                list = repository.GetList(where, navigationProperties);
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("{0} : Get item list failed. {1}", Reflection.GetCurrentMethodName(), extractExceptionMessage(Ex));
                throw;
            }

            return list;
        }

        public Model GetSingle(Func<Model, bool> where, params Expression<Func<Model, object>>[] navigationProperties)
        {
            Model model = null;

            try
            {
                model = repository.GetSingle(where, navigationProperties);
            }
            catch (Exception Ex)
            {
                _log.ErrorFormat("{0} : Get single item failed. {1}", Reflection.GetCurrentMethodName(), extractExceptionMessage(Ex));
                throw;
            }

            return model;
        }

        public void Add(params Model[] items)
        {
            if (items != null && items.Length > 0)
            {
                try
                {
                    repository.Add(items);
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("{0} : Add item(s) failed. {1}", Reflection.GetCurrentMethodName(), extractExceptionMessage(Ex));
                    throw;
                }
            }
        }

        public void Remove(params Model[] items)
        {
            if (items != null && items.Length > 0)
            {
                try
                {
                    repository.Remove(items);
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("{0} : Remove item(s) failed. {1}", Reflection.GetCurrentMethodName(), extractExceptionMessage(Ex)); 
                    throw;
                }
            }
        }

        public void Update(params Model[] items)
        {
            if (items != null && items.Length > 0)
            {
                try
                {
                    repository.Update(items);
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("{0} : Update item(s) failed. {1}", Reflection.GetCurrentMethodName(), extractExceptionMessage(Ex));
                    throw;
                }
            }
        }

        public Model GetById(int? Id, params Expression<Func<Model, object>>[] navigationProperties)
        {
            Model model = null;

            if (Id != null && Id.Value > 0)
            {
                try
                {
                    model = repository.GetSingle(b => b.Id == Id.Value, navigationProperties);
                }
                catch (Exception Ex)
                {
                    _log.ErrorFormat("{0} : Unable to get entity by Id: {1} {2}", Reflection.GetCurrentMethodName(), Id.Value, extractExceptionMessage(Ex));
                    throw;
                }
            }

            return model;
        }
    }
}
