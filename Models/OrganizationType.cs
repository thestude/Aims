namespace AIMS.Models {

    public partial class OrganizationType : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for OrganizationType constructor in the schema.
        /// </summary>
        public OrganizationType()
        {
            this.Organizations = new Iesi.Collections.Generic.HashedSet<Organization>();
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
        /// There are no comments for Organizations in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Organization> Organizations
        {
            get;
            set;
        }
    }
}
