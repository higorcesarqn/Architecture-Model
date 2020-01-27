using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Models;

namespace Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc = false)
        {
            var command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, orderByProperty);
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static IOrderedQueryable<TEntity> OrderByFrom<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc = false)
        {
            return (IOrderedQueryable<TEntity>)source.OrderBy(orderByProperty, desc);
        }

        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderBy<TEntity>(string orderByProperty, bool desc = false) where TEntity : Entity
        {
            IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> x) => x.OrderByFrom(orderByProperty, desc);

            return OrderBy;
        }

        public static IQueryable<T> OrderPaging<T>(this IQueryable<T> OriginalObject, string sortColumn, string sortDirection, int iDisplayStart, int iDisplayLength)
        {
            if (!string.IsNullOrEmpty(sortColumn) && sortColumn.Length > 0)
            {
                var sortList = sortColumn.Split('.');
                if (sortDirection == "desc")
                {
                    OriginalObject = sortList.Length == 2 ? OriginalObject
                            .OrderByDescending(x => x.GetType().GetProperty(sortList[0]).GetValue(x, null) != null ?
                                                    x.GetType().GetProperty(sortList[0]).GetValue(x, null).GetType().GetProperty(sortList[1]).GetValue(x.GetType().GetProperty(sortList[0]).GetValue(x, null), null) :
                                                    true) : OriginalObject
                            .OrderByDescending(x => x.GetType().GetProperty(sortColumn) != null ?
                                                    x.GetType().GetProperty(sortColumn).GetValue(x, null) :
                                                    true);
                }
                else
                {
                    OriginalObject = sortList.Length == 2 ? OriginalObject
                            .OrderBy(x => x.GetType().GetProperty(sortList[0]).GetValue(x, null) != null ?
                                          x.GetType().GetProperty(sortList[0]).GetValue(x, null).GetType().GetProperty(sortList[1]).GetValue(x.GetType().GetProperty(sortList[0]).GetValue(x, null), null) :
                                          true) : OriginalObject
                            .OrderBy(x => x.GetType().GetProperty(sortColumn) != null ?
                                          x.GetType().GetProperty(sortColumn).GetValue(x, null) :
                                          true);
                }
            }

            return OriginalObject.Skip(iDisplayStart).Take(iDisplayLength);
        }

      
    }
}