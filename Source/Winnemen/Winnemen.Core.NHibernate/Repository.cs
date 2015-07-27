using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using Winnemen.Core.NHibernate.OrderBy;
using Winnemen.Core.NHibernate.Paging;

namespace Winnemen.Core.NHibernate
{
    public class Repository<TScheme> : IRepository<TScheme> where TScheme : class, new()
    {
        private readonly ISession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TScheme}"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public Repository(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>TScheme.</returns>
        public TScheme GetById(object id)
        {
            TScheme value;

            using (var trans = _session.BeginTransaction())
            {
                value = _session.Get<TScheme>(id);
                trans.Commit();
            }

            return value;
        }

        /// <summary>
        /// Singles the or default.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>TScheme.</returns>
        public TScheme SingleOrDefault(Expression<Func<TScheme, bool>> @where)
        {
            TScheme value;

            using (var trans = _session.BeginTransaction())
            {
                value = _session.QueryOver<TScheme>()
                    .Where(@where)
                    .SingleOrDefault();

                trans.Commit();
            }

            return value;
        }

        /// <summary>
        /// Lists the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>List&lt;TScheme&gt;.</returns>
        public List<TScheme> List(Expression<Func<TScheme, bool>> @where)
        {
            List<TScheme> value;

            using (var trans = _session.BeginTransaction())
            {
                value = _session.QueryOver<TScheme>()
                    .Where(@where)
                    .List().ToList();

                trans.Commit();
            }

            return value;
        }

        public List<TScheme> List(ICriterion restriction)
        {
            List<TScheme> value;

            using (var trans = _session.BeginTransaction())
            {
                value = _session.QueryOver<TScheme>()
                    .Where(restriction)
                    .List().ToList();

                trans.Commit();
            }

            return value;
        }

        /// <summary>
        /// Alls this instance.
        /// </summary>
        /// <returns>List&lt;TScheme&gt;.</returns>
        public List<TScheme> All()
        {
            List<TScheme> value;

            using (var trans = _session.BeginTransaction())
            {
                value = _session.QueryOver<TScheme>()
                    .List().ToList();

                trans.Commit();
            }

            return value;
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Delete(TScheme item)
        {
            using (var trans = _session.BeginTransaction())
            {
                _session.Delete(item);
                trans.Commit();
            }
        }

        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>TScheme.</returns>
        public TScheme Save(TScheme item)
        {
            using (var trans = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(item);
                trans.Commit();
            }

            return item;
        }

        public IPagedList<TScheme> Paged(int pageIndex, int pageSize)
        {
            using (var trans = _session.BeginTransaction())
            {
                var rowCount = _session.CreateCriteria<TScheme>()
                                    .SetProjection(Projections.RowCount())
                                    .FutureValue<int>();

                var results = _session.QueryOver<TScheme>()
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Future<TScheme>()
                    .ToList<TScheme>();

                trans.Commit();

                return new DataPagedList<TScheme>(results, pageIndex, pageSize, rowCount.Value);
            }
        }

        public IPagedList<TScheme> Paged(int pageIndex, int pageSize, Expression<Func<TScheme, bool>> @where)
        {
            using (var trans = _session.BeginTransaction())
            {
                var rowCount = _session.QueryOver<TScheme>()
                                .Where(@where)
                                .Select(Projections.RowCount())
                                .FutureValue<int>();

                var results = _session.QueryOver<TScheme>()
                    .Where(@where)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Future<TScheme>()
                    .ToList<TScheme>();

                trans.Commit();

                return new DataPagedList<TScheme>(results, pageIndex, pageSize, rowCount.Value);
            }
        }

        private static IQueryOver<TScheme, TScheme> SetOrderByDirection(OrderByBuilder<TScheme> orderBy, IQueryOver<TScheme, TScheme> query)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (orderBy.OrderByDirection == OrderByDirection.Ascending)
            {
                return query.OrderBy(orderBy.OrderExpresssion).Asc;
            }

            return query.OrderBy(orderBy.OrderExpresssion).Desc;
        }

        public IPagedList<TScheme> Paged(int pageIndex, int pageSize, Expression<Func<TScheme, bool>> @where, Func<OrderByBuilder<TScheme>, OrderByBuilder<TScheme>> orderBy)
        {
            using (var trans = _session.BeginTransaction())
            {
                var rowCountQuery = _session.QueryOver<TScheme>()
                                .Where(@where);

                 rowCountQuery = SetOrderByDirection(orderBy(new OrderByBuilder<TScheme>()), rowCountQuery);

                 var rowCount = rowCountQuery.Select(Projections.RowCount())
                                .FutureValue<int>();

                var resultsQuery = _session.QueryOver<TScheme>()
                    .Where(@where);

                resultsQuery = SetOrderByDirection(orderBy(new OrderByBuilder<TScheme>()), resultsQuery);

                var results = resultsQuery.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Future<TScheme>()
                    .ToList<TScheme>();

                trans.Commit();

                return new DataPagedList<TScheme>(results, pageIndex, pageSize, rowCount.Value);
            }
        }

        public IPagedList<TScheme> Paged(int pageIndex, int pageSize, ICriterion restriction)
        {
            using (var trans = _session.BeginTransaction())
            {
                var rowCount = _session.QueryOver<TScheme>()
                                .Where(restriction)
                                .Select(Projections.RowCount())
                                .FutureValue<int>();

                var query = _session.QueryOver<TScheme>()
                    .Where(restriction);
                
                var results = query.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Future<TScheme>()
                    .ToList<TScheme>();

                trans.Commit();

                return new DataPagedList<TScheme>(results, pageIndex, pageSize, rowCount.Value);
            }
        }
    }
}