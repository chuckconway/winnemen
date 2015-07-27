using System;
using System.Linq.Expressions;

namespace Winnemen.Core.NHibernate
{
    public class OrderByBuilder<TScheme>
    {
        public OrderByBuilder()
        {
            this.OrderByDirection = OrderByDirection.Ascending;
        }

        public Expression<Func<TScheme, object>> OrderExpresssion { get; set; }

        public OrderByDirection OrderByDirection { get; set; }

        public OrderByDirectionBuilder<TScheme> OrderBy(Expression<Func<TScheme, object>> expression)
        {
            OrderExpresssion = expression;
            return new OrderByDirectionBuilder<TScheme>(this);
        }
    }
}