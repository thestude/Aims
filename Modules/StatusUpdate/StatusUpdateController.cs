using System;
using System.Linq;
using System.Web.Mvc;
using AIMS.Helpers;
using AIMS.Infrastructure.Logging;
using AIMS.Modules.StatusUpdate.Services;

namespace AIMS.Modules.StatusUpdate
{
    // TODO: Add tests
    [RoutePrefix("statusupdate")]
    [Route("{action=index}/{id?}")]
    public class StatusUpdateController : Controller
    {
        private readonly IStatusUpdateService _statusUpdateService;
        private readonly IUserInfoHelper _userInfoHelper;
        private readonly ILogger _logger;

        public StatusUpdateController(IStatusUpdateService statusUpdateService,
            IUserInfoHelper userInfoHelper, ILogger logger)
        {
            _statusUpdateService = statusUpdateService;
            _userInfoHelper = userInfoHelper;
            _logger = logger;
        }
        
        // GET: /StatusUpdate/Index/
        public ActionResult Index()
        {
            var currentUserInfo = _userInfoHelper.GetUserInfo(User.Identity.Name);
            var org = currentUserInfo.Organizations.FirstOrDefault();
            var model = _statusUpdateService.GetCurrentStatus(org.Id);
            
            return View(model);
        }

        // TODO: Add Anti-forgery check
        // PUT: /StatusUpdate/5
        public JsonResult Update(int? id, Models.StatusUpdate model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new
                    {
                        errors = ModelState.Errors(),
                        isError = true
                    });
                var currentUserInfo = _userInfoHelper.GetUserInfo(User.Identity.Name);
                if (currentUserInfo != null && currentUserInfo.Organizations != null)
                {
                    var org = currentUserInfo.Organizations.FirstOrDefault();
                    if (org != null)
                    {
                        model.OrganizationId = org.Id;
                        var result = _statusUpdateService.UpdateStatus(model);
                    }
                }
                return Json(new
                {
                    redirectUrl = Url.Action("Index"),
                    isRedirect = true
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return Json(new
                {
                    error = "An error has ocured while updating the status. Try again before contacting technical support." ,
                    isError = true
                });
            }
        }

    }
}
