namespace AIMS.Models
{

    public partial class FacilityStatus : EntityBase
    {
        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        
        /// <summary>
        /// There are no comments for FacilityStatus constructor in the schema.
        /// </summary>
        public FacilityStatus()
        {
            OnCreated();
        }

        /// <summary>
        /// There are no comments for OnGenerator in the schema.
        /// </summary>
        public virtual bool OnGenerator
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
        /// There are no comments for ProjectedIBA in the schema.
        /// </summary>
        public virtual System.Nullable<int> ProjectedIba
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

        
    }
}
