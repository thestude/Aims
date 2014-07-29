using System.Web.Mvc;
using AIMS.Models;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Manage
{
    //[Authorize]
    [RoutePrefix("Change-Password")]
    [Route("{action=index}")]
    public class ChangePasswordController : Controller
    {
        readonly UserAccountService<AimsUser> _userAccountService;
        public ChangePasswordController(UserAccountService<AimsUser> userAccountService)
        {
            this._userAccountService = userAccountService;
        }
        
        public ActionResult Index()
        {
            if (!User.HasUserID())
            {
                return new HttpUnauthorizedResult();
            }

            var acct = this._userAccountService.GetByID(User.GetUserID());
            if (acct.HasPassword())
            {
                return View(new ChangePasswordInputModel());
            }
            else
            {
                return PartialView("SendPasswordReset");
            }
        }

        [ChildActionOnly]
        public ActionResult ChildIndex()
        {
            return Index();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ChangePasswordInputModel model)
        {
            if (!User.HasUserID())
            {
                return new HttpUnauthorizedResult();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._userAccountService.ChangePassword(User.GetUserID(), model.OldPassword, model.NewPassword);
                    return View("Success");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendPasswordReset()
        {
            if (!User.HasUserID())
            {
                return new HttpUnauthorizedResult();
            }

            try
            {
                var acct = this._userAccountService.GetByID(User.GetUserID());
                this._userAccountService.ResetPassword(acct.Tenant, acct.Email);
                return View("Sent");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View("SendPasswordReset");
        }
    }
}
