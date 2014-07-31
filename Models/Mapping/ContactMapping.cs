using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping
{
    public class ContactMapping : EntityBaseMapping<Contact>
    {
        public ContactMapping()
        {
			Lazy(true);
            Property(x => x.FirstName, map => map.NotNullable(true));
            Property(x => x.LastName, map => map.NotNullable(true));
            Property(x => x.Title);
            Property(x => x.PhoneNumber, map => map.NotNullable(true));
            Property(x => x.EmailAddress, map => map.NotNullable(true));
            ManyToOne(x => x.Organization, colmap =>
            {
                colmap.Column("OrganizationId");
                colmap.Cascade(Cascade.Persist);
            });
        }
    }
}