namespace Winnemen.Core.NHibernate
{
    public class OrderByDirectionBuilder<TScheme>
    {
        private readonly OrderByBuilder<TScheme> _builder;

        public OrderByDirectionBuilder(OrderByBuilder<TScheme> builder)
        {
            _builder = builder;
        }

        public OrderByBuilder<TScheme> Asc
        {
            get
            {
                _builder.OrderByDirection = OrderByDirection.Ascending;
                return _builder;
            }
        }

        public OrderByBuilder<TScheme> Desc
        {
            get
            {
                _builder.OrderByDirection = OrderByDirection.Descending;
                return _builder;
            }
        }

    }
}