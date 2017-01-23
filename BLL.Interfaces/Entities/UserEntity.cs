using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Secondname { get; set; }
        public string Thirdname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateRegistered { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
