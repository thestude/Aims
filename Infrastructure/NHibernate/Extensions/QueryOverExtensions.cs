using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using PagedList;

namespace AIMS.Infrastructure.NHibernate.Extensions
{
    public static class QueryOverExtensions
    {

        /// <summary>
        /// Add orderby to the query.
        /// </summary>
        /// <param name="queryOver"></param>
        /// <param name="orders"></param>
        public static void AddOrderToQuery(this IQueryOver queryOver, IEnumerable<Order> orders)
        {
            foreach (Order order in orders)
                queryOver.UnderlyingCriteria.AddOrder(order);
        }

        public static IQueryOver<T, T> AddOrder<T>(this IQueryOver<T, T> criteria, Expression<Func<object>> expression)
        {
            return criteria.OrderBy(expression).Asc;
        }

        /// <summary>
        /// Return a paged result of the query 
        /// </summary>
        /// <param name="query">The query to be executed</param>
        /// <param name="pageIndex"> The index of the current page</param>
        /// <param name="pageSize">size of result set</param>
        /// <param name="orders">An array of ordering</param>
        /// <returns></returns>
        public static StaticPagedList<TR> ToPagedList<TR, T>(this IQueryOver<T> query, int pageIndex, int pageSize, params Order[] orders)
        {
            try
            {

                if (orders != null)
                {
                    query.AddOrderToQuery(orders);
                }
                var rowCountQuery = query.ToRowCountQuery();
                var totalCount = rowCountQuery.FutureValue<int>();
                var list = query.Take(pageSize).Skip((pageIndex - 1) * pageSize).Future<TR>();
                return new StaticPagedList<TR>(list, pageIndex, pageSize, totalCount.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}