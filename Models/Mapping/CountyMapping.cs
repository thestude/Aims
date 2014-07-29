using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class CountyMapping : EntityBaseMapping<County>
    {
        
        public CountyMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
            ManyToOne(x => x.State, colmap =>
            {
                colmap.Column("StateId");
                colmap.Cascade(Cascade.Persist);
            });
			Set(x => x.Organizations, colmap =>
			{
			    colmap.Key(x =>
			    {
			        x.Column("CountyId");
                    x.ForeignKey("county_organization_fk");
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
