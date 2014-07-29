using System.Web.Mvc;

namespace AIMS.Modules.Blog
{
    [RoutePrefix("blog")]
    [Route("{action=index}/{id?}")] 
    public class BlogController : Controller
    {
		// GET: /Blog/Example/
		public ActionResult Index() 
		{
            return View();
        }
		
		// GET: /Blog/Example/New
		public ActionResult New() 
		{
            return View();
        }
		
		// POST: /Blog/Example/
		public ActionResult Create(FormCollection collection)
		{
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
		
		// GET: /Blog/Example/5
		public ActionResult Show(int id)
		{
            return Content(string.Format("<h2>Example details for Blog with parameter id = {0}</h2>", id), "text/html");
        }
		
		// GET: /Blog/Example/5/Edit
		public ActionResult Edit(int id)
		{
            return View();
        }
		
		// PUT: /Blog/Example/5
		public ActionResult Update(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
		
		// GET: /Blog/Example/5/Delete
		public ActionResult Delete(int id)
		{
             return Content(string.Format("<h2>Are you sure you want to delete the Blog Example record with id = {0}</h2>", id), "text/html");
        }
		
		// DELETE: /Blog/Example/5
		public ActionResult Destroy(int id)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
		
		// GET: /Blog/Example/Search/thesearchterm
        [Route("Search/{keyword}"), HttpGet]
        public ActionResult Search(string keyword)
        {
            return Content(string.Format("<h2>Blog Example search for: {0}</h2>", keyword), "text/html");
        }
        
    }
}
