using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Курсач.Methods;
using Курсач.Singleton;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Курсач.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        #region Data
        SaltedHash sh;
        LIBRARYEntities db = new LIBRARYEntities();
        Notifier notifier;
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
        public SecureString firstPassword = new SecureString();
        public SecureString FirstPassword
        {
            get { return firstPassword; }
            set
            {
                firstPassword = value;
                OnPropertyChanged("FirstPassword");
            }
        }
        public SecureString secondPassword = new SecureString();
        public SecureString SecondPassword
        {
            get { return secondPassword; }
            set
            {
                secondPassword = value;
                OnPropertyChanged("SecondPassword");
            }
        }
        [Required(ErrorMessage ="Поле Email не может быть пустым")]
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

        private string passwordValidation;

        public string PasswordValidation
        {
            get { return passwordValidation; }
            set 
            { 
                passwordValidation = value;
                OnPropertyChanged("PasswordValidation");
            }
        }

        private string secondPasswordValidation;

        public string SecondPasswordValidation
        {
            get { return secondPasswordValidation; }
            set 
            { 
                secondPasswordValidation = value;
                OnPropertyChanged("SecondPasswordValidation");
            }
        }

        private string secondNameValidation;

        public string SecondNameValidation
        {
            get { return secondNameValidation; }
            set 
            { 
                secondNameValidation = value;
                OnPropertyChanged("SecondNameValidation");
            }
        }

        private string nameValidation;

        public string NameValidation
        {
            get { return nameValidation; }
            set 
            {
                nameValidation = value;
                OnPropertyChanged("NameValidation");
            }
        }

        private string emailValidation;

        public string EmailValidation
        {
            get { return emailValidation; }
            set 
            { 
                emailValidation = value;
                OnPropertyChanged("EmailValidation");
            }
        }


        #endregion

        #region Commands
        public ICommand RegistrationCommand { get; private set; } // open form for sign on
        public ICommand OpenSignInCommand { get; private set; } // open form for sign in
        #endregion

        //Constructor
        public RegistrationViewModel()
        {
            RegistrationCommand = new DelegateCommand(OpenSendMessage);
            OpenSignInCommand = new DelegateCommand(OpenSignIn);
            MainWindow thisWin = null;
            foreach (Window win in Application.Current.Windows)
            {
                if (win is MainWindow)
                {
                    thisWin = win as MainWindow;
                }
            }
            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        #region Commands' Logic
        private void OpenSendMessage(object obj)
        {
            bool flag = true;
            Regex complexPassword = new Regex("(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}");
            IntPtr password1 = default(IntPtr);
            IntPtr password2 = default(IntPtr);
            string insecurePassword1 = "";
            string insecurePassword2 = "";
            string hash = "";
            try
            {
                if (App.db.USERS.FirstOrDefault(n => n.EMAIL == Email) != null)
                {
                    notifier.ShowWarning("Пользователь с таким Email уже существует");
                    return;
                }
                
                password1 = Marshal.SecureStringToBSTR(FirstPassword);
                insecurePassword1 = Marshal.PtrToStringBSTR(password1);
                password2 = Marshal.SecureStringToBSTR(SecondPassword);
                insecurePassword2 = Marshal.PtrToStringBSTR(password2);
                sh = new SaltedHash(insecurePassword1);
                if (!complexPassword.IsMatch(insecurePassword1))
                {
                    PasswordValidation = "Пароль должен быть в длину более 6 символов, содержать цифры, спец символы, латинские буквы в верхнем и нижнем регистре";
                    flag = false;
                }
                else
                {
                    PasswordValidation = "";
                }

                if (insecurePassword1 != insecurePassword2)
                {
                    SecondPasswordValidation = "Введенные пароли должны совпадать";
                    flag = false;
                }
                else
                    SecondPasswordValidation = "";

                if(Email == null || Email == "")
                {
                    EmailValidation = "Это поле не должно быть пустым";
                    flag = false;
                }
                else
                {
                    EmailValidation = "";
                }

                if (SecondName == null || SecondName == "")
                {
                    SecondNameValidation = "Это поле не должно быть пустым";
                    flag = false;
                }
                else
                {
                    SecondNameValidation = "";
                }

                if (Name == null || Name == "")
                {
                    NameValidation = "Это поле не должно быть пустым";
                    flag = false;
                }
                else
                {
                    NameValidation = "";
                }

                if (!flag) return;

                if (insecurePassword1 == insecurePassword2 && flag)
                {
                    hash += sh.Hash + sh.Salt;
                    SecondPasswordValidation = "";
                }

                insecurePassword1 = "";
                insecurePassword2 = "";
                if(FirstPassword != null)
                {
                    FirstPassword.Dispose();
                }
                if (SecondPassword != null)
                {
                    SecondPassword.Dispose();        
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                insecurePassword1 = "";
                insecurePassword2 = "";
                if (FirstPassword != null)
                {
                    FirstPassword.Dispose();
                }
                if (SecondPassword != null)
                {
                    SecondPassword.Dispose();
                }
            }
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new SendMessageViewModel(new USERS { ACCOUNT = "Пользователь", EMAIL = Email, NAME = Name, PASSWORD = hash, SECOND_NAME = SecondName });

        }

        private void OpenSignIn(object obj)
        {
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new RegisterViewModel();
        }
        #endregion
    }
}
