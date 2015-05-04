using System;
using System.Collections.Generic;
using System.Linq;

namespace Winnemen.Core
{
    public class CollectionReconciliation<TMaster, TReconcile, TId>
        where TMaster : class, new()
        where TReconcile : class, new()
        where TId : struct
    {

        private List<TMaster> _master;
        private List<TReconcile> _reconcile;
        private Func<TMaster, TId> _masterId;
        private Func<TReconcile, TId> _reconcileId;

        private Func<TMaster, TReconcile> _add;

        private Action<TMaster, TReconcile> _update;

        private Action<TReconcile> _delete = s => { };

        /// <summary>
        /// Sets the master identifier.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <returns>CollectionReconciliation&lt;TMaster, TReconcile, TId&gt;.</returns>
        public CollectionReconciliation<TMaster, TReconcile, TId> Add(Func<TMaster, TReconcile> add)
        {
            _add = add;
            return this;
        }

        /// <summary>
        /// Adds the specified update.
        /// </summary>
        /// <param name="update">The update.</param>
        /// <returns>CollectionReconciliation&lt;TMaster, TReconcile, TId&gt;.</returns>
        public CollectionReconciliation<TMaster, TReconcile, TId> Update(Action<TMaster, TReconcile> update)
        {
            _update = update;
            return this;
        }

        /// <summary>
        /// Adds the specified update.
        /// </summary>
        /// <param name="update">The update.</param>
        /// <returns>CollectionReconciliation&lt;TMaster, TReconcile, TId&gt;.</returns>
        public CollectionReconciliation<TMaster, TReconcile, TId> Delete(Action<TReconcile> delete)
        {
            _delete = delete;
            return this;
        }


        /// <summary>
        /// Sets the master identifier.
        /// </summary>
        /// <param name="masterId">The master identifier.</param>
        /// <returns>CollectionReconciliation&lt;TMaster, TReconcile, TId&gt;.</returns>
        public CollectionReconciliation<TMaster, TReconcile, TId> MasterId(Func<TMaster, TId> masterId)
        {
            _masterId = masterId;
            return this;
        }

        /// <summary>
        /// Sets the master identifier.
        /// </summary>
        /// <param name="reconcileId">The reconcile identifier.</param>
        /// <returns>CollectionReconciliation&lt;TMaster, TReconcile, TId&gt;.</returns>
        public CollectionReconciliation<TMaster, TReconcile, TId> ReconcileId(Func<TReconcile, TId> reconcileId)
        {
            _reconcileId = reconcileId;
            return this;
        }

        /// <summary>
        /// Sets the master identifier.
        /// </summary>
        /// <param name="master">The master.</param>
        /// <returns>CollectionReconciliation&lt;TMaster, TReconcile, TId&gt;.</returns>
        public CollectionReconciliation<TMaster, TReconcile, TId> Master(List<TMaster> master)
        {
            _master = master;
            return this;
        }

        /// <summary>
        /// Sets the master identifier.
        /// </summary>
        /// <param name="reconcile">The reconcile.</param>
        /// <returns>CollectionReconciliation&lt;TMaster, TReconcile, TId&gt;.</returns>
        public CollectionReconciliation<TMaster, TReconcile, TId> Reconcile(List<TReconcile> reconcile)
        {
            _reconcile = reconcile;
            return this;
        }


        /// <summary>
        /// Reconciles the two collections.
        /// </summary>
        /// <returns>TApprentice.</returns>
        public List<TReconcile> Reconcile()
        {
            foreach (var address in _master)
            {
                if (address != null)
                {
                    bool isNew = EqualityComparer<TId>.Default.Equals(_masterId(address), default(TId));

                    //Insert, New Address
                    if (isNew)
                    {
                        var item = _add(address);
                        _reconcile.Add(item);
                    }
                    else
                    {
                        var address1 = address;
                        foreach (var apprentice in _reconcile.Where(a => EqualityComparer<TId>.Default.Equals(_reconcileId(a), _masterId(address1))))
                        {
                            _update(address, apprentice);
                        }
                    }
                }
            }

            //Remove deleted rows
            var currentIds = _reconcile.Select(_reconcileId);
            var masterIds = _master.Select(_masterId);

            var removedIds = currentIds.Except(masterIds);

            //Get items that need to be removed
            var tempCollection = removedIds
                .Select(id => _reconcile.Where(s => s != null).SingleOrDefault(s => EqualityComparer<TId>.Default.Equals(_reconcileId(s), id)))
                .ToList();


            foreach (var reconcile in tempCollection)
            {
                _delete(reconcile);
                _reconcile.Remove(reconcile);
            }

            return _reconcile;
        }

    }
}
