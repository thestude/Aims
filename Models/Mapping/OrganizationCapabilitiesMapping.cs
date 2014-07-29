using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class OrganizationCapabilitiesMapping : EntityBaseMapping<OrganizationCapabilities>
    {
        
        public OrganizationCapabilitiesMapping() {
			Lazy(true);
			Property(x => x.CapabilitiesList, map => map.NotNullable(true));
            Set(x => x.Organizations, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("OrganizationCapabilitiesId");
                    x.ForeignKey("organizationcapabilities_organization_fk");
                }); 
                colmap.Inverse(true);
                colmap.Cascade(Cascade.Persist);
            }, map =>
            {
                map.OneToMany();
            });
        }
    }
}
