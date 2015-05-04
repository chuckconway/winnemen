using System.Collections.Generic;

namespace Winnemen.Core.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Reconciles the specified reconcile.
        /// </summary>
        /// <typeparam name="TReconcile">The type of the t reconcile.</typeparam>
        /// <typeparam name="TMaster">The type of the t master.</typeparam>
        /// <param name="reconcile">The reconcile.</param>
        /// <param name="master">The master.</param>
        /// <param name="reconcileId">The reconcile identifier.</param>
        /// <param name="masterId">The master identifier.</param>
        /// <param name="newItem">The new item.</param>
        /// <param name="update">The update.</param>
        public static CollectionReconciliation<TMaster, TReconcile, TId> Reconcile<TReconcile, TMaster, TId>(this List<TReconcile> reconcile)
            where TMaster : class, new()
            where TReconcile : class, new()
            where TId : struct
        {

            var collection = new CollectionReconciliation<TMaster, TReconcile, TId>();
            return collection.Reconcile(reconcile);
        }
    }
}
