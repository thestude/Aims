using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {
    
    
    public class CapacityMapping : EntityBaseMapping<Capacity> {
        
        public CapacityMapping() {
			Lazy(true);
			Property(x => x.TotalCapacity, map => map.NotNullable(true));
            ManyToOne(x => x.Facility, colmap =>
            {
                colmap.Column("FacilityId");
                colmap.Cascade(Cascade.Persist);
            });
        }
    }
}
