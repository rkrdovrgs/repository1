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
using System.Data.Objects;
using System.Data.Entity.Infrastructure;


namespace WebPortfolio.Repositories
{
    [DataObject]
    public class Repository<T, C> : RepositoryBase<C>, IRepository<T>
        where T : class, IEntity
        where C : DbContext, new()
    {

        //private Dictionary<Type, string> _KeyNames;
        //private string GetKeyName(Type type)
        //{
        //    var _KeyName = string.Empty;

        //    if (!_KeyNames.ContainsKey(type))
        //    {
        //        ObjectContext objectContext = ((IObjectContextAdapter)DataContext).ObjectContext;
        //        var set = objectContext.CreateObjectSet<T>();

        //        IEnumerable<string> keyNames = set.EntitySet.ElementType
        //                                                    .KeyMembers
        //                                                    .Select(k => k.Name);
        //        _KeyName = keyNames.SingleOrDefault();
        //    }
        //    else
        //        _KeyName = _KeyNames[type];

        //    return _KeyName;

        //}


        //public Repository()
        //    : base()
        //{
        //    _KeyNames = new Dictionary<Type, string>();
        //}


        //private Expression<Func<T, bool>> BuildKeyPredicate(Type type, object filter)
        //{
        //    var fieldName = GetKeyName(type);
        //    var parm = Expression.Parameter(type, type.Name);
        //    var exp = Expression.PropertyOrField(parm, fieldName);
        //    var filt = Expression.Constant(filter);
        //    var predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(exp, filt), parm);

        //    return predicate;
        //}

        /// <summary>
        /// Returns entity by key as long as the table name
        /// contains a column Id. If not, throws an exception
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {

            //var predicate = BuildKeyPredicate(typeof(T), id);


            //return DataContext.Set<T>().Where(predicate).SingleOrDefault();

            return DataContext.Set<T>().Where(x => x.Id == id).FirstOrDefault();
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return DataContext.Set<T>().Where(predicate).FirstOrDefault();

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

        private void Insert(IEntity entity, bool submit)
        {
            DataContext.Set(entity.GetType()).Add(entity);
            //opStatus.Status = (submit ? SubmitChanges() : DataContext.SaveChanges() > 0);
            if (submit)
                DataContext.SaveChanges();
        }

        public virtual void Insert(T entity)
        {
            Insert(entity, true);
        }

        private void Update(IEntity entity, bool submit)
        {

            //var predicate = BuildKeyPredicate(entity.GetType(), KeyName, 

            var attachedEntity = DataContext.Set(entity.GetType()).Local
                                    .OfType<IEntity>()
                                    .FirstOrDefault(e => e.Id == entity.Id);

            if (attachedEntity != null)
            {
                DataContext.Entry(attachedEntity).CurrentValues.SetValues(entity);
            }

            else
            {
                DataContext.Set(entity.GetType()).Attach(entity);
                DataContext.Entry(entity).State = System.Data.EntityState.Modified;
            }


            if (submit)
                DataContext.SaveChanges();
            //opStatus.Status = DataContext.SaveChanges() > 0;
        }

        public virtual void Update(T entity)
        {
            Update(entity, true);
        }

        private void InsertOrUpdate(IEntity entity, bool submit)
        {
            ///Deep looping through
            var props = entity.GetType().GetProperties();
            foreach (var p in props.Where(x => x.PropertyType.GetInterfaces().Contains(typeof(IEntity))))
            {
                IEntity innerEntity = p.GetValue(entity) as IEntity;
                if (innerEntity != null)
                    InsertOrUpdate(innerEntity, false);
            }

            foreach (var p in props.Where(x => x.PropertyType.IsGenericType && (typeof(ICollection<>)).IsAssignableFrom(x.PropertyType.GetGenericTypeDefinition())))
            {
                var entCol = p.GetValue(entity) as IEnumerable<IEntity>;

                if (entCol != null)
                    InsertOrUpdateCollection(entCol, false);
            }



            if (entity.Id == 0)
                Insert(entity, submit);
            else
                Update(entity, submit);
        }

        public virtual void InsertOrUpdate(T entity)
        {
            InsertOrUpdate(entity, true);
        }

        private void InsertOrUpdateCollection(IEnumerable<IEntity> collection, bool submit)
        {
            foreach (var entity in collection)
            {
                InsertOrUpdate((IEntity)entity, false);
            }
            if (submit)
                DataContext.SaveChanges();
        }


        public virtual void InsertOrUpdateCollection(ICollection<T> collection)
        {

            InsertOrUpdateCollection(collection, true);
        }

        public int ExecuteStoreCommand(string cmdText, params object[] parameters)
        {
            int recordsAffected;

            recordsAffected = DataContext.Database.ExecuteSqlCommand(cmdText, parameters);

            return recordsAffected;
        }

        private void Delete(T entity, bool submit)
        {
            if (entity == null)
                return;

            var objectSet = DataContext.Set<T>();
            objectSet.Attach(entity);
            objectSet.Remove(entity);
            if (submit)
                DataContext.SaveChanges();


            //opStatus.Status = DataContext.SaveChanges() > 0;

        }

        public virtual void Delete(T entity)
        {
            Delete(entity, true);
        }

        public virtual void DeleteCollection(ICollection<T> collection)
        {
            foreach (var item in collection)
            {
                Delete(item, false);
            }

            DataContext.SaveChanges();
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
