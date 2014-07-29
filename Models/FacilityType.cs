namespace AIMS.Models {

    public partial class FacilityType : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for FacilityType constructor in the schema.
        /// </summary>
        public FacilityType()
        {
            this.Facilities = new Iesi.Collections.Generic.HashedSet<Facility>();
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
        /// There are no comments for Facility in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Facility> Facilities
        {
            get;
            set;
        }
    }
}
