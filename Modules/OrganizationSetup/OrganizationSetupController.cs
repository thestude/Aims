using System;
using System.Linq;
using System.Web.Mvc;
using AIMS.Helpers;
using AIMS.Infrastructure.Logging;
using AIMS.Modules.OrganizationSetup.Services;
using AIMS.Modules.OrganizationSetup.Models;
using NHibernate;

namespace AIMS.Modules.OrganizationSetup
{
    // TODO: Add validation
    // TODO: Implement delete contact
    // TODO: Add tests
    [RoutePrefix("organizationsetup")]
    [Route("{action=index}/{id?}")]
    public class OrganizationSetupController : Controller
    {
        private readonly IOrganizationSetupService _organizationSetupService;
        private readonly IUserInfoHelper _userInfoHelper;
        private readonly ISession _session;
        private readonly ILogger _logger;

        public OrganizationSetupController(IOrganizationSetupService organizationSetupService,
            IUserInfoHelper userInfoHelper, ISession session, ILogger logger)
        {
            _organizationSetupService = organizationSetupService;
            _userInfoHelper = userInfoHelper;
            _session = session;
            _logger = logger;
        }

        // GET: /OrganizationSetup/
        public ActionResult Index()
        {
            var orgSetup = new OrganizationSetUp();
            var currentUserInfo = _userInfoHelper.GetUserInfo(User.Identity.Name);
            var org = currentUserInfo.Organizations.FirstOrDefault();
            orgSetup = _organizationSetupService.GetOrganizationSetUp(org.Id);

            _organizationSetupService.FillTypes(orgSetup);
            return View(orgSetup);
        }

        // TODO: Add Anti-forgery check
        public ActionResult Update(int? id, OrganizationSetUp model)
        {
            try
            {
                if(!ModelState.IsValid)
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
                        model.OrganizationId = org.Id.ToString();
                        var result = _organizationSetupService.UpdateOrganizationSetUp(model);
                    }
                }
                return Json(new
                {
                    redirectUrl = Url.Action("Index"),
                    isRedirect = true
                });
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                return Json(new
                {
                    error = "An error has ocured while updating the organization. Try again before contacting technical support.",
                    isError = true
                });
            }
        }
    }
}
