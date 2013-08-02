using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using WebPortfolio.Core.DataAccess.Abstract;

namespace WebPortfolio.Core.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        T Get<T>(int id) where T : class, IEntity;

        T Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetList(Expression<Func<T, bool>> predicate);
        /*
        IQueryable<T> GetList<TKey>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> orderBy);

        IQueryable<T> GetList<TKey>(Expression<Func<T, TKey>> orderBy);
        */
        IQueryable<T> GetList();

        OperationStatus Save(T entity, bool submit = false);

        OperationStatus Update(T entity, params string[] propsToUpdate);

        OperationStatus ExecuteStoreCommand(string cmdText, params object[] parameters);

        OperationStatus Delete<T>(T entity) where T : class;

        OperationStatus Delete(Expression<Func<T, bool>> predicate);

        //Pagination functions
        IQueryable<T> GetPageOrderByAscending<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, int page, int pageSize);

        IQueryable<T> GetPageOrderByAscending<TKey>(Expression<Func<T, TKey>> orderBy, int page, int pageSize);

        IQueryable<T> GetPageOrderByDescendent<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, int page, int pageSize);

        IQueryable<T> GetPageOrderByDescendent<TKey>(Expression<Func<T, TKey>> orderBy, int page, int pageSize);

    }
}
