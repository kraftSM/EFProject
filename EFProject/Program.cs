using System;
using System.Data;
using EFProject.Entities;
using EFProject.Repositories;
using EFProject.Views;

namespace EFProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserRepository userRepository = new UserRepository();
            BookRepository bookRepository = new BookRepository();
            # region
            using (AppContext app = new AppContext())
                
            {
                app.Database.EnsureDeleted();
                app.Database.EnsureCreated();

                ////// Добавление информации
                ////var user1 = new User { Name = "Arthur", Email = "111@mil.ru" };
                var user1 = new User { Name = "Arthur", Role = "LibMan", Email = "111@lib.ru" };
                var user2 = new User { Name = "Klim", Role = "User", Email = "222@lib.ru" };
                var user3 = new User { Name = "Bruce", Role = "User", Email = "333@lib.ru" };
                var user4 = new User { Name = "Bob", Role = "User", Email = "444@lib.ru" };
                app.Users.Add(user1);
                ////app.Users.Add(user1);
                app.Users.Add(user2);
                //// Добавление нескольких пользователей
                app.Users.AddRange(user3, user4);
                app.SaveChanges();
                //  db.SaveChanges();

                Book book_1 = new Book { Name = "20000 лье под водой", Genre = "Научная фантастика", Autor = "Верн", Year = 1870, UserId = 2 };
                Book book_2 = new Book { Name = "Ведьмак", Genre = "Фэнтези", Autor = "Сапковский", Year = 1993, UserId = 3 };
                app.Books.AddRange(book_1, book_2);

                var book1 = new Book { Name = "Азбука", Autor = "Пушкин", Genre = "Учебник", Year = 2000, UserId = 1 };
                var book2 = new Book { Name = "Герой", Autor = "Лермон", Year = 2000, Genre = "Проза" };
                var book3 = new Book { Name = "Мертвые души", Autor = "Гоголь", Year = 2003, Genre = "Поэма" };
                var book4 = new Book { Name = "Евгений Онегин", Autor = "Пушкин", Year = 2007, Genre = "Поэма" };
                var book5 = new Book { Name = "Чиполлино", Autor = "Родари", Genre = "Сказки", Year = 1998 };
                var book6 = new Book { Name = "Золушка", Autor = "Перро", Genre = "Сказки", Year = 1990 };

                app.Books.AddRange(book1, book2, book3, book4, book5, book6);
                //db.Books.AddRange(book5, book6);




                app.SaveChanges();

                //UsersDebugInfo
                var UserResult = app.Users.ToList();

                foreach (var UserItm in UserResult)
                {
                    Console.Write($"{UserItm.ToString()}\t \n");
                }
                Console.Write($" \t --------------------------------- \n");
                //BooksDebugInfo
                var BooksResult = app.Books.ToList();

                foreach (var BooksItm in BooksResult)
                {
                    //Console.Write($"{BooksItm.Id}\t{BooksItm.Year}\t{BooksItm.Genre}\t {BooksItm.Autor}\t{BooksItm.Name}\n");
                    Console.Write($"{BooksItm.ToString()}\t \n");
                }
            }
            #endregion

            BookNavMenu bookView = new BookNavMenu(bookRepository);
            UserNavMenu userView = new UserNavMenu(userRepository);
            MainNavMenu mainView = new MainNavMenu(bookView, userView);
            mainView.ShowMainView();
        }
    }
}