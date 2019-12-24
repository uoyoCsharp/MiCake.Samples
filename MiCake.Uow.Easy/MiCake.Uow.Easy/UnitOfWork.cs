using MiCake.Core.Abstractions;
using MiCake.Core.Util.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiCake.Uow.Easy
{
    public class UnitOfWork : IUnitOfWork
    {
        public Guid ID => Guid.NewGuid();

        public bool IsDisposed => _isDisposed;

        /// <summary>
        /// 具有事务功能的数据访问容器。比如在EF里面，具有事务操纵的应该是DbContext
        /// </summary>
        private Dictionary<string, ITransactionFeature> _transcationContainer;

        private bool _isRollback = false;
        private bool _isCommit = false;
        private bool _isDisposed = false;

        public UnitOfWork()
        {
            _transcationContainer = new Dictionary<string, ITransactionFeature>();
        }

        public void Rollback()
        {
            if (_isRollback)
                throw new MiCakeException("Can not rollback in this unitofwork");

            _isRollback = true;

            foreach (var TransactionFeature in _transcationContainer)
            {
                TransactionFeature.Value.Rollback();
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_isRollback)
                throw new MiCakeException("Can not rollback in this unitofwork");

            _isRollback = true;

            foreach (var TransactionFeature in _transcationContainer)
            {
                await TransactionFeature.Value.RollbackAsync();
            }
        }

        public void SaveChanges()
        {
            if (_isCommit)
                throw new MiCakeException("Can not commit in this unitofwork");

            _isCommit = true;

            foreach (var TransactionFeature in _transcationContainer)
            {
                TransactionFeature.Value.Commit();
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (_isCommit)
                throw new MiCakeException("Can not commit in this unitofwork");

            _isCommit = true;

            foreach (var TransactionFeature in _transcationContainer)
            {
                await TransactionFeature.Value.CommitAsync();
            }
        }

        public void ResigtedTransactionFeature(string key, ITransactionFeature TransactionFeature)
        {
            if (!_transcationContainer.ContainsKey(key))
                _transcationContainer.Add(key, TransactionFeature);
        }

        public ITransactionFeature GetOrAddTransactionFeature(string key, ITransactionFeature TransactionFeature)
        {
            if (_transcationContainer.ContainsKey(key))
                return _transcationContainer.GetValueOrDefault(key);

            _transcationContainer.Add(key, TransactionFeature);
            return TransactionFeature;
        }

        public ITransactionFeature GetTransactionFeature(string key)
        {
            ITransactionFeature result = null;
            if (_transcationContainer.TryGetValue(key, out result))
            {
                return result;
            }

            return default;
        }

        public void RemoveTranscation(string key)
        {
            _transcationContainer.Remove(key);
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            try
            {
                foreach (var TransactionFeature in _transcationContainer)
                {
                    TransactionFeature.Value.Dispose();
                }
            }
            catch
            {
                //   throw 
            }
        }
    }
}
