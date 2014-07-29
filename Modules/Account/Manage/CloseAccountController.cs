using System.Web.Mvc;
using AIMS.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Manage
{
    //[Authorize]
    [RoutePrefix("Close-Account")]
    [Route("{action=index}")]
    public class CloseAccountController : Controller
    {
        readonly UserAccountService<AimsUser> _userAccountService;
        public CloseAccountController(UserAccountService<AimsUser> userAccountService)
        {
            this._userAccountService = userAccountService;
        }

        public ActionResult Index()
        {
            return PartialView("Index");
        }

        [ChildActionOnly]
        public ActionResult ChildIndex()
        {
            return Index();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string button)
        {
            if (button == "yes")
            {
                try
                {
                    this._userAccountService.DeleteAccount(User.GetUserID());
                    return RedirectToAction("Destroy", "Session");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View("Index");
        }

    }
}
