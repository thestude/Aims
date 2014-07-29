using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class SystemsMapping : EntityBaseMapping<Systems>
    {
        
        public SystemsMapping() {
			Lazy(true);
			Property(x => x.Status, map => map.NotNullable(true));
			Property(x => x.Notes);
            ManyToOne(x => x.SystemsType, colmap =>
            {
                colmap.Column("SystemsTypeId");
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
