using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class BedMapping : EntityBaseMapping<Bed>
    {
        
        public BedMapping() {
			Lazy(true);
			Property(x => x.StandardCapacity, map => map.NotNullable(true));
			Property(x => x.CurrentCapacity, map => map.NotNullable(true));
			Property(x => x.InUse, map => map.NotNullable(true));
			Property(x => x.Available, map => map.NotNullable(true));
			Property(x => x.Notes);
            ManyToOne(x => x.BedType, colmap =>
            {
                colmap.Column("BedTypeId");
                colmap.Cascade(Cascade.Persist);
            });
            ManyToOne(x => x.Facility, colmap =>
            {
                colmap.Column("FacilityId");
                colmap.Cascade(Cascade.Persist);
            });
        }
    }
}
