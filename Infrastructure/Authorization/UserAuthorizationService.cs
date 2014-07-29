using System;
using NHibernate;
using System.Data;
using System.Linq;
using AIMS.Models;
using Rhino.Security;
using NHibernate.Criterion;
using Rhino.Security.Model;
using Rhino.Security.Interfaces;
using System.Collections.Generic;

namespace AIMS.Infrastructure.Authorization
{
    public class UserAuthorizationService
    {
        private const string PermissionGroupsGroupName = "permission-groups";
        private const string ApplicationUserGroupsGroupName = "application-users-group";
        private const string OrganizationUserGroupsGroupName = "organization-usergroups";
        private const string ApplicationPermissionGroupsGroupName = "application-permission-groups";
        private const string OrganizationPermissionGroupsGroupName = "organization-permission-groups";

        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly ISession _session;
        private IAuthorizationService _authorizationService;
        private IPermissionsService _permissionService;
        private readonly IPermissionsBuilderService _permissionsBuilderService;


        public UserAuthorizationService(ISession session,
                                        IPermissionsService permissionService,
                                        IAuthorizationService authorizationService,
                                        IAuthorizationRepository authorizationRepository,
                                        IPermissionsBuilderService permissionsBuilderService)
        {
            _session = session;
            _permissionService = permissionService;
            _authorizationService = authorizationService;
            _authorizationRepository = authorizationRepository;
            _permissionsBuilderService = permissionsBuilderService;
        }

        public UsersGroup GetUsersGroup(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The group name cannot be null");
            }
            return _authorizationRepository.GetUsersGroupByName(name);
        }

        public UsersGroup GetPermisionGroup(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The name cannot be null");
            }

            //Check if the permission group exists
            var pgroup = GetUsersGroup(name);
            if (pgroup == null) { throw new NoNullAllowedException("Could not find the permision group"); }

            //Check that it is a permision group [Child of PermissionGroupsGroupName] and not another type of group 
            var parentPermGroup = GetUsersGroup(PermissionGroupsGroupName);
            var permGroups = parentPermGroup.AllChildren.ToList();
            return permGroups.Contains(pgroup) ? pgroup : null;
        }

        public bool CreateOperation(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The operation name cannot be null");
            }

            Operation operation = _authorizationRepository.CreateOperation(name);
            return operation.Name == name;
        }

        public bool RemoveOperation(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The group name cannot be null");
            }

            _authorizationRepository.RemoveOperation(name);
            Operation operation = _authorizationRepository.GetOperationByName(name);
            return operation == null;
        }

        public IEnumerable<Operation> GetAllOperations()
        {
            return _session.CreateCriteria<Operation>().List<Operation>();
        }

        public Operation GetOperation(string opname)
        {
            if (String.IsNullOrEmpty(opname))
            {
                throw new ArgumentException("The group name cannot be null");
            }

            return _authorizationRepository.GetOperationByName(opname);
        }

        public bool RemovePermissionGroup(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The group name cannot be null");
            }
            return RemoveGroup(name);
        }

        public bool RemoveGroup(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The group name cannot be null");
            }

            _authorizationRepository.RemoveUsersGroup(name);
            UsersGroup usersgroup = _authorizationRepository.GetUsersGroupByName(name);
            return usersgroup == null;
        }

        public bool CreateApplicationPermissionGroupsGroup(string name = ApplicationPermissionGroupsGroupName)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The permission group name cannot be null");
            }

            UsersGroup permGroup = _authorizationRepository.GetUsersGroupByName(PermissionGroupsGroupName) ??
                                   _authorizationRepository.CreateUsersGroup(PermissionGroupsGroupName);

            UsersGroup appPermGroup = _authorizationRepository.CreateChildUserGroupOf(permGroup.Name, name);
            return appPermGroup != null && appPermGroup.Name == name;
        }

        public bool CreateOrganizationPermissionGroupsGroup(string name = OrganizationPermissionGroupsGroupName)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The permission group name cannot be null");
            }

            var parentPermGroup = _authorizationRepository.GetUsersGroupByName(PermissionGroupsGroupName) ??
                                   _authorizationRepository.CreateUsersGroup(PermissionGroupsGroupName);

            var orgPermGroup = _authorizationRepository.CreateChildUserGroupOf(parentPermGroup.Name, name);

            return orgPermGroup != null && orgPermGroup.Name == name;
        }

        public bool CreateOrganizationUserGroupsGroup(string name = OrganizationUserGroupsGroupName)
        {
            UsersGroup usersgroup = _authorizationRepository.CreateUsersGroup(name);
            return usersgroup.Name == name;
        }

        public bool CreateApplicationUsersGroup(string name = ApplicationUserGroupsGroupName)
        {
            UsersGroup usersgroup = _authorizationRepository.CreateUsersGroup(name);
            return usersgroup.Name == name;
        }

        public bool CreateOrganizationUsersGroup(string name, string orgUserGroupsGroupName = OrganizationUserGroupsGroupName)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("The group name cannot be null");

            UsersGroup usersgroup = _authorizationRepository.CreateChildUserGroupOf(orgUserGroupsGroupName, name);
            return usersgroup.Name == name;
        }

        public bool CreateApplicationPermisionGroupType(string appPermissionGroupTypeName, string appPermGrpsGrpName = ApplicationPermissionGroupsGroupName)
        {
            if (String.IsNullOrEmpty(appPermissionGroupTypeName))
            {
                throw new ArgumentException("The permission-group-type name cannot be null");
            }

            UsersGroup usersgroup = _authorizationRepository.CreateChildUserGroupOf(appPermGrpsGrpName, appPermissionGroupTypeName);
            return usersgroup.Name == appPermissionGroupTypeName;
        }

        public bool CreateOrganizationPermisionGroupType(string orgPermissionGroupTypeName, string orgPermissionGroupName = OrganizationPermissionGroupsGroupName)
        {
            if (String.IsNullOrEmpty(orgPermissionGroupTypeName))
            {
                throw new ArgumentException("The permission-group-type name cannot be null");
            }

            UsersGroup usersgroup = _authorizationRepository.CreateChildUserGroupOf(orgPermissionGroupName, orgPermissionGroupTypeName);
            return usersgroup.Name == orgPermissionGroupTypeName;
        }

        public bool CreateApplicationPermissionGroup(string name, string appPermissionGroupType)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(appPermissionGroupType))
            {
                throw new ArgumentException("The group name or type cannot be null");
            }

            UsersGroup usersgroup = _authorizationRepository.CreateChildUserGroupOf(appPermissionGroupType, name);
            return usersgroup.Name == name;
        }

        public bool CreateOrganizationPermissionGroup(string name, string orgPermissionGroupType)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The group name cannot be null");
            }

            UsersGroup usersgroup = _authorizationRepository.CreateChildUserGroupOf(orgPermissionGroupType, name);
            return usersgroup.Name == name;
        }

        public IEnumerable<string> GetAllOrganizationPermissionGroupTypes(string orgPermissionGroupName = OrganizationPermissionGroupsGroupName)
        {
            var orgPermGrpTypes = GetUsersGroup(orgPermissionGroupName);
            if (orgPermGrpTypes == null)
                return null;
            return orgPermGrpTypes.DirectChildren.Select(g => g.Name);
        }

        public IEnumerable<string> GetAllOrganizationPermissionGroups(string orgPermGrpsGrpName = OrganizationPermissionGroupsGroupName)
        {
            var grpTypeNames = GetAllOrganizationPermissionGroupTypes();
            var permGrpsGrpName = GetUsersGroup(orgPermGrpsGrpName);

            if (grpTypeNames == null || permGrpsGrpName == null)
                return null;

            return permGrpsGrpName.AllChildren.Select(g => g.Name).Where(grp => !grpTypeNames.Contains(grp));
        }

        public IEnumerable<string> GetAllOrganizationPermissionGroupsByType(string orgPermissionGroupType)
        {
            var permGroupType = GetUsersGroup(orgPermissionGroupType);
            if (permGroupType == null)
                return null;

            return permGroupType.DirectChildren.Select(g => g.Name);
        }

        public IEnumerable<string> GetAllApplicationPermissionGroupTypes(string appPermissionGroupName = ApplicationPermissionGroupsGroupName)
        {
            var appPermGrpTypes = GetUsersGroup(appPermissionGroupName);
            if (appPermGrpTypes == null)
                return null;

            return appPermGrpTypes.DirectChildren.Select(g => g.Name);
        }

        public IEnumerable<string> GetAllApplicationPermissionGroups(string appPermGrpsGrpName = ApplicationPermissionGroupsGroupName)
        {
            var grpTypeNames = GetAllApplicationPermissionGroupTypes();
            var permGrpsGrpName = GetUsersGroup(appPermGrpsGrpName);

            if (grpTypeNames == null || permGrpsGrpName == null)
                return null;

            return permGrpsGrpName.AllChildren.Select(g => g.Name).Where(grp => !grpTypeNames.Contains(grp));
        }

        public IEnumerable<string> GetAllApplicationPermissionGroupsByType(string appPermissionGroupType)
        {
            var permGroupType = GetUsersGroup(appPermissionGroupType);
            if (permGroupType == null)
                return null;

            return permGroupType.DirectChildren.Select(g => g.Name);
        }

        public IEnumerable<IUser> GetAllUsersInGroup(string groupname)
        {
            if (String.IsNullOrEmpty(groupname))
            {
                throw new ArgumentException("The group name cannot be null");
            }
            return GetUsersGroup(groupname).Users;
        }

        public IEnumerable<IUser> GetAllUsersNotInGroup(string groupname)
        {
            if (String.IsNullOrEmpty(groupname))
            {
                throw new ArgumentException("The group name cannot be null");
            }

            List<Guid> users = GetAllUsersInGroup(groupname).Select(u => ((AimsUser)u).ID).ToList();

            return _session.CreateCriteria<IUser>().Add(Restrictions.Not(Restrictions.In("Id", users))).List<IUser>();
        }

        public IEnumerable<IUser> GetAllUsersInPermissionGroup(string permGroupName)
        {
            if (String.IsNullOrEmpty(permGroupName))
            {
                throw new ArgumentException("The role name cannot be null");
            }
            return GetAllUsersInGroup(permGroupName);
        }

        public IEnumerable<IUser> GetAllUsersNotInPermissionGroup(string permGroupName)
        {
            if (String.IsNullOrEmpty(permGroupName))
            {
                throw new ArgumentException("The group name cannot be null");
            }

            return GetAllUsersNotInGroup(permGroupName);
        }

        public IEnumerable<IUser> GetOrganizationUsersInPermissionGroup(string orgname, string permGroupName)
        {
            if (String.IsNullOrEmpty(orgname) || String.IsNullOrEmpty(permGroupName))
            {
                throw new ArgumentException("The organization or role name cannot be null");
            }

            //TODO: Maybe need bounded the result. check performance and mem usage
            IEnumerable<IUser> groupusers = GetAllUsersInGroup(orgname);
            return GetAllUsersInGroup(permGroupName).Intersect(groupusers);
        }

        public IEnumerable<IUser> GetOrgnizationUsersNotInPermissionGroup(string orgname, string permGroupName)
        {
            if (String.IsNullOrEmpty(orgname) || String.IsNullOrEmpty(permGroupName))
            {
                throw new ArgumentException("The organization or role name cannot be null");
            }

            //TODO: Maybe need bounded result. Check performance and mem usage
            var users = GetAllUsersInGroup(orgname);
            var roleusers = GetAllUsersInGroup(permGroupName);
            List<IUser> usersnotingroup = users.Where(x => !(roleusers.Contains(x))).ToList();
            return usersnotingroup;
        }

        public bool AddUserToGroup(IUser user, string groupname)
        {
            if (String.IsNullOrEmpty(groupname) || user == null)
            {
                throw new ArgumentException("The group name and/or user cannot be null");
            }
            _authorizationRepository.AssociateUserWith(user, groupname);
            var usersGroup = GetUsersGroup(groupname);
            if (usersGroup == null)
                return false;
            return usersGroup.Users.Contains(user);
        }
        //TODO:Test
        public bool RemoveUserFromGroup(string groupname, IUser user)
        {
            if (String.IsNullOrEmpty(groupname) || user == null)
            {
                throw new ArgumentException("The group name and/or user cannot be null");
            }
            _authorizationRepository.DetachUserFromGroup(user, groupname);
            var usersGroup = GetUsersGroup(groupname);
            return !usersGroup.Users.Contains(user);
        }
        //TODO: Consider Removing
        public bool RemoveUserFromPermissionGroup(IUser user, string permGroupName)
        {
            if (String.IsNullOrEmpty(permGroupName) || user == null)
            {
                throw new ArgumentException("The permGroupName name and/or user cannot be null");
            }

            return RemoveUserFromGroup(permGroupName, user);
        }
        //TODO: Consider Removing
        public bool AddUserToPermissionGroup(IUser user, string permGroupName)
        {
            if (String.IsNullOrEmpty(permGroupName) || user == null)
            {
                throw new ArgumentException("The permGroupName name and/or user cannot be null");
            }

            return AddUserToGroup(user, permGroupName);
        }
        //TODO: Test
        public bool RenameGroup(string groupname, string newname)
        {
            if (String.IsNullOrEmpty(groupname) || String.IsNullOrEmpty(newname))
            {
                throw new ArgumentException("The group name and/or new name cannot be null");
            }

            var usersGroup = _authorizationRepository.RenameUsersGroup(groupname, newname);

            return usersGroup.Name == newname;
        }
        //TODO: Test
        public void RemoveUser(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentException("The user cannot be null");
            }
            _authorizationRepository.RemoveUser(user);
        }

        public void AddPermisionToGroup(string operationName, string groupname)
        {
            if (String.IsNullOrEmpty(operationName) || String.IsNullOrEmpty(groupname))
            {
                throw new ArgumentException("The operation and/or group name cannot be null");
            }
            _permissionsBuilderService.Allow(operationName)
                .For(groupname)
                .OnEverything()
                .DefaultLevel()
                .Save();
        }

        public IEnumerable<Permission> GetPermissionsInGroup(string groupname)
        {
            if (String.IsNullOrEmpty(groupname))
            {
                throw new ArgumentException("The group name cannot be null");
            }
            var permsInGrp = _session.CreateCriteria<Permission>()
                .CreateAlias("UsersGroup", "ug")
                .Add(Expression.Eq("ug.Name", groupname)/*Restrictions.InsensitiveLike("ug.Name", groupname, MatchMode.Start)*/).List<Permission>();

            return permsInGrp;
        }

        public IEnumerable<string> GetOperationsNotInGroup(string groupname)
        {
            if (String.IsNullOrEmpty(groupname))
            {
                throw new ArgumentException("The group name cannot be null");
            }

            var criteria = DetachedCriteria.For<Permission>()
                .CreateAlias("UsersGroup", "ug")
                .Add(Expression.Eq("ug.Name", groupname))
                .SetProjection(Projections.Property("Operation.Id"));

            var opNamesNotInGrp = _session.CreateCriteria<Operation>()
                .Add(Subqueries.PropertyNotIn("Id", criteria))
                .SetProjection(Projections.Property("Name"))
                .List<string>();

            return opNamesNotInGrp;
        }

        public void RemovePermision(string permisionid)
        {
            var pid = new Guid(permisionid);

            var permision = _session.CreateCriteria<Permission>().Add(Restrictions.Eq("Id", pid)).UniqueResult<Permission>();

            _authorizationRepository.RemovePermission(permision);
        }

        /**
         * Application Roles Group
         *      NAME: application-roles
         *      This group contains all the application level permision/role groups
         *      All Application level users belong to a Application Permission group
         *      The only permisions allowed on this group are the application wide permissions.
         *          NOTE: Application wide permissions are applied across the whole application
         *          i.e A permision set on this group will be granted to all members of this group and all its children
         *          
         * Organization Usergroups Group
         *      NAME: organization-usergroups-collection
         *      This group contains all the organization level user groups
         *      Each organization user group is a child of this group
         * 
         * Application user Group
         *      NAME: application-users-group
         *      Group used for the sole purpose of isolating application level users.
         *          All Application level users belong to this group
         *          No permisions are assigned to this group.
         * 
         * Organization user Groups
         *      Groups used for the sole purpose of isolating users of a certain facility.
         *      All Facility/Organization users belong to a Organization group
         *      No permisions are assigned to this groups.
         *      The name of each group should be the facility name in all lowercase followed by the word "users-group".
         *          All spaces shall be replaced by a dash (-)
         *          If there are multiple words in the facility name, then the words should be joined by a dash (-)
         *          Example: For a facility named "Facility One Two" its group name shall be "facility-one-two-users-group"
         *          
         * Organization Roles Group
         *      NAME: organization-roles
         *      This group contains all the organization level permision groups
         *      All Facility/Organization users belong to a Organization Permission group
         *      All Facility/Organization users permissions belong to a Organization Permission group
         *      The only permisions allowed on this group are organization wide permisions. 
         *          NOTE: Organization wide permision span Organisations. 
         *          i.e A permision set on this group will be granted to all members of this group and all its children
         * 
         * Application Permissions Groups
         *      The groups can be thought of as Roles
         *      Groups used for isolating a group of Application level users with certain permisions.
         *      All Application Permissions Groups are children of Application Permission Group (application-roles) 
         *      The name of each group should be dictated by and be descriptive of the role that a group of users will be performing
         *          If there are multiple words in the group name, then the words should be joined by a dash (-)
         *          Example: For Application administrators the name might be "application-administrator" 
         * 
         * 
         * Organization Permisions Groups
         *      The groups can be thought of as Roles
         *      Groups used for isolating a group of Organization level users with certain permisions.
         *      All Organization Permissions Groups are children of Organization Permission Group (organization-roles) 
         *      The name of each group should be dictated by and be descriptive of the role that a group of users will be performing
         *          If there are multiple words in the group name, then the words should be joined by a dash (-)
         *          Example: For Organization administrators the name might be "organization-administrator"   
         *  
         **/
    }
}