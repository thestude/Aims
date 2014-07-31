namespace AIMS.Models
{
    public partial class Contact : EntityBase
    {
        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion

        public Contact()
        {
            OnCreated();
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Title { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual Organization Organization { get; set; }
 
    }
}