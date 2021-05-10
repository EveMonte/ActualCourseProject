using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
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

        public ICommand RegistrationCommand { get; private set; }
        public ICommand OpenSignInCommand { get; private set; }
       
        public RegistrationViewModel()
        {
            RegistrationCommand = new DelegateCommand(OpenSendMessage);
            OpenSignInCommand = new DelegateCommand(OpenSignIn);
        }

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
    }
}
