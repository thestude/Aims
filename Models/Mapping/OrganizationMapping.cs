using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class OrganizationMapping : EntityBaseMapping<Organization>
    {
        
        public OrganizationMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
			Property(x => x.AddressLine1, map => map.NotNullable(true));
			Property(x => x.AddressLine2);
			Property(x => x.Latitude);
			Property(x => x.Longitude);
			Property(x => x.Elevation);
			Property(x => x.Phone);
			Property(x => x.Acronym, map => map.NotNullable(true));
			Property(x => x.OrganizationPreferences);
			Property(x => x.OrganizationAssociationId);
			Property(x => x.City, map => map.NotNullable(true));
			Property(x => x.ZipCode, map => map.NotNullable(true));
            ManyToOne(x => x.OrganizationType, colmap =>
            {
                colmap.Column("OrganizationTypeId");
                colmap.Cascade(Cascade.Persist);
            });
            ManyToOne(x => x.County, colmap =>
            {
                colmap.Column("CountyId");
                colmap.Cascade(Cascade.Persist);
            });
            ManyToOne(x => x.ParentOrganization, colmap =>
            {
                colmap.Column("ParentOrganizationId");
                colmap.Cascade(Cascade.Persist);
            });
            Set(x => x.ChildOrganizations, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("ParentOrganizationId");
                    x.ForeignKey("organization_childorganization_fk");
                }); 
                colmap.Inverse(true);
                colmap.Cascade(Cascade.Persist);
            }, map =>
            {
                map.OneToMany();
            });

            Set(x => x.Users, collectionMapping =>
            {
                collectionMapping.Table("OrganizationUsers");
                collectionMapping.Key(k => k.Column("OrganizationId"));
                collectionMapping.Cascade(Cascade.Persist);
            }, map => map.ManyToMany(p => p.Column("UserId")));
        }
    }
}
