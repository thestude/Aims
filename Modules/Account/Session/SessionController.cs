using System;
using System.Security.Claims;
using AIMS.Models;
using System.Web.Mvc;
using FluentValidation;
using System.IdentityModel.Tokens;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.Security.Cryptography.X509Certificates;

namespace AIMS.Modules.Account.Session
{
    [RoutePrefix("Session")]
    [Route("{action=new}")] 
    public class SessionController : Controller
    {
        readonly UserAccountService<AimsUser> _userAccountService;
        readonly AuthenticationService<AimsUser> _authSvc;

        public SessionController(AuthenticationService<AimsUser> authSvc)
        {
            this._userAccountService = authSvc.UserAccountService;
            this._authSvc = authSvc;
        }

        // GET: /FacilitySetup/Example/New
        [AllowAnonymous]
        public ActionResult New()
        {
            return View(new LoginInputModel());
        }

        // POST: /FacilitySetup/Example/
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                AimsUser account;
                if (_userAccountService.AuthenticateWithUsernameOrEmail(model.Username, model.Password, out account))
                {
                    var userClaims = new[]
                                     {
                                         new Claim(ClaimTypes.Email, account.Email),
                                         new Claim(ClaimTypes.Name, account.Username),
                                         new Claim(ClaimTypes.GivenName, account.LastName + ", " + account.FirstName)
                                     };
                    //add claims
                    _userAccountService.AddClaims(account.ID, userClaims);

                    _authSvc.SignIn(account, model.RememberMe);

                    if (account.RequiresTwoFactorAuthCodeToSignIn())
                    {
                        return RedirectToAction("TwoFactorAuthCodeLogin");
                    }
                    if (account.RequiresTwoFactorCertificateToSignIn())
                    {
                        return RedirectToAction("CertificateLogin");
                    }

                    if (account.RequiresPasswordReset)
                    {
                        // this might mean many things -- 
                        // it might just mean that the user should change the password, 
                        // like the expired password below, so we'd just redirect to change password page
                        // or, it might mean the DB was compromised, so we want to force the user
                        // to reset their password but via a email token, so we'd want to 
                        // let the user know this and invoke ResetPassword and not log them in
                        // until the password has been changed
                        //userAccountService.ResetPassword(account.ID);

                        // so what you do here depends on your app and how you want to define the semantics
                        // of the RequiresPasswordReset property
                    }
                    
                    if (_userAccountService.IsPasswordExpired(account))
                    {
                        return RedirectToAction("Index", "ChangePassword");
                    }
                    
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }

            return View("New",model);
        }

        [Route("TwoFactorAuth"), HttpGet]
        public ActionResult TwoFactorAuthCodeLogin()
        {
            if (!User.HasUserID())
            {
                // if the temp cookie is expired, then make the login again
                return RedirectToAction("Index");
            }

            return View();
        }

        [ValidateAntiForgeryToken]
        [Route("TwoFactorAuth"), HttpPost]
        public ActionResult TwoFactorAuthCodeLogin(string button, TwoFactorAuthInputModel model)
        {
            if (!User.HasUserID())
            {
                // if the temp cookie is expired, then make the login again
                return RedirectToAction("Index");
            }

            if (button == "signin")
            {
                if (ModelState.IsValid)
                {
                    AimsUser account;
                    if (_userAccountService.AuthenticateWithCode(this.User.GetUserID(), model.Code, out account))
                    {
                        _authSvc.SignIn(account);

                        if (_userAccountService.IsPasswordExpired(account))
                        {
                            //TODO: Add flash Message
                            return RedirectToAction("Index", "ChangePassword");
                        }

                        if (Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Code");
                    }
                }
            }
            
            if (button == "resend")
            {
                ModelState.Clear();
                this._userAccountService.SendTwoFactorAuthenticationCode(this.User.GetUserID());
            }

            return View("TwoFactorAuthCodeLogin", model);
        }

        [Route("CertificateLogin"), HttpGet]
        public ActionResult CertificateLogin()
        {
            if (Request.ClientCertificate != null && 
                Request.ClientCertificate.IsPresent && 
                Request.ClientCertificate.IsValid)
            {
                try
                {
                    var cert = new X509Certificate2(Request.ClientCertificate.Certificate);
                    AimsUser account;

                    var result = false;
                    // we're allowing the use of certs for login and for two factor auth. normally you'd 
                    // do only one or the other, but for the sake of the sample we're allowing both.
                    if (User.Identity.IsAuthenticated)
                    {
                        // this is when we're doing cert logins for two factor auth
                        result = this._authSvc.UserAccountService.AuthenticateWithCertificate(User.GetUserID(), cert, out account);
                    }
                    else
                    {
                        // this is when we're just doing certs to login (so no two factor auth)
                        result = this._authSvc.UserAccountService.AuthenticateWithCertificate(cert, out account);
                    }

                    if (result)
                    {
                        this._authSvc.SignIn(account, AuthenticationMethods.X509);

                        if (_userAccountService.IsPasswordExpired(account))
                        {
                            return RedirectToAction("Index", "ChangePassword");
                        }

                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "Invalid login");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            
            return View();
        }

        // DELETE: /FacilitySetup/Example/5
        public ActionResult Destroy()
        {
            if (User.Identity.IsAuthenticated)
            {
                _authSvc.SignOut();
                return RedirectToAction("New");
            }
            //TODO: redirect to login
            return View();
        }
    }
}
