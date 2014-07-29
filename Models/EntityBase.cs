using System;

namespace AIMS.Models
{
    public abstract class EntityBase
    {
        public virtual Guid ID { get; protected set; }
        public virtual long Version { get; protected set; }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual DateTime? LastUpdateOn { get; set; }
    }
}