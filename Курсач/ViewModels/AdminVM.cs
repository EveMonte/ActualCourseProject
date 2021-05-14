using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Курсач.ViewModels
{
    public class AdminVM : BaseViewModel
    {
        private BaseViewModel currentPageViewModel;

        public BaseViewModel CurrentPageViewModel
        {
            get { return currentPageViewModel; }
            set 
            { 
                currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        public ICommand OpenBooksCommand { get; private set; }
        public ICommand OpenAdminsCommand { get; private set; }
        public ICommand OpenUsersCommand { get; private set; }
        public ICommand OpenUserCommand { get; private set; }
        public ICommand OpenSettingsCommand { get; private set; }
        public AdminVM()
        {
            OpenBooksCommand = new DelegateCommand(OpenBooks);
            OpenAdminsCommand = new DelegateCommand(OpenAdmins);
            OpenUsersCommand = new DelegateCommand(OpenUsers);
            OpenUserCommand = new DelegateCommand(OpenUser);
            OpenSettingsCommand = new DelegateCommand(OpenSettings);
            CurrentPageViewModel = new ListOfBooksAdminVM();
        }

        private void OpenUser(object obj)
        {
            CurrentPageViewModel = new UserPageVM();
        }

        private void OpenSettings(object obj)
        {
            CurrentPageViewModel = new SettingsVM();
        }

        private void OpenUsers(object obj)
        {
            CurrentPageViewModel = new UsersPageVM();
        }

        private void OpenAdmins(object obj)
        {
            CurrentPageViewModel = new AdminsVM();
        }

        private void OpenBooks(object obj)
        {
            CurrentPageViewModel = new ListOfBooksAdminVM();
        }
    }
}
