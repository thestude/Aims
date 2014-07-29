using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {


    public class FuelTypeMapping : EntityBaseMapping<FuelType>
    {
        
        public FuelTypeMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
            Set(x => x.Fuels, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("FuelTypeId");
                    x.ForeignKey("fueltype_fuel_fk");
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
