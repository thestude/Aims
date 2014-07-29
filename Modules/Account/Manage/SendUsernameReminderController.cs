using System.Web.Mvc;
using AIMS.Models;
using AIMS.Modules.Account.Models;
using BrockAllen.MembershipReboot;
using System.ComponentModel.DataAnnotations;

namespace AIMS.Modules.Account.Manage
{
    //[Authorize]
    [RoutePrefix("Send-Username-Reminder")]
    [Route("{action=index}")]
    public class SendUsernameReminderController : Controller
    {
        readonly UserAccountService<AimsUser> _userAccountService;

        public SendUsernameReminderController(UserAccountService<AimsUser> userAccountService)
        {
            this._userAccountService = userAccountService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ChildIndex()
        {
            return Index();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SendUsernameReminderInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._userAccountService.SendUsernameReminder(model.Email);
                    ViewData["Email"] = model.Email;
                    return View("Success");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }
    }
}
