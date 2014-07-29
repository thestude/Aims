using NHibernate.Mapping.ByCode;

namespace AIMS.Models.Mapping {

    public class StaffTypeMapping : EntityBaseMapping<StaffType>
    {
        
        public StaffTypeMapping() {
			Lazy(true);
			Property(x => x.Name, map => map.NotNullable(true));
            Set(x => x.Staffs, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("StaffTypeId");
                    x.ForeignKey("stafftype_staff_fk");
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
