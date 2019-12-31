using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MiCake.Uow.Easy
{
    public class UnitOfWorkOptions
    {
        public IsolationLevel? IsolationLevel { get; set; }

        public TimeSpan? Timeout { get; set; }

        public UnitOfWorkOptions() : this(default)
        {
        }

        public UnitOfWorkOptions(IsolationLevel? isolationLevel) :
            this(isolationLevel, null)
        {
        }

        public UnitOfWorkOptions(IsolationLevel? isolationLevel, TimeSpan? timeOut)
        {
            IsolationLevel = isolationLevel;
            Timeout = timeOut;
        }

        public UnitOfWorkOptions Clone()
        {
            return new UnitOfWorkOptions(IsolationLevel, Timeout);
        }
    }
}
