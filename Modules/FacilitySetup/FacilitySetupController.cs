using System;
using System.Threading;
using System.Web.Mvc;
using AIMS.Helpers;
using AIMS.Modules.FacilitySetup.Services;

namespace AIMS.Modules.FacilitySetup
{
    [RoutePrefix("facilitysetup")]
    [Route("{action=index}/{id?}")]
    public class FacilitySetupController : Controller
    {
        public readonly IFacilitySetupService _facilitySetupService;
        private readonly IUserInfoHelper _userInfoHelper;

        public FacilitySetupController(IFacilitySetupService facilitySetupService, IUserInfoHelper userInfoHelper)
        {
            _facilitySetupService = facilitySetupService;
            _userInfoHelper = userInfoHelper;
        }

        // GET: /FacilitySetup/
        public ActionResult Index()
        {
            //var currentUserInfo = _userInfoHelper.GetUserInfo(User.Identity.)
            return View(_facilitySetupService.GetFacilitySetUp(Guid.Empty));
        }

    }
}
