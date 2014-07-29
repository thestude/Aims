using System;
using System.Linq;
using AIMS.Models;
using System.Web.Mvc;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Manage
{
    //[Authorize]
    [RoutePrefix("Change-Secret-Question")]
    [Route("{action=index}")]
    public class ChangeSecretQuestionController : Controller
    {
        readonly UserAccountService<AimsUser> _userAccountService;
        public ChangeSecretQuestionController(UserAccountService<AimsUser> userAccountService)
        {
            this._userAccountService = userAccountService;
        }
        
        public ActionResult Index()
        {
            var account = this._userAccountService.GetByID(User.GetUserID());
            var vm = new PasswordResetSecretsViewModel
            {
                Secrets = account.PasswordResetSecrets.ToArray()
            };
            return PartialView("Index", vm);
        }

        [ChildActionOnly]
        public ActionResult ChildIndex()
        {
            return Index();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(Guid id)
        {
            this._userAccountService.RemovePasswordResetSecret(User.GetUserID(), id);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddSecretQuestionInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._userAccountService.AddPasswordResetSecret(User.GetUserID(), model.Question, model.Answer);
                    return RedirectToAction("Index");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("Add", model);
        }
    }
}
