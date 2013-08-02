using System;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Core.DataAccess;
using WebPortfolio.Core.DataAccess.Abstract;
using System.Data.Entity;

namespace WebPortfolio.Repositories
{
    [DataObject]
    public class Repository<T, C> : RepositoryBase<C>, IRepository<T>
        where T : class
        where C: DbContext, new()
    {
        public virtual T Get<T>(int id) where T : class, IEntity 
        {
            return DataContext.Set<T>().Where(x => x.Id == id).SingleOrDefault();
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {

                try
                {
                    return DataContext.Set<T>().Where(predicate).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                throw new ApplicationException("Predicate value must be passed to Get<T>.");
            }
            //return null;
        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return DataContext.Set<T>().Where(predicate);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        /*
        public virtual IQueryable<T> GetList<TKey>(Expression<Func<T, TKey>> orderBy)
        {
            try
            {
                return GetList().OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }
        */

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public virtual IQueryable<T> GetList()
        {
            try
            {
                return DataContext.Set<T>();
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public virtual OperationStatus Save(T entity, bool submit = false)
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                DataContext.Set<T>().Add(entity);
                opStatus.Status = (submit ? SubmitChanges() : DataContext.SaveChanges() > 0);
                
            }
            catch (Exception exp)
            {
                opStatus = OperationStatus.CreateFromException("Error saving " + typeof(T) + ".", exp);
            }

            return opStatus;
        }

        public virtual OperationStatus Update(T entity, params string[] propsToUpdate)
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                //DataContext.Set<T>().Attach(entity);
                DataContext.Entry(entity).State = System.Data.EntityState.Modified;
                opStatus.Status = DataContext.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                opStatus = OperationStatus.CreateFromException("Error updating " + typeof(T) + ".", exp);
            }

            return opStatus;
        }

        public OperationStatus ExecuteStoreCommand(string cmdText, params object[] parameters)
        {
            var opStatus = new OperationStatus { Status = true };

            try
            {
                opStatus.RecordsAffected = DataContext.Database.ExecuteSqlCommand(cmdText, parameters);
            }
            catch (Exception exp)
            {
                OperationStatus.CreateFromException("Error executing store command: ", exp);
            }
            return opStatus;
        }

        public virtual OperationStatus Delete<T>(T entity) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                var objectSet = DataContext.Set<T>();
                objectSet.Attach(entity);
                objectSet.Remove(entity);
                opStatus.Status = DataContext.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error deleting " + typeof(T), exp);
            }

            return opStatus;
        }


        public virtual OperationStatus Delete(Expression<Func<T, bool>> predicate)
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                var objectSet = DataContext.Set<T>().Where(predicate).FirstOrDefault();
                DataContext.Entry(objectSet).State = System.Data.EntityState.Deleted;
                opStatus.Status = DataContext.SaveChanges() > 0;

            }
            catch (Exception exp)
            {
                return OperationStatus.CreateFromException("Error deleting " + typeof(T), exp);
            }

            return opStatus;
        }

        /*
        public virtual IQueryable<T> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy)
        {
            try
            {
                return GetList(predicate).OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }
        */

        //Pagination functions
        public virtual IQueryable<T> GetPageOrderByAscending<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, int page, int pageSize)
        {
            return GetPage<TKey>(predicate, orderBy, page, pageSize);
        }

        public virtual IQueryable<T> GetPageOrderByAscending<TKey>(Expression<Func<T, TKey>> orderBy, int page, int pageSize)
        {
            return GetPage<TKey>(null, orderBy, page, pageSize);
        }

        public virtual IQueryable<T> GetPageOrderByDescendent<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, int page, int pageSize)
        {
            return GetPage<TKey>(predicate, orderBy, page, pageSize, "desc");
        }

        public virtual IQueryable<T> GetPageOrderByDescendent<TKey>(Expression<Func<T, TKey>> orderBy, int page, int pageSize)
        {
            return GetPage<TKey>(null, orderBy, page, pageSize, "desc");
        }

        public virtual IQueryable<T> GetPage<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, int page, int pageSize, string orderType = "asc")
        {
            IQueryable<T> r = null;
            if (predicate != null)
                r = GetList(predicate);
            else
                r = GetList();

            if (orderType == "desc")
                r = r.OrderByDescending(orderBy);
            else
                r = r.OrderBy(orderBy);

            r = r.Skip((page - 1) * pageSize)
                    .Take(pageSize);

            return r;
        }
    }
}
