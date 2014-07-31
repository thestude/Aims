using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AIMS.Models;
using NHibernate;

namespace AIMS.Helpers
{
    public interface IUserInfoHelper
    {
        UserInfo GetUserInfo(string userName);
    }

    public class UserInfoHelper : IUserInfoHelper
    {
        private UserInfo _currentUser;
        private readonly ISession _session;

        public UserInfoHelper()
        {
            _session = DependencyResolver.Current.GetService<ISession>();
        }

        public UserInfoHelper(ISession session)
        {
            _session = session;
        }

        public UserInfo GetUserInfo(string userName)
        {
            var userInfo = new UserInfo();

            var user = _session.QueryOver<AimsUser>().Where(u => u.Username == userName).SingleOrDefault();
            if (user != null)
            {
                userInfo.FirstName = user.FirstName;
                userInfo.LastName = user.LastName;
                userInfo.Email = user.Email;
                userInfo.Organizations = new List<Organization>();
                foreach (var organization in user.Organizations)
                {
                    userInfo.Organizations.Add(new Organization
                    {
                        Id = organization.ID,
                        Acronym = organization.Acronym,
                        Name = organization.Name
                    });
                }
            }
            return userInfo;
        }
    }

    public class UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); }}
        public string Email { get; set; }
        public List<Organization> Organizations { get; set; }
    }

    public class Organization
    {
        public Guid Id { get; set; }
        public string Acronym { get; set; }
        public string Name { get; set; } 
    }

}