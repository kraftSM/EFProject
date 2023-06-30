using EFProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Views
{
    internal class UserNavMenu
    {
        private UserRepository _UserRepository;
        public UserNavMenu(UserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public void WorkWithUsers()
        {
            string choice = string.Empty;
            bool uExitRq = false;
            do
            {
                Console.WriteLine("WorkWithUsers: Entry Your TASK:" +
                    "\n'show or [1]': показать все данные;" +
                    "\n'add': добавить пользователя;" +
                    "\n'update': изменить имя;" +
                    "\n'delete': удалить по Id;" +
                    "\n'exit or [0] ' - выход: \n");
                Console.Write("Users:\t Введите команду: ");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    case nameof(Choice.show):                    
                        //Console.Write(" SelectAllUsers : "); 
                        _UserRepository.SelectAllUsers();
                        break;
                    case nameof(Choice.delete):
                        Console.Write("Введите Id пользователя для удаления: ");
                        _UserRepository.DeleteUserById(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case nameof(Choice.update):
                        Console.Write("Введите Id пользователя для редактирования: ");
                        _UserRepository.UpdateUserDataById(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case nameof(Choice.add):
                        _UserRepository.AddUser();
                        break;

                    case nameof(Choice.exit):
                    case "0":
                        uExitRq = true;
                        break;
                }
            } while ( !uExitRq);
        }
    }
}
