using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFProject.Repositories;

namespace EFProject.Views
{
    internal class BookNavMenu
    {
        private BookRepository _BookRepository;
        public BookNavMenu(BookRepository bookRepository)
        {
            _BookRepository = bookRepository;
        }
        public void WorkWithBooks()
        {
            string choice = string.Empty;
            bool bExitRq = false;
            do
            {
                Console.WriteLine("WorkWithBooks: Entry Your TASK" +
                    "\n'show or [1]': show ALL book data;" +
                    "\n'add': add Book;" +
                    "\n'update': изменить название;"+
                    "\n'sgyy or [2]': \t Select BooksOnGenreYears;" +
                    "\n'sba or [3]': \tsort book by по автору;" +
                    "\n'cbna or [4]': \tcheck book by по заголовку и автору ;" +
                    "\n'sort or [5]': \tsort book by Название ;" +
                    "\n'boh or [6]': books on Hand ;" +
                    "\n'bou or [7]': books on Users;" +
                    "\n'ubn or [8]': Update books on User(UserId) ;" +
                    "\n'snb or [9]': select New book ;" + 
                    "\n'delete': delete by Id;" +
                    "\n'exit or [0] ' - выход: выход\n");
                Console.Write("Book:\t Введите команду: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":                    
                    case nameof(Choice.show):
                        _BookRepository.SelectAllBooks();
                        break;
                    case nameof(Choice.delete):
                        //Console.Write("Введите Id книги для удаления: ");
                        //_BookRepository.DeleteBookById(Convert.ToInt32(Console.ReadLine()));
                        //Console.Write("Введите Id книги для удаления: ");
                        _BookRepository.DeleteBookById();
                        break;
                    case nameof(Choice.update):
                        //Console.Write("Введите Id книги для редактирования: ");
                        //_BookRepository.UpdateBookInfoById(Convert.ToInt32(Console.ReadLine()));
                        _BookRepository.UpdateBookInfoById();
                        break;
                    case nameof(Choice.add):
                        _BookRepository.AddBook();
                        break;
                    case "2":
                    case nameof(Choice.sgyy):
                        _BookRepository.SelectBooksOnGenreYears();                         
                        break;
                    case "3":                    
                    case nameof(Choice.sba):
                        _BookRepository.ShowBooksFromAuthor();
                        break;
                    case "4":
                    case nameof(Choice.cbna):
                        _BookRepository.ChekBooksByNameAuthor();
                        break;                        
                    case "5":                    
                    case nameof(Choice.sort):
                        _BookRepository.SortBooksByName();
                        break;
                    
                    case "6":
                    case nameof(Choice.boh):
                        _BookRepository.SelectBooksOnHands();
                        break;
                    case "7":
                    case nameof(Choice.bou):
                        _BookRepository.SelectBooksOnUser();
                        break;
                    case "8":
                    case nameof(Choice.ubn):
                        //Console.Write("Введите Id книги для выдачи/возврата: ");
                        //_BookRepository.UpdateBookOnUserById(Convert.ToInt32(Console.ReadLine()));
                        _BookRepository.UpdateBookOnUserById();
                        break;
                    case "9":                    
                    case nameof(Choice.snb):
                        //Console.Write("Введите Id книги для выдачи/возврата: ");
                        _BookRepository.SelectNewBook();
                        break;                        

                    case nameof(Choice.exit):
                    case "0":
                        bExitRq = true;
                        break;
                }
            } while( !bExitRq);
        }
    }
}
