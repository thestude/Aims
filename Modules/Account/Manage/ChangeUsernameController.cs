using System.Web.Mvc;
using AIMS.Models;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Manage
{
    //[Authorize]
    [RoutePrefix("Change-Username")]
    [Route("{action=index}")]
    public class ChangeUsernameController : Controller
    {
        readonly UserAccountService<AimsUser> _userAccountService;
        readonly AuthenticationService<AimsUser> _authSvc;

        public ChangeUsernameController(AuthenticationService<AimsUser> authSvc)
        {
            this._userAccountService = authSvc.UserAccountService;
            this._authSvc = authSvc;
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
        public ActionResult Index(ChangeUsernameInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._userAccountService.ChangeUsername(User.GetUserID(), model.NewUsername);
                    this._authSvc.SignIn(User.GetUserID());
                    return RedirectToAction("Success");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View("Index", model);
        }

        public ActionResult Success()
        {
            return View("Success", (object)User.Identity.Name);
        }
    }
}
