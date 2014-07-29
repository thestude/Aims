namespace AIMS.Models {

    public partial class Staff : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for Staff constructor in the schema.
        /// </summary>
        public Staff()
        {
            OnCreated();
        }

        /// <summary>
        /// There are no comments for Status in the schema.
        /// </summary>
        public virtual bool Status
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for AmountShort in the schema.
        /// </summary>
        public virtual int AmountShort
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
        /// There are no comments for StaffTypes in the schema.
        /// </summary>
        public virtual StaffType StaffType
        {
            get;
            set;
        }
    }
}
