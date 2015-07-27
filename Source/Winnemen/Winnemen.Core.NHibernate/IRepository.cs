using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate.Criterion;
using Winnemen.Core.NHibernate.OrderBy;
using Winnemen.Core.NHibernate.Paging;

namespace Winnemen.Core.NHibernate
{
    /// <summary>
    /// Interface IRepositoryBase
    /// </summary>
    /// <typeparam name="TScheme">The type of the t scheme.</typeparam>
    public interface IRepository<TScheme> where TScheme : class, new()
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TScheme.</returns>
        TScheme GetById(object id);

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>List&lt;TScheme&gt;.</returns>
        List<TScheme> All();

        /// <summary>
        /// Singles the or default.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>TScheme.</returns>
        TScheme SingleOrDefault(Expression<Func<TScheme, bool>> @where);

        /// <summary>
        /// Lists the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>List&lt;TScheme&gt;.</returns>
        List<TScheme> List(Expression<Func<TScheme, bool>> @where);

        /// <summary>
        /// Lists the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>List&lt;TScheme&gt;.</returns>
        List<TScheme> List(ICriterion restriction);

        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>TScheme.</returns>
        TScheme Save(TScheme item);


        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Delete(TScheme item);

        IPagedList<TScheme> Paged(int pageIndex, int pageSize);

        IPagedList<TScheme> Paged(int pageIndex, int pageSize, Expression<Func<TScheme, bool>> @where);

        IPagedList<TScheme> Paged(int pageIndex, int pageSize, Expression<Func<TScheme, bool>> @where, Func<OrderByBuilder<TScheme>, OrderByBuilder<TScheme>> orderBy);

        IPagedList<TScheme> Paged(int pageIndex, int pageSize, ICriterion restriction);
    }
}