using System;
using System.Linq;
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

namespace Курсач.ViewModels
{
    class UserPageVM : BaseViewModel
    {
        #region 
        Notifier notifier;

        USERS currentUser; // Current user, get from Workframe
        private string mainCode; //Code which is generated here to confirm new password or email
        /////////// User's data 
        private string secondName;
        public string SECONDNAME
        {
            get
            {
                return secondName;
            }
            set
            {
                secondName = value;
                OnPropertyChanged("SECONDNAME");
            }
        }
        private string name;
        public string NAME
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("NAME");
            }
        }
        private string subscription;
        public string Subscription
        {
            get
            {
                return subscription;
            }
            set
            {
                subscription = value;
                OnPropertyChanged("Subscription");
            }
        }
        private string creditCard;
        public string CreditCard
        {
            get
            {
                return creditCard;
            }
            set
            {
                creditCard = value;
                OnPropertyChanged("CreditCard");
            }
        }
        private int yourBooks;
        public int YourBooks
        {
            get
            {
                return yourBooks;
            }
            set
            {
                yourBooks = value;
                OnPropertyChanged("YourBooks");
            }
        }
        private int marks;
        public int Marks
        {
            get
            {
                return marks;
            }
            set
            {
                marks = value;
                OnPropertyChanged("Marks");
            }
        }
        // secure keeping of passwords
        private SecureString oldPassword;
        public SecureString OldPassword
        {
            get
            {
                return oldPassword;
            }
            set
            {
                oldPassword = value;
                OnPropertyChanged("OldPassword");
            }
        }
        private SecureString firstPassword;
        public SecureString FirstPassword
        {
            get
            {
                return firstPassword;
            }
            set
            {
                firstPassword = value;
                OnPropertyChanged("FirstPassword");
            }
        }
        private SecureString secondPassword;
        public SecureString SecondPassword
        {
            get
            {
                return secondPassword;
            }
            set
            {
                secondPassword = value;
                OnPropertyChanged("SecondPassword");
            }
        }
        //email
        private string newEmail;
        public string NewEmail
        {
            get
            {
                return newEmail;
            }
            set
            {
                newEmail = value;
                OnPropertyChanged("NewEmail");
            }
        }
        //Type of account (User or Admin)
        private string account;
        public string ACCOUNT
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
                OnPropertyChanged("ACCOUNT");
            }
        }
        //Code written by user
        private string code;
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }
        private string code2;
        public string Code2
        {
            get
            {
                return code2;
            }
            set
            {
                code2 = value;
                OnPropertyChanged("Code2");
            }
        }
        #endregion

        #region Commands
        public ICommand SendPasswordMessageCommand { get; private set; } //Command which send an email with safety code to user's email
        public ICommand SendEmailMessageCommand { get; private set; } //Command which send an email with safety code to user's email
        public ICommand ApplyCreditCardCommand { get; private set; } // Open UserControl where user fills fields with credit card's data
        public ICommand ApplyEmailCommand { get; private set; } // Compare generated code with written code to confirm email change
        public ICommand ApplyPasswordCommand { get; private set; } // Compare generated code with written code to confirm password change
        public ICommand SignOutCommand { get; private set; } // Closes Workframe window and opens registration window

        #endregion

        //Constructor
        public UserPageVM(USERS currentUser)
        {
            this.currentUser = currentUser;
            //Delegate Command
            ApplyEmailCommand = new DelegateCommand(ApplyEmail);
            ApplyPasswordCommand = new DelegateCommand(ApplyPassword);
            ApplyCreditCardCommand = new DelegateCommand(ApplyCreditCard);
            SendPasswordMessageCommand = new DelegateCommand(ValidatePasswords);
            SendEmailMessageCommand = new DelegateCommand(ValidateEmail);
            SignOutCommand = new DelegateCommand(OpenRegistrationWindow);

            //////////////////////////////////////////////////////
            //Get necessary info about current user
            //currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser ?? AdminWindowSingleTone.GetInstance().AdminVM.currentUser;
            SECONDNAME = currentUser.SECOND_NAME;
            NAME = currentUser.NAME;
            ACCOUNT = currentUser.ACCOUNT;
            if (currentUser.SUBSCRIPTION == null) // Check status of subscription
            {
                Subscription = "Отсутствует";
            }
            else
            {
                Subscription = "Действует";
            }
            CreditCard = currentUser.CREDIT_CARD;
            YourBooks = App.db.YOUR_BOOKS.Where(n => n.USER_ID == currentUser.USER_ID).Count(); //Count books on your shelve
            Marks = App.db.MARKS.Where(n => n.USER_ID == currentUser.USER_ID).Count(); //Count your marks

            Workframe thisWin = null;
            foreach (Window win in Application.Current.Windows)
            {
                if (win is Workframe)
                {
                    thisWin = win as Workframe;
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

        private void ValidateEmail(object obj)
        {
            if(NewEmail == null || NewEmail == "")
            {
                notifier.ShowWarning("Поле для ввода нового Email не должно быть пустым!");
                return;
            }
            else if(NewEmail == currentUser.EMAIL)
            {
                notifier.ShowWarning("Старый и новый Email не должны совпадать");
                return;
            }
            else 
            { 
                foreach (USERS user in App.db.USERS)
                {
                    if (user.EMAIL == NewEmail)
                    {
                        notifier.ShowWarning("Пользователь с таким Email уже существует!");
                        return;
                    }
                    else
                    {
                        SendMessage();
                        break;
                    }
                }
            }
        }

        private void ValidatePasswords(object obj)
        {
            IntPtr password1 = default(IntPtr);
            IntPtr password2 = default(IntPtr);
            IntPtr oldpassword = default(IntPtr);
            string oldinsecurePassword = "";
            string insecurePassword1 = "";
            string insecurePassword2 = "";

            //Try-catch block where we get unsecure password, check it, hash and set to DB
            try
            {
                //Get unsecure passwords from SecureString
                password1 = Marshal.SecureStringToBSTR(FirstPassword);
                insecurePassword1 = Marshal.PtrToStringBSTR(password1);
                password2 = Marshal.SecureStringToBSTR(SecondPassword);
                insecurePassword2 = Marshal.PtrToStringBSTR(password2);
                oldpassword = Marshal.SecureStringToBSTR(OldPassword);
                oldinsecurePassword = Marshal.PtrToStringBSTR(oldpassword);
                if(insecurePassword1 == null || insecurePassword1 == "" || insecurePassword2 == null || insecurePassword2 == "" || oldinsecurePassword == null || oldinsecurePassword == "")
                {
                    notifier.ShowWarning("Поля для ввода паролей не должны быть пустыми");
                    return;
                }
                else if (SaltedHash.Verify(currentUser.PASSWORD.Substring(44), currentUser.PASSWORD.Substring(0, 44), insecurePassword1) && insecurePassword1 != oldinsecurePassword)
                {
                    notifier.ShowWarning("Старый и новый пароль не должны совпадать!");
                    return;
                }
                else if (insecurePassword1 != insecurePassword2)
                {
                    notifier.ShowWarning("Введенные пароли должны совпадать!");
                    return;
                }
                else
                {
                    SendMessage();
                }
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }

        #region Commands' Logic
        private void ApplyPassword(object obj) // Check passwords
        {
            //Data
            IntPtr password1 = default(IntPtr);
            IntPtr password2 = default(IntPtr);
            IntPtr oldpassword = default(IntPtr);
            string oldinsecurePassword = "";
            string insecurePassword1 = "";
            string insecurePassword2 = "";

            //Try-catch block where we get unsecure password, check it, hash and set to DB
            try
            {
                //Get unsecure passwords from SecureString
                password1 = Marshal.SecureStringToBSTR(FirstPassword);
                insecurePassword1 = Marshal.PtrToStringBSTR(password1);
                password2 = Marshal.SecureStringToBSTR(SecondPassword);
                insecurePassword2 = Marshal.PtrToStringBSTR(password2);
                oldpassword = Marshal.SecureStringToBSTR(OldPassword);
                oldinsecurePassword = Marshal.PtrToStringBSTR(oldpassword);

                if (mainCode != Code2) //Check written code
                {
                    notifier.ShowWarning("Неверный код!");
                }
                else
                {
                    USERS user = App.db.USERS.FirstOrDefault(n => n.USER_ID == currentUser.USER_ID);
                    SaltedHash sh = new SaltedHash(insecurePassword1); //Hashing password
                    user.PASSWORD = sh.Hash + sh.Salt;
                    currentUser.PASSWORD = user.PASSWORD;
                    App.db.SaveChangesAsync().GetAwaiter();

                    //Reset and dispose variables
                    insecurePassword1 = "";

                    insecurePassword2 = "";
                    oldinsecurePassword = "";
                    FirstPassword.Dispose();
                    SecondPassword.Dispose();
                    OldPassword.Dispose();
                }
            }

            catch // If we catch an exception remove variables
            {
                insecurePassword1 = "";
                insecurePassword2 = "";
                oldinsecurePassword = "";
                FirstPassword.Dispose();
                SecondPassword.Dispose();
                OldPassword.Dispose();
            }
            OpenRegistrationWindow(obj);
        }

        private void ApplyEmail(object obj) //Check email
        {
            if (mainCode != Code)
            {
                notifier.ShowWarning("Неверный код!");
                return;
            }
            else if (NewEmail == String.Empty || NewEmail == "")
            {
                notifier.ShowWarning("Поле для ввода нового Email не должно быть пустым!");
                return;
            }
            else if (NewEmail == currentUser.EMAIL)
            {
                notifier.ShowWarning("Старый и новый Email не должны совпадать!");
                return;
            }
            else 
            {
                foreach (USERS user in App.db.USERS)
                {
                    if(user.EMAIL == NewEmail)
                    {
                        notifier.ShowWarning("Пользователь с таким Email уже существует!");
                        return;
                    }
                }

                currentUser.EMAIL = NewEmail;
                App.db.SaveChangesAsync().GetAwaiter();
                OpenRegistrationWindow(obj);
            }
        }

        private void ApplyCreditCard(object obj) // Open AddCreditCart UserControl
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AddCreditCardVM();
        }

        private void SendMessage() // Send message to user
        {
            mainCode = MessageSender.GenerateCode();
            string message = $"С Вашей учетной записи поступил запрос на смену личных данных. Если это были Вы, то введите символьный код, расположенный ниже, в приложение:\n{mainCode}\nИначе свяжитесь с администрацией приложения!";
            MessageSender.SendEmailAsync(currentUser.EMAIL, mainCode, message, "Смена личных данных").GetAwaiter();
            notifier.ShowInformation("На вашу почту был отправлен код подтверждения для смены личных данных. Проверьте сообщения и введите код в поле.");
        }

        public void OpenRegistrationWindow(object obj) // Выйти в окно регистрации
        {            
            (new MainWindow()).Show();
            var windows = Application.Current.Windows;
            foreach(Window window in windows)
                if (window is Workframe || window is AdminWindow)
                    window.Close();
        }
        #endregion
    }
}
