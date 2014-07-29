using System.Linq;
using System.Web.Mvc;
using AIMS.Models;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Manage
{
    //[Authorize]
    [RoutePrefix("Password-Reset")]
    [Route("{action=index}")] 
    public class PasswordResetController : Controller
    {
        readonly UserAccountService<AimsUser> _userAccountService;
        readonly AuthenticationService<AimsUser> _authenticationService;
        public PasswordResetController(AuthenticationService<AimsUser> authenticationService)
        {
            this._authenticationService = authenticationService;
            this._userAccountService = authenticationService.UserAccountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PasswordResetInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = this._userAccountService.GetByEmail(model.Email);
                    if (account != null)
                    {
                        if (!account.PasswordResetSecrets.Any())
                        {
                            this._userAccountService.ResetPassword(model.Email);
                            return View("ResetSuccess");
                        }

                        var vm = new PasswordResetWithSecretInputModel(account.ID);
                        vm.Questions =
                            account.PasswordResetSecrets.Select(
                                x => new PasswordResetSecretViewModel
                                {
                                    QuestionID = x.PasswordResetSecretID,
                                    Question = x.Question
                                }).ToArray();

                        return View("ResetWithQuestions", vm);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email");
                    }
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ResetWithQuestions/{id}"), HttpGet]
        public ActionResult ResetWithQuestions(PasswordResetWithSecretInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var answers =
                        model.Questions.Select(x => new PasswordResetQuestionAnswer { QuestionID = x.QuestionID, Answer = x.Answer });
                    this._userAccountService.ResetPasswordFromSecretQuestionAndAnswer(model.UnprotectedAccountID.Value, answers.ToArray());
                    return View("ResetSuccess");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var id = model.UnprotectedAccountID;
            if (id != null)
            {
                var account = this._userAccountService.GetByID(id.Value);
                if (account != null)
                {
                    var vm = new PasswordResetWithSecretInputModel(account.ID);
                    vm.Questions =
                        account.PasswordResetSecrets.Select(
                            x => new PasswordResetSecretViewModel
                            {
                                QuestionID = x.PasswordResetSecretID,
                                Question = x.Question
                            }).ToArray();
                    return View("ResetWithQuestions", vm);
                }
            }

            return RedirectToAction("Index");
        }

        [Route("Confirm/{id}"), HttpPost]
        public ActionResult Confirm(string id)
        {
            var vm = new ChangePasswordFromResetKeyInputModel()
            {
                Key = id
            };
            return View("Confirm", vm);
        }

        [ValidateAntiForgeryToken]
        [Route("Confirm/{id}"), HttpPost]
        public ActionResult Confirm(ChangePasswordFromResetKeyInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AimsUser account;
                    if (this._userAccountService.ChangePasswordFromResetKey(model.Key, model.Password, out account))
                    {
                        if (account.IsLoginAllowed && !account.IsAccountClosed)
                        {
                            this._authenticationService.SignIn(account);
                            if (account.RequiresTwoFactorAuthCodeToSignIn())
                            {
                                return RedirectToAction("TwoFactorAuthCodeLogin", "Login");
                            }
                            if (account.RequiresTwoFactorCertificateToSignIn())
                            {
                                return RedirectToAction("CertificateLogin", "Login");
                            }
                        }

                        return RedirectToAction("Success");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error changing password. The key might be invalid.");
                    }
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
