using NHibernate.Mapping.ByCode.Conformist;


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
        }
    }
}
