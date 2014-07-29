using AIMS.Models;
using System.Web.Mvc;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Registration
{
    [AllowAnonymous]
    [RoutePrefix("Registration")]
    [Route("{action=new}")] 
    public class RegistrationController : Controller
    {
        readonly UserAccountService<AimsUser> _userAccountService;
        readonly AuthenticationService<AimsUser> _authSvc;

        public RegistrationController(AuthenticationService<AimsUser> authSvc)
        {
            _authSvc = authSvc;
            _userAccountService = authSvc.UserAccountService;
        }

        public ActionResult New()
        {
            return View(new RegisterInputModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(RegisterInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = this._userAccountService.CreateAccount(model.Username, model.Password, model.Email);
                    ViewData["RequireAccountVerification"] = this._userAccountService.Configuration.RequireAccountVerification;
                    return View("Success", model);
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);//TODO: Change view from index to new
        }

        [Route("Verify"), HttpGet]
        public ActionResult Verify()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("Verify"), HttpPost]
        public ActionResult Verify(string foo)
        {
            try
            {
                this._userAccountService.RequestAccountVerification(User.GetUserID());
                return View("Success");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
        }

        [Route("Cancel/{id}"), HttpGet]
        public ActionResult Cancel(string id)
        {
            try
            {
                bool closed;
                this._userAccountService.CancelVerification(id, out closed);
                if (closed)
                {
                    return View("Closed");
                }
                else
                {
                    return View("Cancel");
                }
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View("Error");
        }
    }
}
