using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class FuelMapping : EntityBaseMapping<Fuel>
    {
        
        public FuelMapping() {
			Lazy(true);
			Property(x => x.TotalCapacity, map => map.NotNullable(true));
			Property(x => x.Status, map => map.NotNullable(true));
			Property(x => x.AmountShort, map => map.NotNullable(true));
			Property(x => x.Measurement);
			Property(x => x.Notes);
            ManyToOne(x => x.FuelType, colmap =>
            {
                colmap.Column("FuelTypeId");
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
