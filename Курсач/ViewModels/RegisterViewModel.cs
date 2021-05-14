using System;
using System.Windows.Input;
using Курсач.Singleton;
using Курсач.Methods;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows;

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
        private SecureString password;
        public SecureString Password
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
        void OpenWorkFrame(object obj) // Открыть главную страницу
        {
            IntPtr passwordBSTR = default(IntPtr);
            string insecurePassword = "";


            // immediately use insecurePassword (in local variable) value after decrypting it:
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var users = db.USERS;
                foreach (USERS u in users)
                {
                    try
                    {
                        passwordBSTR = Marshal.SecureStringToBSTR(password);
                        insecurePassword = Marshal.PtrToStringBSTR(passwordBSTR);

                        if (SaltedHash.Verify(u.PASSWORD.Substring(44), u.PASSWORD.Substring(0, 44), insecurePassword) && u.EMAIL == Email)
                        {
                            currentUser = u;
                            if(u.ACCOUNT == "Администратор")
                            {
                                AdminWindowSingleTone.GetInstance(new AdminVM()).AdminVM.currentUser = u;
                                AdminWindow adminWindow = new AdminWindow();
                                adminWindow.Show();
                            }
                            else
                            {
                                WorkFrameSingleTone.GetInstance(new WorkframeViewModel(u)).WorkframeViewModel.currentUser = u;
                                Workframe workframe = new Workframe();
                                workframe.Show();
                            }
                            var windows = Application.Current.Windows;
                            foreach (Window window in windows)
                                if (window != null && window is MainWindow)
                                    window.Close();
                            Password.Dispose();
                            insecurePassword = null;
                            break;
                        }
                    }

                    catch
                    {
                        insecurePassword = "";
                        Password.Dispose();
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
