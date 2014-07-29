using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;


namespace AIMS.Models.Mapping {


    public class FacilityMapping : JoinedSubclassMapping<Facility>
    {
        
        public FacilityMapping() {
			Lazy(true);
            Key(k =>
            {
                k.Column(c =>
                {
                    c.Name("OrganizationId");
                });

                k.ForeignKey("organization_facility_fk");
                k.NotNullable(true);
                k.OnDelete(OnDeleteAction.Cascade); // or OnDeleteAction.NoAction
                k.Unique(true);
                k.Update(true);
            });
            //Property(x => x.OnGenerator, map => map.NotNullable(true));
            //Property(x => x.Status, map => map.NotNullable(true));
            //Property(x => x.ProjectedIBA);
            //Property(x => x.Notes);
            ManyToOne(x => x.FacilityType, colmap =>
            {
                colmap.Column("FacilityTypeId");
                colmap.Cascade(Cascade.Persist);
            });
            Set(x => x.Beds, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("FacilityId");
                    x.ForeignKey("facility_bed_fk");
                });
                colmap.Inverse(true);
                colmap.Cascade(Cascade.Persist);
            }, map =>
            {
                map.OneToMany();

            });
            Set(x => x.Capacities, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("FacilityId");
                    x.ForeignKey("facility_capacity_fk");
                });
                colmap.Inverse(true);
                colmap.Cascade(Cascade.Persist);
            }, map =>
            {
                map.OneToMany();
            });
            Set(x => x.Fuels, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("FacilityId");
                    x.ForeignKey("facility_fuel_fk");
                });
                colmap.Inverse(true);
                colmap.Cascade(Cascade.Persist);
            }, map =>
            {
                map.OneToMany();
            });
            Set(x => x.Staffs, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("FacilityId");
                    x.ForeignKey("facility_staff_fk");
                });
                colmap.Inverse(true);
                colmap.Cascade(Cascade.Persist);
            }, map =>
            {
                map.OneToMany();
            });
            Set(x => x.Systems, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("FacilityId");
                    x.ForeignKey("facility_systems_fk");
                });
                colmap.Inverse(true);
                colmap.Cascade(Cascade.Persist);
            }, map =>
            {
                map.OneToMany();
            });
            Set(x => x.FacilityStatuses, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("FacilityId");
                    x.ForeignKey("facility_status_fk");
                });
                colmap.Inverse(true);
            }, map =>
            {
                map.OneToMany();
            });
        }
    }
}
