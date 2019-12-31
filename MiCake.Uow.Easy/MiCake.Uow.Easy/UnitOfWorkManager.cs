using MiCake.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiCake.Uow.Easy
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private IUnitOfWork currentUow;
        private bool _isDisposed = false;

        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkManager(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork Create()
        {
            return Create(new UnitOfWorkOptions());
        }

        public IUnitOfWork Create(UnitOfWorkOptions options)
        {
            currentUow = CreateNewUnitOfWork(options);

            return currentUow;
        }

        public IUnitOfWork GetCurrentUnitOfWork()
        {
            return currentUow;
        }

        //Create a new unitofwork 
        private IUnitOfWork CreateNewUnitOfWork(UnitOfWorkOptions options)
        {
            IUnitOfWork result;

            var uowScope = _serviceProvider.CreateScope();

            try
            {
                result = uowScope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                if (options != null)
                    result.SetOptions(options);

                result.DisposeHandler += (sender, args) =>
                {
                    uowScope.Dispose();
                    currentUow = null;
                };
            }
            catch (Exception ex)
            {
                uowScope.Dispose();
                throw ex;
            }

            return result;
        }

        public void Dispose()
        {
            if (_isDisposed)
                throw new MiCakeException("this manager is already disposed");

            _isDisposed = true;

            currentUow?.Dispose();
        }


    }
}
