using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFProject.Entities;

namespace EFProject.Repositories
{
    public class UserRepository
    {
        public void SelectAllUsers()
        {
            //Console.Write("---- SelectAllUsers ------ \n"); 
            using (AppContext app = new AppContext())
            {
                //List<Entities.User> users = app.Users.ToList();
                var ResultList = app.Users.ToList();
                Console.Write($"SelectAllUsers - users.Count: {ResultList.Count.ToString()} \n");
                foreach (var item in ResultList)                {

                    string e = item.Email != null ? item.Email : "не задано";
                    Console.WriteLine($"{item.ToString()}");  
                    //Console.WriteLine($"Id: {item.Id}\tИмя: {item.Name}\tEmail: {e}\tКниги на руках: ");
                }
            }
            Console.Write("---------- \n"); 
        }
        public void DeleteUserById(int id)
        {
            using (AppContext app = new AppContext())
            {
                Entities.User? user = app.Users.Where(x => x.Id == id).FirstOrDefault();
                app.Users.Remove(user);
                app.SaveChanges();
            }
        }
        public void AddUser()
        {
            Entities.User user = new Entities.User();
            Console.WriteLine("Введите Имя нового пользователя: ");
            user.Name = Console.ReadLine();

            Console.WriteLine("Введите Email нового пользователя: ");
            user.Email = Console.ReadLine();

            Console.WriteLine("Введите Role нового пользователя: ");
            user.Role = Console.ReadLine();

            using (AppContext app = new AppContext())
            {
                Entities.User newUser = app.Users.FirstOrDefault();
                app.Users.Add(user);
                app.SaveChanges();
            }
        }
        public void UpdateUserDataById(int Id)
        {
            using (AppContext app = new AppContext())
            {
                Entities.User userData = app.Users.Where(i => i.Id == Id).FirstOrDefault();
                Console.Write("Введите новое имя пользователя: ");
                userData.Name = Console.ReadLine();
                app.SaveChanges();
            }
        }
        public User GetUser(int id, AppContext db)
        {
            User user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            return user;
        }
        public List<User> GetAllUsers(AppContext db)
        {
            return db.Users.ToList();
        }
        public void AddUser(User user, AppContext db)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void DeleteUser(User user, AppContext db)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }
        public bool HasBooksInUser(AppContext db, Book book, User user)
        {
            return db.Users.Where(x => x == user && x.booksUser.Contains(book)).Count() > 0;
        }
        public int CountBooksInUser(AppContext db, User user)
        {
            return db.Users.Where(x => x == user).Select(x => x.booksUser).ToList().Count;
        }


    }
}
