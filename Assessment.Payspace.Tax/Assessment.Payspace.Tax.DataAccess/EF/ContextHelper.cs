using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Assessment.Payspace.Tax.DataAccess.EF
{
    internal static class ContextHelper
    {
        public static List<T> ToListReadUncommitted<T>(this IQueryable<T> query)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }))
            {
                List<T> toReturn = query.ToList();
                scope.Complete();
                return toReturn;
            }
        }

        public static T ReadUncommitted<T>(this IQueryable<T> query)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.RequiresNew,
                new TransactionOptions()
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                }))
            {
                T toReturn = query.FirstOrDefault();
                scope.Complete();
                return toReturn;
            }
        }
    }
}
