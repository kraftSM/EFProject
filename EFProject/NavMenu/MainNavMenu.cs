using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Views
{
    public enum Choice
    {
        show,
        add,
        update,
        delete,
        sba,
        sort,
        exit
    }
    internal class MainNavMenu
    {

        private BookNavMenu _bookView;
        private UserNavMenu _userView;
        public MainNavMenu(BookNavMenu bookView, UserNavMenu userView)
        {
            _bookView = bookView;
            _userView = userView;
        }


        public void ShowMainView()
        {
            string choice = string.Empty;
            bool mExitRq = false;
            do
            {
                Console.Write("Main: Work with Books [1] Users [2]  'exit or [0] ' - выход)?: ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        _bookView.WorkWithBooks();
                        break;
                    case "2":
                        _userView.WorkWithUsers();
                        break;

                    case nameof(Choice.exit):
                    case "0":
                        mExitRq = true;
                        break;

                }
            } 
            while (!mExitRq);
        }
    }
}
