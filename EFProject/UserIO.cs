using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject
{
    public class UserIO
    {
        public static string GetString(string Mes)
        { 
            Console.WriteLine(Mes);
            string res = Console.ReadLine();
            return res;
        }
        public static int GetInt(string Mes)
        {
            Console.WriteLine(Mes);
            int res = Convert.ToInt32(Console.ReadLine());
            return res;
        }
    }
}
