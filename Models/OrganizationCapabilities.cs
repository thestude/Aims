namespace AIMS.Models {

    public partial class OrganizationCapabilities : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for OrganizationCapabilities constructor in the schema.
        /// </summary>
        public OrganizationCapabilities()
        {
            this.Organizations = new Iesi.Collections.Generic.HashedSet<Organization>();
            OnCreated();
        }


        /// <summary>
        /// There are no comments for CapabilitiesList in the schema.
        /// </summary>
        public virtual string CapabilitiesList
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Organizations in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Organization> Organizations
        {
            get;
            set;
        }
    }
}
