using System.Web.Mvc;
using AIMS.Models;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Manage
{
    //[Authorize]
    [RoutePrefix("Change-Mobile")]
    [Route("{action=index}")] 
    public class ChangeMobileController : Controller
    {
        UserAccountService<AimsUser> userAccountService;
        AuthenticationService<AimsUser> authSvc;

        public ChangeMobileController(AuthenticationService<AimsUser> authSvc)
        {
            this.userAccountService = authSvc.UserAccountService;
            this.authSvc = authSvc;
        }

        public ActionResult Index()
        {
            var userId = User.GetUserID();
            var acct = this.authSvc.UserAccountService.GetByID(userId);
            return PartialView("Index", new ChangeMobileRequestInputModel { Current = acct.MobilePhoneNumber });
        }

        [ChildActionOnly]
        public ActionResult ChildIndex()
        {
            return Index();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string button, ChangeMobileRequestInputModel model)
        {
            if (button == "change")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        this.userAccountService.ChangeMobilePhoneRequest(User.GetUserID(), model.NewMobilePhone);
                        return View("ChangeRequestSuccess", (object)model.NewMobilePhone);
                    }
                    catch (ValidationException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }

            if (button == "remove")
            {
                this.userAccountService.RemoveMobilePhone(User.GetUserID());
                return View("Success");
            }

            var acct = this.authSvc.UserAccountService.GetByID(User.GetUserID());
            model.Current = acct.MobilePhoneNumber;
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(ChangeMobileFromCodeInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (this.userAccountService.ChangeMobilePhoneFromCode(this.User.GetUserID(), model.Code))
                    {
                        // since the mobile had changed, reissue the 
                        // cookie with the updated claims
                        authSvc.SignIn(User.GetUserID());

                        return View("Success");
                    }

                    ModelState.AddModelError("", "Error confirming code.");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View("Confirm", model);
        }
    }
}
