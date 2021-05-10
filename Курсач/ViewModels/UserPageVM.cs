using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    class UserPageVM : BaseViewModel
    {
        #region 
        USERS currentUser;
        LIBRARYEntities db = new LIBRARYEntities();
        private string mainCode;
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
        public ICommand SendMessageCommand { get; private set; }
        public ICommand ApplyCreditCardCommand { get; private set; }
        public ICommand ApplyEmailCommand { get; private set; }
        public ICommand ApplyPasswordCommand { get; private set; }


        public UserPageVM()
        {
            ApplyEmailCommand = new DelegateCommand(ApplyEmail);
            ApplyPasswordCommand = new DelegateCommand(ApplyPassword);
            ApplyCreditCardCommand = new DelegateCommand(ApplyCreditCard);
            SendMessageCommand = new DelegateCommand(SendMessage);
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            SECONDNAME = currentUser.SECOND_NAME;
            NAME = currentUser.NAME;
            ACCOUNT = currentUser.ACCOUNT;
            if (currentUser.SUBSCRIPTION == null)
            {
                Subscription = "Отсутствует";
            }
            else
            {
                Subscription = "Действует";
            }
            CreditCard = currentUser.CREDIT_CARD;
            YourBooks = db.YOUR_BOOKS.Where(n => n.USER_ID == currentUser.USER_ID).Count();
            Marks = db.MARKS.Where(n => n.USER_ID == currentUser.USER_ID).Count();
        }

        private void ApplyPassword(object obj)
        {
            IntPtr password1 = default(IntPtr);
            IntPtr password2 = default(IntPtr);
            IntPtr oldpassword = default(IntPtr);
            string oldinsecurePassword = "";
            string insecurePassword1 = "";
            string insecurePassword2 = "";
            string hash = "";
            try
            {
                password1 = Marshal.SecureStringToBSTR(FirstPassword);
                insecurePassword1 = Marshal.PtrToStringBSTR(password1);
                password2 = Marshal.SecureStringToBSTR(SecondPassword);
                insecurePassword2 = Marshal.PtrToStringBSTR(password2);
                if (mainCode != Code2)
                {
                    System.Windows.Forms.MessageBox.Show("Неверный код!");
                }
                else if (SaltedHash.Verify(currentUser.PASSWORD.Substring(44), currentUser.PASSWORD.Substring(0, 44), insecurePassword1) && insecurePassword1 != oldinsecurePassword)
                {
                    System.Windows.Forms.MessageBox.Show("Старый и новый пароль не должны совпадать!");
                }
                else if (insecurePassword1 != insecurePassword2)
                {
                    System.Windows.Forms.MessageBox.Show("Введенные пароли должны совпадать!");
                }
                else
                {

                    currentUser.PASSWORD = insecurePassword1;
                    USERS user = db.USERS.FirstOrDefault(n => n.USER_ID == currentUser.USER_ID);
                    SaltedHash sh = new SaltedHash(insecurePassword1);
                    user.PASSWORD = sh.Hash + sh.Salt;
                    db.SaveChangesAsync().GetAwaiter();
                    insecurePassword1 = "";
                    insecurePassword2 = "";
                    oldinsecurePassword = "";
                    FirstPassword.Dispose();
                    SecondPassword.Dispose();
                    OldPassword.Dispose();
                }
            }
            catch
            {
                insecurePassword1 = "";
                insecurePassword2 = "";
                oldinsecurePassword = "";
                FirstPassword.Dispose();
                SecondPassword.Dispose();
                OldPassword.Dispose();
            }
        }

        private void ApplyEmail(object obj)
        {
            if (mainCode != Code)
            {
                System.Windows.Forms.MessageBox.Show("Неверный код!");
            }
            else if (NewEmail != currentUser.EMAIL)
            {
                System.Windows.Forms.MessageBox.Show("Старый и новый Email не должны совпадать!");
            }
            else {
                foreach (USERS user in db.USERS)
                {
                    if(user.EMAIL == NewEmail)
                    {
                        System.Windows.Forms.MessageBox.Show("Пользователь с таким Email уже существует!");
                    }
                }
                currentUser.EMAIL = NewEmail;
                db.SaveChangesAsync().GetAwaiter();
            }

        }

        private void ApplyCreditCard(object obj)
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AddCreditCardVM();
        }

        private void SendMessage(object obj)
        {
            mainCode = MessageSender.GenerateCode();
            string message = $"С Вашей учетной записи поступил запрос на смену Email. Если это были Вы, то введите символьный код, расположенный ниже, в приложение:\n{mainCode}\nИначе свяжитесь с администрацией приложения!";
            MessageSender.SendEmailAsync(currentUser.EMAIL, mainCode, message, "Смена Email").GetAwaiter();
        }
    }
}
