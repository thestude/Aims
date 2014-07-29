namespace AIMS.Models {

    public partial class County : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for County constructor in the schema.
        /// </summary>
        public County()
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


        /// <summary>
        /// There are no comments for State in the schema.
        /// </summary>
        public virtual State State
        {
            get;
            set;
        }
    }
}
