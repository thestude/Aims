using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {

    public class BedTypeMapping : EntityBaseMapping<BedType>
    {
        
        public BedTypeMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
            Set(x => x.Beds,colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("BedTypeId");
                    x.ForeignKey("bedtype_bed_fk");
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
