namespace AIMS.Models {

    public partial class Facility: Organization
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for Facility constructor in the schema.
        /// </summary>
        public Facility()
        {
            this.Beds = new Iesi.Collections.Generic.HashedSet<Bed>();
            this.Staffs = new Iesi.Collections.Generic.HashedSet<Staff>();
            this.Systems = new Iesi.Collections.Generic.HashedSet<Systems>();
            this.Fuels = new Iesi.Collections.Generic.HashedSet<Fuel>();
            this.Capacities = new Iesi.Collections.Generic.HashedSet<Capacity>();
            this.FacilityStatuses = new Iesi.Collections.Generic.HashedSet<FacilityStatus>();
            OnCreated();
        }

        ///// <summary>
        ///// There are no comments for OnGenerator in the schema.
        ///// </summary>
        //public virtual bool OnGenerator
        //{
        //    get;
        //    set;
        //}


        ///// <summary>
        ///// There are no comments for Status in the schema.
        ///// </summary>
        //public virtual string Status
        //{
        //    get;
        //    set;
        //}


        ///// <summary>
        ///// There are no comments for ProjectedIBA in the schema.
        ///// </summary>
        //public virtual System.Nullable<int> ProjectedIBA
        //{
        //    get;
        //    set;
        //}


        ///// <summary>
        ///// There are no comments for Notes in the schema.
        ///// </summary>
        //public virtual string Notes
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// There are no comments for Organization in the schema.
        /// </summary>
        public virtual Organization Organization
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for FacilityTypes in the schema.
        /// </summary>
        public virtual FacilityType FacilityType
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Beds in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Bed> Beds
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Staffs in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Staff> Staffs
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


        /// <summary>
        /// There are no comments for Fuels in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Fuel> Fuels
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Capacities in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Capacity> Capacities
        {
            get;
            set;
        }

        
        public virtual Iesi.Collections.Generic.ISet<FacilityStatus> FacilityStatuses
        {
            get;
            set;
        }
    }
}
