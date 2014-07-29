namespace AIMS.Models {

    public partial class Bed : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for Bed constructor in the schema.
        /// </summary>
        public Bed()
        {
            OnCreated();
        }

        /// <summary>
        /// There are no comments for StandardCapacity in the schema.
        /// </summary>
        public virtual int StandardCapacity
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for CurrentCapacity in the schema.
        /// </summary>
        public virtual int CurrentCapacity
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for InUse in the schema.
        /// </summary>
        public virtual int InUse
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Available in the schema.
        /// </summary>
        public virtual int Available
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
        /// There are no comments for BedTypes in the schema.
        /// </summary>
        public virtual BedType BedType
        {
            get;
            set;
        }
    }
}
