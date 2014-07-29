using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class OrganizationTypeMapping : EntityBaseMapping<OrganizationType>
    {
        
        public OrganizationTypeMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
			Set(x => x.Organizations, colmap =>
			{
			    colmap.Key(x =>
			    {
			        x.Column("OrganizationTypeId");
                    x.ForeignKey("organizationtype_organization_fk");
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
