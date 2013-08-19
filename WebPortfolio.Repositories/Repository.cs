using System;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Core.DataAccess;
using System.Data.Entity;
using WebPortfolio.Core.DataAccess.Abstract;
using System.Collections.Generic;


namespace WebPortfolio.Repositories
{
    [DataObject]
    public class Repository<T, C> : RepositoryBase<C>, IRepository<T>
        where T : class, IEntity
        where C : DbContext, new()
    {
        /// <summary>
        /// Returns entity by Id as long as the table name
        /// contains a column Id. If not, throws an exception
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            //var type = typeof(T);

            //if (type.GetProperties().Any(x => x.Name.ToLower() == "id"))
            //    throw new Exception("Table does not contain a column Id");

            //var parm = Expression.Parameter(type, type.Name);
            //var predicate = Expression.Lambda<Func<T, bool>>
            //        (Expression.Convert(Expression.Property(parm, "Id"), typeof(int)), parm);
            //return DataContext.Set<T>().Where(predicate).SingleOrDefault();

            return DataContext.Set<T>().Where(x => x.Id == id).FirstOrDefault();
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return DataContext.Set<T>().Where(predicate).SingleOrDefault();

        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return DataContext.Set<T>().Where(predicate);
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

        public virtual IQueryable<T> GetList()
        {
            return DataContext.Set<T>();
        }

        private void Insert(T entity, bool submit)
        {
            DataContext.Set<T>().Add(entity);
            //opStatus.Status = (submit ? SubmitChanges() : DataContext.SaveChanges() > 0);
            if (submit)
                DataContext.SaveChanges();
        }

        public virtual void Insert(T entity)
        {
            Insert(entity, true);
        }

        private void Update(T entity, bool submit)
        {

            DataContext.Set<T>().Attach(entity);
            DataContext.Entry(entity).State = System.Data.EntityState.Modified;
            if (submit)
                DataContext.SaveChanges();
            //opStatus.Status = DataContext.SaveChanges() > 0;
        }

        public virtual void Update(T entity)
        {
            Update(entity, true);
        }

        private void InsertOrUpdate(T entity, bool submit)
        {
            if (entity.Id == 0)
                Insert(entity, submit);
            else
                Update(entity, submit);
        }

        public virtual void InsertOrUpdate(T entity)
        {
            InsertOrUpdate(entity, true);
        }

        public virtual void InsertOrUpdateCollection(ICollection<T> collection)
        {
            foreach (var entity in collection)
            {
                InsertOrUpdate(entity, false);
            }

            DataContext.SaveChanges();
        }

        public int ExecuteStoreCommand(string cmdText, params object[] parameters)
        {
            int recordsAffected;

            recordsAffected = DataContext.Database.ExecuteSqlCommand(cmdText, parameters);
            
            return recordsAffected;
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
                return;

            var objectSet = DataContext.Set<T>();
            objectSet.Attach(entity);
            objectSet.Remove(entity);
            DataContext.SaveChanges();
            //opStatus.Status = DataContext.SaveChanges() > 0;

        }


        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {

                var objectSet = DataContext.Set<T>().Where(predicate).FirstOrDefault();
                DataContext.Entry(objectSet).State = System.Data.EntityState.Deleted;
            DataContext.SaveChanges();
                //opStatus.Status = DataContext.SaveChanges() > 0;


        }


        //public bool Save()
        //{
        //    var status = DataContext.SaveChanges() > 0;
        //    return status;
        //}

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
