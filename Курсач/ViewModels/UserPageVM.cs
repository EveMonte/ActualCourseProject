using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    class UserPageVM : BaseViewModel
    {
        #region Data
        LIBRARYEntities db = new LIBRARYEntities();
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
        private string oldPassword;
        public string OldPassword
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
        #endregion
        public ICommand SendMessageCommand { get; private set; }
        public UserPageVM()
        {
            SendMessageCommand = new DelegateCommand(SendMessage);
            USERS currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            SECONDNAME = currentUser.SECOND_NAME;
            NAME = currentUser.NAME;
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

        private void SendMessage(object obj)
        {
            string sfsf = "";
            SaltedHash s = new SaltedHash(OldPassword);
            sfsf += s.Hash + "\n";
            sfsf += s.Salt + "\n";
            sfsf += SaltedHash.Verify(s.Salt, s.Hash, OldPassword);
            System.Windows.Forms.MessageBox.Show(sfsf);
        }
    }
}
