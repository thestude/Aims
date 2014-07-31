using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace AIMS.Models.Mapping
{
    public class AimsUserMapping : JoinedSubclassMapping<AimsUser>// : EntityBaseMapping<AimsUser>
    {
        public AimsUserMapping()
        {
            Key(k =>
            {
                k.Column(c =>
                {
                    c.Name("UserId");
                });

                k.ForeignKey("User_fk");
                k.NotNullable(true);
                k.OnDelete(OnDeleteAction.Cascade); // or OnDeleteAction.NoAction
                k.PropertyRef(x => x.FirstName);
                k.Unique(true);
                k.Update(true);
            });

            Property(x => x.FirstName, pm =>
            {
                pm.Column(col =>
                {
                    col.Name("FirstName");
                    col.Length(100);
                    col.NotNullable(false);
                });
                pm.Index("firstname_idx");
                pm.Update(true);
                pm.Insert(true);
            });

            Property(x => x.LastName, pm =>
            {
                pm.Column(col =>
                {
                    col.Name("LastName");
                    col.Length(100);
                    col.NotNullable(false);
                });
                pm.Index("lastname_idx");
                pm.Update(true);
                pm.Insert(true);
            });

            Set(x => x.Organizations, collectionMapping =>
            {
                collectionMapping.Table("OrganizationUsers");
                collectionMapping.Key(k =>
                {
                    k.Column("UserId");
                    k.ForeignKey("user_organization_fk");
                });
                collectionMapping.Cascade(Cascade.Persist);
            }, map => map.ManyToMany(p => p.Column("OrganizationId")));
        }
    }
}