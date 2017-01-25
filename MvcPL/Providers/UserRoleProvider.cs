using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using BLL.Interfaces.Services;
using ORM;

namespace MvcPL.Providers
{
    public class UserRoleProvider : RoleProvider
    {
        private IUserService userService => (IUserService)System.Web.Mvc.DependencyResolver
            .Current.GetService(typeof(IUserService));

        public override string[] GetRolesForUser(string email)
        {
            string[] roles = {};
            var user = userService.GetUsersByPredicate(u => u.Email.Equals(email), 1)
                        .FirstOrDefault();

            if (user == null)
                return roles;
            return user.Roles.ToArray();
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            var user = userService.GetUsersByPredicate(u => u.Email.Equals(email), 1)
                        .FirstOrDefault();
            if (user != null && user.Roles.Count(role => role.Equals(roleName)) != 0)
            {
                return true;
            }
            return false;
        }

        #region Stubs
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}