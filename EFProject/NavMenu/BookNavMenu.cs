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
                    "\n'update': изменить название;" +
                    "\n'sba or [4]': \tsort book by по автору;" +
                    "\n'sort or [5]': \tsort book by Название ;" +
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
                        Console.Write("Введите Id книги для удаления: ");
                        _BookRepository.DeleteBookById(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case nameof(Choice.update):
                        Console.Write("Введите Id книги для редактирования: ");
                        _BookRepository.UpdateBookInfoById(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case nameof(Choice.add):
                        _BookRepository.AddBook();
                        break;
                    case "5":                    
                    case nameof(Choice.sort):
                        _BookRepository.SortBooksByName();
                        break;
                    case "4":                    
                    case nameof(Choice.sba):
                        _BookRepository.ShowBooksFromAuthor();
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
