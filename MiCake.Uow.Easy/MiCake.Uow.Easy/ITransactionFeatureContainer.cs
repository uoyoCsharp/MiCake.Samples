using System;
using System.Collections.Generic;
using System.Text;

namespace MiCake.Uow.Easy
{
    public interface ITransactionFeatureContainer
    {
        void RegisteTranasctionFeature(string key, ITransactionFeature TransactionFeature);

        ITransactionFeature GetOrAddTransactionFeature(string key, ITransactionFeature TransactionFeature);

        ITransactionFeature GetTransactionFeature(string key);

        void RemoveTransaction(string key);
    }
}
