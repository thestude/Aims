using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class SystemsTypeMapping : EntityBaseMapping<SystemsType>
    {
        
        public SystemsTypeMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
            Set(x => x.Systems, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("SystemsTypeId");
                    x.ForeignKey("systemstype_systems_fk");
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
