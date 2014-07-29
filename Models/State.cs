namespace AIMS.Models {

    public partial class State : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for State constructor in the schema.
        /// </summary>
        public State()
        {
            this.Counties = new Iesi.Collections.Generic.HashedSet<County>();
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
        /// There are no comments for Counties in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<County> Counties
        {
            get;
            set;
        }
    }
}
