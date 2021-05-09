using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Курсач.Commands;
using Курсач.Methods;
using Курсач.Models;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        #region Data
        SaltedHash sh;
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        List<USERS> listOfUsers;
        public string firstPassword = "";
        public string FirstPassword
        {
            get { return firstPassword; }
            set
            {
                firstPassword = value;
                OnPropertyChanged("FirstPassword");
            }
        }
        public string secondPassword = "";
        public string SecondPassword
        {
            get { return secondPassword; }
            set
            {
                secondPassword = value;
                OnPropertyChanged("SecondPassword");
            }
        }
        public string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        public ICommand RegistrationCommand { get; private set; }
        public ICommand OpenSignInCommand { get; private set; }

        // Если пользователя с таким email не существует, введенные пароли совпадают и длина паролей больше 6 символов
        bool CanExecute(object parametr)
        {
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var users = db.USERS;
                foreach (USERS u in users)
                {
                    if (u.EMAIL != Email && firstPassword == secondPassword && firstPassword.Length > 6)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        
        public RegistrationViewModel()
        {
            RegistrationCommand = new DelegateCommand(OpenSendMessage);
            OpenSignInCommand = new DelegateCommand(OpenSignIn);
        }

        private void OpenSendMessage(object obj)
        {
            string hash = "";
            sh = new SaltedHash(FirstPassword);
            hash += sh.Hash + sh.Salt;
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new SendMessageViewModel(new USERS { ACCOUNT = "User", EMAIL = Email, NAME = Name, PASSWORD = hash, SECOND_NAME = SecondName });

        }

        private void OpenSignIn(object obj)
        {
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new RegisterViewModel();
        }
    }
}
