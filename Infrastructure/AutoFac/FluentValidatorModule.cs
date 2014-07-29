using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using Module = Autofac.Module;

namespace AIMS.Infrastructure.AutoFac
{
    public class FluentValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ToList()
                .ForEach(item => builder.RegisterType(item.ValidatorType).Keyed<IValidator>(item.InterfaceType).As<IValidator>());

            /*
            var findValidatorsInAssembly = AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly());
            foreach (AssemblyScanner.AssemblyScanResult item in findValidatorsInAssembly)
            {
                builder
                .RegisterType(item.ValidatorType)
                .Keyed<IValidator>(item.InterfaceType)
                .As<IValidator>();
            }
            */
        }
    }
}