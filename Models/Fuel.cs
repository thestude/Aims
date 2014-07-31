namespace AIMS.Models {

    public partial class Fuel : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for Fuel constructor in the schema.
        /// </summary>
        public Fuel()
        {
            OnCreated();
        }

        /// <summary>
        /// There are no comments for TotalCapacity in the schema.
        /// </summary>
        public virtual int TotalCapacity
        {
            get;
            set;
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
        /// There are no comments for AmountShort in the schema.
        /// </summary>
        public virtual int AmountShort
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Measurement in the schema.
        /// </summary>
        public virtual string Measurement
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
        /// There are no comments for FuelTypes in the schema.
        /// </summary>
        public virtual FuelType FuelType
        {
            get;
            set;
        }
    }
}
