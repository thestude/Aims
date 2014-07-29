using System.Web.Mvc;
using AIMS.Models;
using BrockAllen.MembershipReboot;

namespace AIMS.Modules.Account.Manage
{
    [RoutePrefix("Management")]
    [Route("{action=index}")] 
    public class ManagementController : Controller
    {
        readonly AuthenticationService<AimsUser> _authSvc;
        readonly UserAccountService<AimsUser> _userAccountService;

        public ManagementController(AuthenticationService<AimsUser> authSvc)
        {
            _authSvc = authSvc;
            _userAccountService = authSvc.UserAccountService;
        }

        //
        // GET: /Management/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Management/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Management/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Management/Create
        [HttpPost]
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

        //
        // GET: /Management/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Management/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        //
        // GET: /Management/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Management/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
    }
}
