using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Input;
using Курсач.Methods;
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
        public SecureString firstPassword;
        public SecureString FirstPassword
        {
            get { return firstPassword; }
            set
            {
                firstPassword = value;
                OnPropertyChanged("FirstPassword");
            }
        }
        public SecureString secondPassword;
        public SecureString SecondPassword
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

        #region Commans
        public ICommand RegistrationCommand { get; private set; } // open form for sign on
        public ICommand OpenSignInCommand { get; private set; } // open form for sign in
        #endregion

        //Constructor
        public RegistrationViewModel()
        {
            RegistrationCommand = new DelegateCommand(OpenSendMessage);
            OpenSignInCommand = new DelegateCommand(OpenSignIn);
        }

        #region Commans' Logic
        private void OpenSendMessage(object obj)
        {
            IntPtr password1 = default(IntPtr);
            IntPtr password2 = default(IntPtr);
            string insecurePassword1 = "";
            string insecurePassword2 = "";
            string hash = "";
            try
            {
                password1 = Marshal.SecureStringToBSTR(FirstPassword);
                insecurePassword1 = Marshal.PtrToStringBSTR(password1);
                password2 = Marshal.SecureStringToBSTR(SecondPassword);
                insecurePassword2 = Marshal.PtrToStringBSTR(password2);
                sh = new SaltedHash(insecurePassword1);
                if (insecurePassword1 == insecurePassword2 && insecurePassword1.Length > 6)
                    hash += sh.Hash + sh.Salt;
                else return;
                insecurePassword1 = "";
                insecurePassword2 = "";
                FirstPassword.Dispose();
                SecondPassword.Dispose();
            }
            catch
            {
                insecurePassword1 = "";
                insecurePassword2 = "";
                FirstPassword.Dispose();
                SecondPassword.Dispose();
            }
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new SendMessageViewModel(new USERS { ACCOUNT = "User", EMAIL = Email, NAME = Name, PASSWORD = hash, SECOND_NAME = SecondName });

        }

        private void OpenSignIn(object obj)
        {
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new RegisterViewModel();
        }
        #endregion
    }
}
