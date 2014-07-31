using NHibernate.Mapping.ByCode;


namespace AIMS.Models.Mapping
{


    public class FacilityStatusMapping : EntityBaseMapping<FacilityStatus>
    {

        public FacilityStatusMapping()
        {
            Lazy(true);
            Property(x => x.OnGenerator, map => map.NotNullable(true));
            Property(x => x.Status, map => map.NotNullable(true));
            Property(x => x.ProjectedIba);
            Property(x => x.Notes);
            ManyToOne(x => x.Facility, colmap =>
            {
                colmap.Column("FacilityId");
                colmap.Cascade(Cascade.Persist);
            });
        }
    }
}
