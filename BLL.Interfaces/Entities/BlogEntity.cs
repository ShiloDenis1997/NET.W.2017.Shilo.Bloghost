using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class BlogEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public DateTime DateStarted { get; set; }
        public int UserId { get; set; }
    }
}
