using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using ORM;

namespace MvcPL.Providers
{
    public class UserRoleProvider : RoleProvider
    {
        private DbContext context = (DbContext)System.Web.Mvc.DependencyResolver
            .Current.GetService(typeof(DbContext));

        public override string[] GetRolesForUser(string email)
        {
            string[] roles = {};
            User user = context.Set<User>().FirstOrDefault(u => u.Email.Equals(email));

            if (user == null)
                return roles;

            Role role = user.Role;
            if (role != null)
                return roles = new [] {role.Rolename};

            return roles;
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            User user = context.Set<User>().FirstOrDefault(u => u.Email.Equals(email));
            if (user != null && user.Role.Rolename.Equals(roleName))
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