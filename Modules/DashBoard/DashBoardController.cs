using System.Web.Mvc;

namespace AIMS.Modules.DashBoard
{
    [RoutePrefix("dashboard")]
    [Route("{action=index}/{id?}")]
    public class DashBoardController : Controller
    {
        // GET: /DashBoard/Example/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /DashBoard/Example/New
        public ActionResult New()
        {
            return View();
        }

        // POST: /DashBoard/Example/
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

        // GET: /DashBoard/Example/5
        public ActionResult Show(int id)
        {
            return Content(string.Format("<h2>Example details for DashBoard with parameter id = {0}</h2>", id), "text/html");
        }

        // GET: /DashBoard/Example/5/Edit
        public ActionResult Edit(int id)
        {
            return View();
        }

        // PUT: /DashBoard/Example/5
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

        // GET: /DashBoard/Example/5/Delete
        public ActionResult Delete(int id)
        {
            return Content(string.Format("<h2>Are you sure you want to delete the DashBoard Example record with id = {0}</h2>", id), "text/html");
        }

        // DELETE: /DashBoard/Example/5
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

        // GET: /DashBoard/Example/Search/thesearchterm
        [Route("Search/{keyword}"), HttpGet]
        public ActionResult Search(string keyword)
        {
            return Content(string.Format("<h2>DashBoard Example search for: {0}</h2>", keyword), "text/html");
        }

    }
}
