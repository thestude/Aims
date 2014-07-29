namespace AIMS.Models {

    public partial class StaffType : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for StaffType constructor in the schema.
        /// </summary>
        public StaffType()
        {
            this.Staffs = new Iesi.Collections.Generic.HashedSet<Staff>();
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
        /// There are no comments for Staff in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Staff> Staffs
        {
            get;
            set;
        }
    }
}
