using System;

namespace AIMS.Models {

    public partial class Organization : EntityBase
    {

        #region Extensibility Method Definitions

        /// <summary>
        /// There are no comments for OnCreated in the schema.
        /// </summary>
        partial void OnCreated();

        #endregion
        /// <summary>
        /// There are no comments for Organization constructor in the schema.
        /// </summary>
        public Organization()
        {
            this.ChildOrganizations = new Iesi.Collections.Generic.HashedSet<Organization>();
            this.Users = new Iesi.Collections.Generic.HashedSet<AimsUser>();
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
        /// There are no comments for AddressLine1 in the schema.
        /// </summary>
        public virtual string AddressLine1
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for AddressLine2 in the schema.
        /// </summary>
        public virtual string AddressLine2
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Latitude in the schema.
        /// </summary>
        public virtual double? Latitude
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Longitude in the schema.
        /// </summary>
        public virtual double? Longitude
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Elevation in the schema.
        /// </summary>
        public virtual double? Elevation
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Phone in the schema.
        /// </summary>
        public virtual string Phone
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Acronym in the schema.
        /// </summary>
        public virtual string Acronym
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for OrganizationPreferences in the schema.
        /// </summary>
        public virtual string OrganizationPreferences
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for OrganizationAssociationId in the schema.
        /// </summary>
        public virtual string OrganizationAssociationId
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for City in the schema.
        /// </summary>
        public virtual string City
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for ZIP in the schema.
        /// </summary>
        public virtual string ZipCode
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for OrganizationType in the schema.
        /// </summary>
        public virtual OrganizationType OrganizationType
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for County in the schema.
        /// </summary>
        public virtual County County
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Organizations in the schema.
        /// </summary>
        public virtual Iesi.Collections.Generic.ISet<Organization> ChildOrganizations
        {
            get;
            set;
        }


        /// <summary>
        /// There are no comments for Organization1 in the schema.
        /// </summary>
        public virtual Organization ParentOrganization
        {
            get;
            set;
        }

        public virtual string Capabilities
        {
            get;
            set;
        }

        public virtual Iesi.Collections.Generic.ISet<AimsUser> Users
        {
            get;
            set;
        }

        public virtual Iesi.Collections.Generic.ISet<Contact> Contacts
        {
            get;
            set;
        }

    }
}
