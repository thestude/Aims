using AIMS.Models;
using System.Web.Mvc;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Manage
{
    //[Authorize]
    [RoutePrefix("Change-Email")]
    [Route("{action=index}")] 
    public class ChangeEmailController : Controller
    {
        readonly AuthenticationService<AimsUser> _authSvc;
        readonly UserAccountService<AimsUser> _userAccountService;

        public ChangeEmailController(AuthenticationService<AimsUser> authSvc)
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
        public ActionResult Index(ChangeEmailRequestInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._userAccountService.ChangeEmailRequest(User.GetUserID(), model.NewEmail);
                    if (_userAccountService.Configuration.RequireAccountVerification)
                    {
                        return View("ChangeRequestSuccess", (object)model.NewEmail);
                    }
                    else
                    {
                        //TODO: Replace with a flash message
                        return View("Success");
                    }
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View("Index", model);
        }

        [AllowAnonymous]
        [Route("Confirm/{id}"), HttpGet]
        public ActionResult Confirm(string id)
        {
            var account = this._userAccountService.GetByVerificationKey(id);
            if (account.HasPassword())
            {
                var vm = new ChangeEmailFromKeyInputModel {Key = id};
                return View("Confirm", vm);
            }

            try
            {
                _userAccountService.VerifyEmailFromKey(id, out account);
                // since we've changed the email, we need to re-issue the cookie that
                // contains the claims.
                _authSvc.SignIn(account);
                return RedirectToAction("Success");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View("Confirm", null);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Confirm/{id}"), HttpPost]
        public ActionResult Confirm(ChangeEmailFromKeyInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AimsUser account;
                    this._userAccountService.VerifyEmailFromKey(model.Key, model.Password, out account);
                    
                    // since we've changed the email, we need to re-issue the cookie that
                    // contains the claims.
                    _authSvc.SignIn(account);
                    return RedirectToAction("Success");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            
            return View("Confirm", model);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
