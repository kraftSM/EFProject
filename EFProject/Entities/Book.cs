using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Entities
{
    public class Book
    {
        public int? Id { get; set; } //Primary Key
        public string Name { get; set; }
        public int Year { get; set; }
        public string Autor { get; set; }
        public string Genre { get; set; }

        public int? UserId { get; set; } //ForeignKey Key [UserId]
        
        public User? User { get; set; } //Link prop to [ForeignKey("UserId")]
        public string ToString()
        {
            return string.Format("Id:{0,-3} Autor:{1,-10} Name:{2,-16}\n  Year:{3,-5}  Genre:{4,-8} OnHand:{5,-3}", Id.ToString(), Autor, Name, Year, Genre, UserId.ToString());
        }
    }
}
