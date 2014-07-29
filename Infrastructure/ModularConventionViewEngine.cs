using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AIMS;

namespace AIMS.Infrastructure
{
    public class ModularConventionViewEngine : RazorViewEngine
    {
        //This needs to be initialized to the root namespace of the MVC project.
        //Usually, the namespace of the Global.asax's codebehind will do the trick.
        //TODO: Replace this with a variable pointing to the root domain
        private static readonly string RootNamespace = typeof(MvcApplication).Namespace;
        private static readonly ConcurrentDictionary<string, string> ViewPathCache = new ConcurrentDictionary<string, string>();

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var path = GetPath(controllerContext, viewName);

            if (path != null)
            {
                return new ViewEngineResult(CreateView(controllerContext, path, null), this);
            }
            //Check the usual suspects
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var path = GetPath(controllerContext, partialViewName);

            if (path != null)
            {
                return new ViewEngineResult(CreatePartialView(controllerContext, path), this);
            }
            //check the usual suspects
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        private string GetPath(ControllerContext controllerContext, string viewName)
        {
            string path;

            var ctrlProps = GetControllerProperties(controllerContext, viewName);

            //check if in cache
            var found = ViewPathCache.TryGetValue(ctrlProps.cachekey, out path);
            if (!found && ctrlProps.controllerNameSpace != null)
            {
                IEnumerable<string> paths = GetPossiblePaths(ctrlProps.controllerNameSpace, ctrlProps.controllerName, viewName);
                path = paths.FirstOrDefault(p => VirtualPathProvider.FileExists(p));

                //add to cache or indicate to delegate processing by adding a null for path
                ViewPathCache.TryAdd(ctrlProps.cachekey, path);
            }
            return path;
        }

        private static IEnumerable<string> GetPossiblePaths(string controllerNamespace, string controllerName, string viewName)
        {
            var paths = new List<string>();

            var controllerFolder = string.Format("~{0}", controllerNamespace);

            //View in folder with controller name within a views-folder within a controller-folder (controller-folder/views/controller-name/view.cshtml)
            paths.Add(string.Format("{0}/Views/{1}/{2}.cshtml", controllerFolder, controllerName, viewName));

            //View in a view-folder within controller-folder (controller-folder/views/view.cshtml)
            paths.Add(string.Format("{0}/Views/{1}.cshtml", controllerFolder, viewName));

            //View in the same folder as controller (controller-folder/view.cshtml)
            paths.Add(string.Format("{0}/{1}.cshtml", controllerFolder, viewName));

            return paths;
        }

        private static dynamic GetControllerProperties(ControllerContext controllerContext, string viewName)
        {
            string controllerNameSpace = null;
            var controllerType = controllerContext.Controller.GetType();
            var controllerName = controllerType.Name.Replace("Controller", string.Empty);
            if (controllerType.Namespace != null)
            {
                controllerNameSpace = controllerType.Namespace.Replace(RootNamespace, string.Empty).Replace(".", "/");
            }
            //NOTE: Is this a good key????
            var cachekey = string.Format("{0}-{1}-{2}", controllerNameSpace, controllerName, viewName);

            return new { controllerNameSpace, controllerName, cachekey };
        }
    }
}