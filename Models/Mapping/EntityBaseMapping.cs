using System;
using NHibernate.Type;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AIMS.Models.Mapping
{
    public abstract class EntityBaseMapping<T> : ClassMapping<T> where T : EntityBase
    {
        public EntityBaseMapping()
        {
            Id(x => x.ID, idm => idm.Generator(Generators.GuidComb));

            Version(
                x => x.Version,
                vm =>
                {
                    vm.Generated(VersionGeneration.Never);
                    vm.Type(new Int64Type());
                });
            Property(x => x.CreatedOn, map =>
            {
                map.Column(col =>
                {
                    col.SqlType("timestamptz");
                    col.Default("(now() at time zone 'utc')");
                });
            });
            Property(x => x.LastUpdateOn, map =>
            {
                map.Column(col =>
                {
                    col.SqlType("timestamptz");
                    col.Default("(now() at time zone 'utc')");
                });
            });

            DynamicInsert(true);
            DynamicUpdate(true);

        }
    }
}