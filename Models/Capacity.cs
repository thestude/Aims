namespace AIMS.Models {

    public partial class Capacity : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for Capacity constructor in the schema.
        /// </summary>
        public Capacity()
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
        /// There are no comments for Facility in the schema.
        /// </summary>
        public virtual Facility Facility
        {
            get;
            set;
        }
    }
}
