using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using ORM;

namespace MvcPL.Providers
{
    public class UserMembershipProvider : MembershipProvider
    {
        private DbContext context = (DbContext)System.Web.Mvc.DependencyResolver
            .Current.GetService(typeof(DbContext));

        public MembershipUser CreateUser(string login, string firstname,
            string secondname, string thirdname, string email, string password)
        {
            MembershipUser membershipUser = GetUser(email, false);
            
            if (membershipUser != null)
            {
                return null;
            }

            User user = new User
            {
                Login = login,
                Firstname = firstname,
                Secondname = secondname,
                Thirdname = thirdname,
                Email = email,
                Password = Crypto.HashPassword(password),
                DateRegistered = DateTime.Now,
            };

            Role role = context.Set<Role>().FirstOrDefault(r => r.Rolename.Equals("User"));

            if (role != null)
            {
                user.RoleId = role.id;
            }

            context.Set<User>().Add(user);
            context.SaveChanges();
            membershipUser = GetUser(email, false);
            return membershipUser;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.Email.Equals(email));
            if (user == null)
                return null;
            return new MembershipUser("UserMembershipProvider", user.Login, null, 
                user.Email, null, null, false, false, user.DateRegistered, 
                DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 
                DateTime.MinValue
                );
        }

        public override bool ValidateUser(string email, string password)
        {
            User user = context.Set<User>().FirstOrDefault(u => u.Email.Equals(email));
            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }
            return false;
        }

        public override bool DeleteUser(string email, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        #region Stubs

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

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

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}