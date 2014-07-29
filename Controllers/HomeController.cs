using System;
using System.Linq;
using System.Web.Mvc;
using AIMS.Helpers;
using AIMS.Models;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Nh;
using NHibernate;
using NHibernate.Linq;

namespace AIMS.Controllers
{
    public class HomeController : Controller
    {
        static Random _r = new Random();
        private readonly ISession _session;
        private readonly UserAccountService<AimsUser> _userAccountService;

        public HomeController(ISession session, UserAccountService<AimsUser> userAccountService)
        {
            _session = session;
            _userAccountService = userAccountService;
        }

        public ActionResult Index()
        {
            var user  =  new AimsUser();
            user = this._userAccountService.CreateAccount("Test" + _r.Next(65), "password", "test" + _r.Next(65) + "@email.com");
            user.FirstName = "Test" + _r.Next(65);
            user.LastName = "Test" + _r.Next(65);
            _session.Save(user);

            return View();
        }

        public ActionResult About()
        {

            var user2 = _session.Query<AimsUser>().FirstOrDefault();
            ViewBag.Message = string.Format("Your application description page.{0}", user2.FirstName);

            return View();
        }

        public ActionResult Contact()
        {
            var user = _session.Query<AimsUser>().FirstOrDefault();
            user.FirstName = "Changed Name" + _r.Next(65);
            _userAccountService.Update(user);

            var user2 = _session.Get<AimsUser>(user.ID);
            ViewBag.Message = "Your contact page. " + user2.FirstName;

            return View();
        }

        [Route("seed/{task?}")]
        public ActionResult Seed(string task)
        {
            if (!string.IsNullOrEmpty(task))
            {
#if DEBUG
                var basePath = Server.MapPath("~/");
                var seed = new SeedDatabase(_session,_userAccountService, basePath);
                seed.Seed(task);
                return Content(task + " seeded successfully.");
#endif
            }
            return View();
        }
    }
}