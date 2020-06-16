using MiCake.Audit;
using System;

namespace MiCakeDemoApplication.Audit
{
    public class HasAuditInfo : IHasAudit
    {
        public DateTime CreationTime { get; set; }
        public DateTime? ModificationTime { get; set; }
    }
}
