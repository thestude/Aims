using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AIMS.Models
{

    /// <summary>
    /// There are no comments for SystemsType in the schema.
    /// </summary>
    public partial class SystemsType : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for SystemsType constructor in the schema.
        /// </summary>
        public SystemsType()
        {
            this.Systems = new Iesi.Collections.Generic.HashedSet<Systems>();
            OnCreated();
        }
        
        /// <summary>
        /// There are no comments for Name in the schema.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Systems in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Systems> Systems
        {
            get;
            set;
        }
    }

}
