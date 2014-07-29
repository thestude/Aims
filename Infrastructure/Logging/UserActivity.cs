using System;
using NHibernate;

namespace AIMS.Infrastructure.Logging
{
    public interface IUserActivity
    {
        void LogIt(string userName, string activity, string fromIp = "");
    }

    public partial class UserActivity : IUserActivity
    {
        readonly ISession _session;
        public UserActivity(ISession session)
        {
            _session = session;
        }
        public virtual void LogIt(string userName, string activity, string fromIp = "")
        {
            //var log = new Models.UserActivity
            //{
            //    IpAddress = fromIp,
            //    Activity = activity,
            //    CreatedOn = DateTime.UtcNow,
            //    UserName = userName
            //};
            //_session.Save(log);
        }
    }
}