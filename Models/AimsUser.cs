using AIMS.Search;
using BrockAllen.MembershipReboot.Nh;
using Rhino.Security;

namespace AIMS.Models
{
    [FullTextIndexed("Username,Email,MobilePhoneNumber")]
    public class AimsUser : NhUserAccount, IUser
    {
        public AimsUser()
        {
            this.Organizations = new Iesi.Collections.Generic.HashedSet<Organization>();
        }

        [FullTextIndexed]
        public virtual string FirstName { get; set; }

        [FullTextIndexed]
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the security info for this user
        /// </summary>
        /// <value>The security info.</value>
        public virtual SecurityInfo SecurityInfo
        {
            get { return new SecurityInfo(FirstName, ID); }
        }

        public virtual Iesi.Collections.Generic.ISet<Organization> Organizations
        {
            get;
            set;
        }
    }
}