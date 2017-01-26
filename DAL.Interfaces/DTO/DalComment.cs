using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalComment : IEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
    }
}
