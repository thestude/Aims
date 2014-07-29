//using FluentNHibernate.Conventions;
//using FluentNHibernate.Conventions.AcceptanceCriteria;
//using FluentNHibernate.Conventions.Inspections;
//using FluentNHibernate.Conventions.Instances;

namespace AIMS.Infrastructure.NHibernate
{
    /// <summary>
    /// Convert all string properties to AnsiString (varchar). This does not work with SQL CE.
    /// </summary>
    //public class AnsiStringConvention : IPropertyConventionAcceptance, IPropertyConvention
    //{
        //public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria)
        //{
        //    criteria.Expect(x => x.Property.PropertyType == typeof(string));
        //}

        //public void Apply(IPropertyInstance instance)
        //{
        //    instance.CustomType("AnsiString");
        //}

    //}
}