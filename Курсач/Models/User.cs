using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Курсач.Models;
using System.Data.SQLite;
using System.Data.Entity;

namespace Курсач.Models
{
    [Table("Users")]
    public class User : INotifyPropertyChanged
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string password;
        public string PASSWORD
        {
            get { return password; }
            set { password = value;
                OnPropertyChanged("Password");
            }
        }
        [Required]
        public string email;
        public string EMAIL
        {
            get { return email; }
            set { email = value;
                OnPropertyChanged("Email");
            }
        }
        [Required]
        private string subscription;
        public string SUBSCRIPTION
        {
            get { return subscription; }
            set { subscription = value; }
        }
        [Required]
        private string account;
        public string ACCOUNT
        {
            get { return account; }
            set { account = value;
                OnPropertyChanged("ACCOUNT");
            }
        }
        [Required]
        private string secondName;
        public string SECOND_NAME
        {
            get { return secondName; }
            set { secondName = value;
                OnPropertyChanged("SECOND_NAME");
            }
        }
        [Required]
        private string name;
        public string NAME
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged("NAME");
            }
        }

        private string creditCard;
        public string CREDIT_CARD
        {
            get { return creditCard; }
            set
            {
                creditCard = value;
                OnPropertyChanged("CREDIT_CARD");
            }
        }
        /*private SQLiteConnection connection;
        private SQLiteCommand command;
        private string baseName = "LibraryDataBase.db";*/
        public User()
        {

        }
        public User(string email, string password, string secondName, string name, string typeOfUser, string Subscription = "User")
        {
            this.EMAIL = email;
            this.PASSWORD = password;
            this.SECOND_NAME = secondName;
            this.NAME = name;
            this.ACCOUNT = typeOfUser;
            this.SUBSCRIPTION = Subscription;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static List<User> GetUsersFromDataBase()
        {
            using (ApplicationContext db = new ApplicationContext()) 
            {
                db.Users.Load();
                return db.Users.ToList();

            }
        }
    }
}
