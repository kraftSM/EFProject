using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<Book> booksUser { get; set; }
        //public List<Book>? Books { get; set; } //Link prop to Books

        public string ToString()
        {
            return string.Format("Id:{0,-3} Name:{1,-6} Email:{2,-10}  Role:{3,-8}", Id.ToString(), Name, Email, Role);
        }
    }
}
