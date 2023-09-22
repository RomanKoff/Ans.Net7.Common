using System.Linq.Expressions;

namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
         * IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property);
         * IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property);
         * IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property);
         * IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property);
         * Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2);
         * Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2);
         */


        public static IOrderedQueryable<T> OrderBy<T>(
            this IQueryable<T> source,
            string property)
        {
            return _applyOrder<T>(source, property, "OrderBy");
        }


        public static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> source,
            string property)
        {
            return _applyOrder<T>(source, property, "OrderByDescending");
        }


        public static IOrderedQueryable<T> ThenBy<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return _applyOrder<T>(source, property, "ThenBy");
        }


        public static IOrderedQueryable<T> ThenByDescending<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return _applyOrder<T>(source, property, "ThenByDescending");
        }


        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var e1 = Expression.Invoke(
                expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(expr1.Body, e1), expr1.Parameters);
        }


        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var e1 = Expression.Invoke(
                expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(expr1.Body, e1), expr1.Parameters);
        }


        /* privates */


        private static IOrderedQueryable<T> _applyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            Type t1 = typeof(T);
            var props1 = property.Split('.');
            var arg1 = Expression.Parameter(t1, "x");
            Expression e1 = arg1;
            foreach (var prop1 in props1)
            {
                var pi1 = t1.GetProperty(prop1);
                e1 = Expression.Property(e1, pi1);
                t1 = pi1.PropertyType;
            }
            Type t2 = typeof(Func<,>).MakeGenericType(typeof(T), t1);
            var lambda1 = Expression.Lambda(t2, e1, arg1);
            object result1 = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                    && method.IsGenericMethodDefinition
                    && method.GetGenericArguments().Length == 2
                    && method.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), t1)
                        .Invoke(null, new object[] { source, lambda1 });
            return (IOrderedQueryable<T>)result1;
        }

    }

}
