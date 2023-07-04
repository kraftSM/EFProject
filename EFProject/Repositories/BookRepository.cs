using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFProject.Entities;

using Microsoft.IdentityModel.Tokens;
using static System.Reflection.Metadata.BlobBuilder;

namespace EFProject.Repositories
{
    public class BookRepository
    {
        public void ShowQueryResult(List<Book> qvResult)
        {
            Console.Write($"{qvResult.Count.ToString()} Row(s) are selected \n");
            Console.Write("---------- \n");
            foreach (var item in qvResult)
            {
                string a = item.Autor != null ? item.Autor : "не задано";
                Console.WriteLine($"{item.ToString()}");
            }
            Console.Write("---------- \n");
        }
        public void SelectAllBooks()
        {
            using (AppContext app = new AppContext())
            {
                List<Book> books = GetAllBooks(app);
                ShowQueryResult(books);
            }
        }
        public void SelectBooksOnHands()
        {
            using (AppContext app = new AppContext())
            {
                List<Book> books = GetBooksOnUsers(app);
                ShowQueryResult(books);
            }
        }
        public void SelectBooksOnUser()
        {
            using (AppContext app = new AppContext())
            {
                int UserId = UserIO.GetInt("Выберите требуемого читателя (Введите UserId ): ");
                List<Book> books = GetBooksOnUser(app, UserId);
                ShowQueryResult(books);
            }
        }
        public void SelectBooksOnGenreYears()
        {
            using (AppContext app = new AppContext())
            {
                string strGenre = UserIO.GetString("Введите жанр книги: "); 
                int YearStr = UserIO.GetInt("Год издания: Введите начало периода: ");
                int YearEnd = UserIO.GetInt("Год издания: Введите конец периода: ");
                List<Book> books = GetAllBooksByGenre(app, strGenre, YearStr, YearEnd);
                ShowQueryResult(books);
            }
        }
        public void SelectNewBook()
        {
            using (AppContext app = new AppContext())
            {
                Book book = GetNewBook(app);
                Console.Write($"{book.ToString()}");
            }
        }
        public List<Book> GetAllBooks(AppContext db)
        {
            return db.Books.ToList();
        }
        //public Book GetBook(int id, AppContext db)
        //{
        //    Book book = db.Books.Where(x => x.Id == id).FirstOrDefault();
        //    return book;
        //}

        //public void AddBook(Book book, AppContext db)
        //{
        //    db.Books.Add(book);
        //    db.SaveChanges();
        //}
        //public void DeleteBook(Book book, AppContext db)
        //{
        //    db.Books.Remove(book);
        //    db.SaveChanges();
        //}
        public void DeleteBookById()
        {
            int UserId = UserIO.GetInt("Выберите требуемую книгу (Введите BookId ): "); 
            using (AppContext app = new AppContext())
            {
                Book? book = app.Books.Where(x => x.Id == UserId).FirstOrDefault();
                app.Books.Remove(book);
                app.SaveChanges();
            }
        }
        public void UpdateBookOnUserById()
        {
            int UserId;
            int BookId = UserIO.GetInt("Выберите требуемую книгу (Введите BookId ): ");

            using (AppContext app = new AppContext())
            {
                Book? book = app.Books.Where(x => x.Id == BookId).FirstOrDefault();
                Console.Write("Введите UserId читающего книгу (0 или Пусто - книга свободна): ");
                var conUser = Console.ReadLine();
                if (conUser.IsNullOrEmpty())
                { book.UserId = null; }
                else
                {
                    UserId = Convert.ToInt32(conUser);
                    if (UserId > 0) book.UserId = UserId;
                    else book.UserId = null;
                };
                app.SaveChanges();
            }
        }
        public void UpdateBookInfoById()
        {
            int UserId = UserIO.GetInt("Выберите требуемую книгу (Введите BookId ): "); 
            using (AppContext app = new AppContext())
            {
                Book bookData = app.Books.Where(i => i.Id == UserId).FirstOrDefault();
                Console.Write("Введите Year (Год издания): ");
                bookData.Year = Convert.ToInt32(Console.ReadLine());
                app.Books.Update(bookData);
                app.SaveChanges();
            }
        }
        public void AddBook()
        {
            Book book = new Book();
            book.Name = UserIO.GetString("Введите Название новой книги: ");
            book.Genre = UserIO.GetString("Введите Жанр новой книги: ");
            book.Autor = UserIO.GetString("Введите Фамилию автора новой книги: ");
            book.Year = UserIO.GetInt("Введите Год издания книги: ");

            using (AppContext app = new AppContext())
            {
                app.Books.Add(book);
                app.SaveChanges();
            }
        }
        public void ShowBooksFromAuthor()
        {
            string strFilter = UserIO.GetString("Введите Фамилию автора книги: ");
            using (AppContext app = new AppContext())
            {
                List<Book> books = GetBooksByAutor(app, strFilter);
                ShowQueryResult(books);
            }
        }
        public void ChekBooksByNameAuthor()
        {
            //Console.Write("Введите фамилию автора: ");
            //Console.Write("Введите Заголовок книги: ");
            string strAutor = UserIO.GetString("Введите Фамилию автора книги: ");            
            string strName = UserIO.GetString("Введите Название книги: ");
            using (AppContext app = new AppContext())
            {
               bool qvResult = HasBooksByAutorAndName(app, strAutor, strName);
                Console.WriteLine(qvResult);
                if (qvResult) Console.WriteLine( "книга {1}:{0} в наличии ", strAutor, strName);
                else Console.WriteLine("книга {1}:{0} отсутствует ", strAutor, strName);
            }

        }
        //8. список книг по заголовку (по возрастанию)
        public void SortBooksByName()
        {
          using (AppContext app = new AppContext())
            {
                List<Book> books = GetAllSortBooks(app);
                ShowQueryResult(books);
            }
        }
        public List<Book> GetAllSortBooks(AppContext db)
        {
            return db.Books.OrderBy(x => x.Name).ToList();
        }
        public List<Book> GetBooksOnUsers(AppContext db)
        {
            return db.Books.Where(x=>x.UserId > 0).OrderBy(x => x.Name).ToList();                
        }
        public List<Book> GetBooksOnUser(AppContext db, int UserId)
        {
            return db.Books.Where(x => x.UserId == UserId).OrderBy(x => x.Name).ToList(); 
        }
        //1. кол-во книг по жанру в диапазоне годов издания
        public List<Book> GetAllBooksByGenre(AppContext db, string genre, int year1, int year2)
        {
            return db.Books.Where(x => x.Genre == genre && x.Year > year1 && x.Year < year2).ToList();
        }
        //2. кол-во книг по автору
        public int CountBooksByAutor(AppContext db, string autor)
        {
            return db.Books.Where(x => x.Autor == autor).Count();
        }
        public List<Book> GetBooksByAutor(AppContext db, string autor)
        {
            return db.Books.Where(x => x.Autor == autor).ToList();
        }
        //3. кол-во книг по жанру
        public int CountBooksByGenre(AppContext db, string genre)
        {
            return db.Books.Where(x => x.Genre == genre).Count();
        }
        //4. наличие книги по автору и названию
        public bool HasBooksByAutorAndName(AppContext db, string autor, string name)
        {
            return db.Books.Where(x => x.Autor == autor && x.Name == name).Count() > 0;
        }
        //7. последняя книга (из вышедших)
        public Book GetNewBook(AppContext db)
        {
            return db.Books.Where(x => x.Year == db.Books.Select(x => x.Year).Max()).FirstOrDefault();
        }
     
        //9. список книг по году издания (по убыванию)
        public List<Book> GetAllSortBooksByYear(AppContext db)
        {
            return db.Books.OrderByDescending(x => x.Year).ToList();
        }
    }
}
