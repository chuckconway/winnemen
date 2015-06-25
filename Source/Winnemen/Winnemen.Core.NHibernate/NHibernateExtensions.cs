﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;

namespace Winnemen.Core.NHibernate
{
    public static class NHibernateExtensions
    {
        /// <summary>
        /// Procedures the list.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="session">The session.</param>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>List&lt;TReturn&gt;.</returns>
        public static IList<TReturn> ProcedureList<TParameters, TReturn>(this ISession session, string procedure, TParameters parameters) where TParameters : class
        {
            var query = SqlQuery<TParameters, TReturn>(session, procedure, parameters);
            return query.List<TReturn>();
        }

        /// <summary>
        /// Procedures the single.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="session">The session.</param>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>TReturn.</returns>
        public static TReturn ProcedureSingle<TParameters, TReturn>(this ISession session, string procedure, TParameters parameters) where TParameters : class
        {
            var query = SqlQuery<TParameters, TReturn>(session, procedure, parameters);
            return query.UniqueResult<TReturn>();
        }

        /// <summary>
        /// Procedures the single.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="session">The session.</param>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>TReturn.</returns>
        public static void ExecuteProcedure<TParameters>(this ISession session, string procedure, TParameters parameters) where TParameters : class
        {
            var query = SqlQueryNoReturn<TParameters>(session, procedure, parameters);
            query.ExecuteUpdate();
        }


        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="session">The session.</param>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>ISQLQuery.</returns>
        private static ISQLQuery SqlQueryNoReturn<TParameters>(ISession session, string procedure, TParameters parameters)
            where TParameters : class
        {
            var properties = parameters.GetType().GetProperties();

            string[] parameterNames = properties.Select(s => string.Format(":{0}", s.Name)).ToArray();
            string procedureWithParameters = string.Format("{0} {1}", procedure, string.Join(",", parameterNames));

            var query = session.CreateSQLQuery(procedureWithParameters);

            foreach (var property in properties)
            {
                query.SetParameter(property.Name, property.GetValue(parameters));
            }
            return query;
        }

        /// <summary>
        /// SQLs the query.
        /// </summary>
        /// <typeparam name="TParameters">The type of the t parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the t return.</typeparam>
        /// <param name="session">The session.</param>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>ISQLQuery.</returns>
        private static ISQLQuery SqlQuery<TParameters, TReturn>(ISession session, string procedure, TParameters parameters)
            where TParameters : class
        {
            var properties = parameters.GetType().GetProperties();

            string[] parameterNames = properties.Select(s => string.Format(":{0}", s.Name)).ToArray();
            string procedureWithParameters = string.Format("{0} {1}", procedure, string.Join(",", parameterNames));

            var query = session.CreateSQLQuery(procedureWithParameters).AddEntity(typeof(TReturn));

            foreach (var property in properties)
            {
                query.SetParameter(property.Name, property.GetValue(parameters));
            }
            return query;
        }

        public static IQueryOver<E, F> WhereStringIsNotNullOrEmpty<E, F>(this IQueryOver<E, F> query, Expression<Func<E, object>> propExpression)
        {
            var prop = Projections.Property(propExpression);
            var criteria = Restrictions.Or(Restrictions.IsNull(prop), Restrictions.Eq(Projections.SqlFunction("trim", NHibernateUtil.String, prop), ""));
            return query.Where(Restrictions.Not(criteria));
        }

    }
}
