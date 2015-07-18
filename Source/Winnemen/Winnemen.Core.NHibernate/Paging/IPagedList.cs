using System.Collections.Generic;
using System.Linq;

namespace Winnemen.Core.NHibernate.Paging
{
    public interface IPagedList<T> : IList<T>
    {
        int PageIndex { get; }

        int PageSize { get; }

        int TotalCount { get; }

        int TotalPages { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }

    public class DataPagedList<T> : List<T>, IPagedList<T>
    {
        public DataPagedList(IQueryable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            AddRange(source);

            var total = totalCount; // source.Count();
            TotalCount = total;
            TotalPages = total / pageSize;

            if (total % pageSize > 0)
            {
                TotalPages++;
            }

            PageSize = pageSize;
            PageIndex = pageIndex;
            AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        public DataPagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            AddRange(source);

            //var total = source.Count();
            TotalCount = totalCount;
            TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
            {
                TotalPages++;
            }

            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }



        public int TotalCount { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }
}
