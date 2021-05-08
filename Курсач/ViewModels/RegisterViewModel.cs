using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач;
using Курсач.Models;
using System.Data.SQLite;
using System.Data.Entity;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using Курсач.Commands;
using Курсач.Singleton;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using Курсач.Methods;

namespace Курсач.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Data
        SaltedHash sh;
        USERS currentUser;
        public USERS CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        #endregion

        #region Commands
        public ICommand OpenRegisterControlCommand { get; private set; } // Открыть user control с формой для регистрации
        public ICommand OpenWorkFrameCommand { get; private set; } // Успешная регистрация и открытие рабочего окна
        #endregion

        #region Command's Logic
        public bool CanExecute(object parameter) // Можно ли войти
        {
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var users = db.USERS;
                foreach (USERS u in users)
                {
                    if (SaltedHash.Verify(u.PASSWORD.Substring(44), u.PASSWORD.Substring(0, 44), Password) && u.EMAIL == Email)
                    {
                        currentUser = u;
                        return true;
                    }
                }
            }
            return false;
        }

        void OpenWorkFrame(object obj) // Открыть главную страницу
        {
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var users = db.USERS;
                foreach (USERS u in users)
                {
                    if (SaltedHash.Verify(u.PASSWORD.Substring(44), u.PASSWORD.Substring(0, 44), Password) && u.EMAIL == Email)
                    {
                        currentUser = u;
                        WorkFrameSingleTone.GetInstance(new WorkframeViewModel(currentUser));
                        Workframe workframe = new Workframe();
                        workframe.Show();
                        break;
                    }
                }
            }

        }
        void OpenRegisterWindow(object obj) // Открыть UserControl с регистрацией
        {
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new RegistrationViewModel();
        }
        #endregion
        public RegisterViewModel()
        {
            OpenRegisterControlCommand = new DelegateCommand(OpenRegisterWindow);
            OpenWorkFrameCommand = new DelegateCommand(OpenWorkFrame);
        }
    }
}
