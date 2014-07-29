using System.Web.Optimization;

namespace AIMS.Config.Bundles
{
    interface ICustomBundleConfig
    {
        void RegisterBundles(BundleCollection bundles);
    }
}
