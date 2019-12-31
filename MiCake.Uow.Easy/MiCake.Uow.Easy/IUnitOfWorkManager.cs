using System;
using System.Collections.Generic;
using System.Text;

namespace MiCake.Uow.Easy
{
    /// <summary>
    /// 工作单元管理类，用于维护和创建工作单元
    /// </summary>
    public interface IUnitOfWorkManager : IUnitOfWokrProvider, IDisposable
    {
        /// <summary>
        /// Create a <see cref="IUnitOfWork"/> with a default options
        /// </summary>
        IUnitOfWork Create();

        /// <summary>
        ///  Create a <see cref="IUnitOfWork"/> with a custom options
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IUnitOfWork Create(UnitOfWorkOptions options);
    }
}
