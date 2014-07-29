namespace AIMS.Models {

    public partial class FuelType : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for FuelType constructor in the schema.
        /// </summary>
        public FuelType()
        {
            this.Fuels = new Iesi.Collections.Generic.HashedSet<Fuel>();
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
        /// There are no comments for Fuel in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Fuel> Fuels
        {
            get;
            set;
        }
    }
}
