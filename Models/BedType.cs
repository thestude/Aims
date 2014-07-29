namespace AIMS.Models {

    public partial class BedType : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for BedType constructor in the schema.
        /// </summary>
        public BedType()
        {
            this.Beds = new Iesi.Collections.Generic.HashedSet<Bed>();
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
        /// There are no comments for Bed in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Bed> Beds
        {
            get;
            set;
        }
    }
}
