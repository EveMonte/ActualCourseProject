using System;
using System.Windows.Input;
using Курсач.Singleton;
using Курсач.Methods;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

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
        private string code;
        #endregion

        #region Commands
        public ICommand OpenRegisterControlCommand { get; private set; } // Открыть user control с формой для регистрации
        public ICommand OpenWorkFrameCommand { get; private set; } // Успешная регистрация и открытие рабочего окна
        public ICommand ForgotPasswordCommand { get; private set; } // Успешная регистрация и открытие рабочего окна
        #endregion

        #region Command's Logic
        void OpenWorkFrame(object obj) // Открыть главную страницу
        {
            IntPtr passwordBSTR = default(IntPtr);
            string insecurePassword = "";


            // immediately use insecurePassword (in local variable) value after decrypting it:

            bool flag = true;
            try
            {
                if (Email == null || Email == "")
                {
                    App.notifier.ShowWarning("Поле для Email не должно быть пустым!");
                    if (Password == null)
                    {
                        App.notifier.ShowWarning("Поле для пароля не должно быть пустым!");
                    }
                    return;
                }
                else
                {
                    if (Password == null)
                    {
                        App.notifier.ShowWarning("Поле для пароля не должно быть пустым!");
                        return;
                    }
                }
                var users = App.db.USERS;
                foreach (USERS u in users)
                {
                    passwordBSTR = Marshal.SecureStringToBSTR(password);
                    insecurePassword = Marshal.PtrToStringBSTR(passwordBSTR);
                    if (SaltedHash.Verify(u.PASSWORD.Substring(44), u.PASSWORD.Substring(0, 44), insecurePassword) && u.EMAIL == Email)
                    {
                        flag = false;
                        currentUser = u;
                        App.currentUser = u;
                        if (u.ACCOUNT != "Пользователь")
                        {
                            AdminWindowSingleTone.GetInstance(new AdminVM());
                            AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new ListOfBooksAdminVM(AdminWindowSingleTone.GetInstance().AdminVM.Books);
                            AdminWindowSingleTone.GetInstance().AdminVM.ButtonVisibility = u.ACCOUNT == "Администратор" ? "Visible" : "Collapsed";
                            AdminWindow adminWindow = new AdminWindow();
                            App.CreateNotifier(adminWindow);
                            adminWindow.Show();
                        }
                        else
                        {
                            App.currentUser = u;
                            Workframe workframe = new Workframe();
                            App.CreateNotifier(workframe);
                            workframe.Show();
                            WorkFrameSingleTone.GetInstance(new WorkframeViewModel());
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
                if (flag)
                {
                    App.notifier.ShowWarning("Пользователя с таким Email или паролем не существует." +
                        "\nПроверьте правильность введенных данных");
                }
            }
            catch
            {
                insecurePassword = "";
                Password.Dispose();
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
            ForgotPasswordCommand = new DelegateCommand(ForgotPassword);        
        }

        private void ForgotPassword(object obj)
        {
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new ForgotPasswordVM();
        }
    }
}
