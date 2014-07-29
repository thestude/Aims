using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class StaffMapping : EntityBaseMapping<Staff>
    {
        
        public StaffMapping() {
			Lazy(true);
			Property(x => x.Status, map => map.NotNullable(true));
			Property(x => x.AmountShort, map => map.NotNullable(true));
			Property(x => x.Notes, map => map.NotNullable(true));
            ManyToOne(x => x.StaffType, colmap =>
            {
                colmap.Column("StaffTypeId");
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
