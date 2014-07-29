using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class StateMapping : EntityBaseMapping<State>
    {
        
        public StateMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
			Set(x => x.Counties, colmap =>
			{
			    colmap.Key(x =>
			    {
			        x.Column("StateId");
                    x.ForeignKey("state_county_fk");
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
