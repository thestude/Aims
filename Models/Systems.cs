using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.Generic;


namespace AIMS.Models {

    public partial class Systems : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for Systems constructor in the schema.
        /// </summary>
        public Systems()
        {
            OnCreated();
        }

        /// <summary>
        /// There are no comments for Status in the schema.
        /// </summary>
        public virtual string Status
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Notes in the schema.
        /// </summary>
        public virtual string Notes
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Facility in the schema.
        /// </summary>
        public virtual Facility Facility
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for SystemsTypes in the schema.
        /// </summary>
        public virtual SystemsType SystemsType
        {
            get;
            set;
        }
    }

}
