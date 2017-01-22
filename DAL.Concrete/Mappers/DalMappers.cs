using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Concrete.Mappers
{
    public static class DalMappers
    {
        public static User ToOrmUser(this DalUser user)
        {
            return new User
            {
                Id = user.Id,
                Login = user.Login,
                Firstname = user.Firstname,
                Secondname = user.Secondname,
                Thirdname = user.Thirdname,
                Email = user.Email,
                Password = user.Password,
                DateRegistered = user.DateRegistered
            };
        }

        public static DalUser ToDalUser(this User user)
        {
            return new DalUser
            {
                Id = user.Id,
                Login = user.Login,
                Firstname = user.Firstname,
                Secondname = user.Secondname,
                Thirdname = user.Thirdname,
                Email = user.Email,
                Password = user.Password,
                DateRegistered = user.DateRegistered,
                Roles = user.Roles.Select(role=>role.Rolename).ToArray(),
            };
        }
    }
}
