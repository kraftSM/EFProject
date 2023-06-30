using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFProject.Entities;

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

        public List<Book> GetAllBooks(AppContext db)
        {
            return db.Books.ToList();
        }
        public Book GetBook(int id, AppContext db)
        {
            Book book = db.Books.Where(x => x.Id == id).FirstOrDefault();
            return book;
        }

        public void AddBook(Book book, AppContext db)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }
        public void DeleteBook(Book book, AppContext db)
        {
            db.Books.Remove(book);
            db.SaveChanges();
        }
        public void DeleteBookById(int id)
        {
            using (AppContext app = new AppContext())
            {
                Book? book = app.Books.Where(x => x.Id == id).FirstOrDefault();
                app.Books.Remove(book);
                app.SaveChanges();
            }
        }
        public void UpdateBookInfoById(int Id)
        {
            using (AppContext app = new AppContext())
            {
                //создается сущность (Entities)
                Book bookData = app.Books.Where(i => i.Id == Id).FirstOrDefault();
                Console.Write("Введите отредактированный Год издания: ");
                bookData.Year = Convert.ToInt32(Console.ReadLine());
                app.Books.Update(bookData);
                //FrameWork при вызове SaveChanges() сам определит, что изменилось и произведет нужный SQLзапрос
                app.SaveChanges();
            }
        }
        public void AddBook()
        {
            Book book = new Book();
            Console.WriteLine("Введите Название новой книги: ");
            book.Name = Console.ReadLine();

            Console.WriteLine("Введите Жанр новой книги: ");
            book.Genre = Console.ReadLine();

            Console.WriteLine("Введите Фамилию автора новой книги: ");
            book.Autor = Console.ReadLine();

            Console.WriteLine("Введите Год издания книги: ");
            book.Year = Convert.ToInt32(Console.ReadLine());

            using (AppContext app = new AppContext())
            {
                Book newBook = app.Books.FirstOrDefault();
                app.Books.Add(book);
                app.SaveChanges();
            }
        }
        public void ShowBooksFromAuthor()
        {
            Console.Write("Введите фамилию автора: ");
            string strFilter = Console.ReadLine();
            //using (AppContext app = new AppContext())
            //{
            //    //List<Book> books = app.Books.Where(a => a.Autor == surname).ToList();
            //    //foreach (var item in books)
            //    //{
            //    //    Console.WriteLine(item.Name + " " + item.Autor);
            //    //}

            //}
            using (AppContext app = new AppContext())
            {
                List<Book> books = GetBooksByAutor(app, strFilter);
                ShowQueryResult(books);
            }

        }
        //8. список книг по заголовку (по возрастанию)
        public void SortBooksByName()
        {
            //using (AppContext app = new AppContext())
            //{
            //    List<Book> books = app.Books.ToList();
            //    var sort =
            //        from book in books
            //        orderby book.Name.ToUpper()
            //        select new { n = book.Name, a = book.Autor };
            //    foreach (var item in sort)
            //    {
            //        Console.WriteLine($"Название: {item.n}\tАвтор: {item.a}");
            //    }
            //}
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
        //1. кол-во книг по жанру в диапазоне годов издания
        public List<Book> GetAllBooksByGenge(AppContext db, string genre, int year1, int year2)
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
 
     
        //9. список книг по году издания книга (по убыванию)
        public List<Book> GetAllSortBooksByYear(AppContext db)
        {
            return db.Books.OrderByDescending(x => x.Year).ToList();
        }
    }
}
