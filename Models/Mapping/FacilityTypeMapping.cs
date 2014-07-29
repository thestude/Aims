using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class FacilityTypeMapping : EntityBaseMapping<FacilityType>
    {
        
        public FacilityTypeMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
            Set(x => x.Facilities, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("FacilityTypeId");
                    x.ForeignKey("facilitytype_facility_fk");
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
