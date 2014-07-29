using System;
using System.Linq;
using AIMS.Config.Bundles;
using System.Web.Optimization;

namespace AIMS
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            var instances = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                            from t in assembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(ICustomBundleConfig))
                                  && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as ICustomBundleConfig;


            foreach (var instance in instances)
            {
                instance.RegisterBundles(bundles);
            }
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}
