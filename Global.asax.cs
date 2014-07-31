using AIMS.Infrastructure.AutoFac;
using Autofac;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Helpers;
using AIMS.Infrastructure;
using System.Security.Claims;
using System.Web.Optimization;
using Autofac.Integration.Mvc;
using Autofac.Extras.CommonServiceLocator;
using FluentValidation.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace AIMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IContainer _container;
        private static ContainerBuilder _builder;

        public static IContainer Container
        {
            get
            {
                if (_container != null) return _container;

                var assembly = typeof (MvcApplication).Assembly;

                //Setup autofac intergration
                _builder = new ContainerBuilder();
                _builder.RegisterAssemblyModules(assembly);
                _builder.RegisterControllers(assembly);

                _container = _builder.Build();

                return _container;
            }
        }
        protected void Application_Start()
        {
           
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));

            // Set the service locator to an AutofacServiceLocator
            var commonsvclocator = new AutofacServiceLocator(Container);
            ServiceLocator.SetLocatorProvider(() => commonsvclocator);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            //Add the custom view engine
            ViewEngines.Engines.Clear();

            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new AutofacValidatorFactory(Container);
            });

            var customViewEngine = new ModularConventionViewEngine();
            ViewEngines.Engines.Add(customViewEngine);

            RouteTable.Routes.MapMvcAttributeRoutes();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
