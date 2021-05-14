using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Курсач.ViewModels
{
    public class AdminsVM : BaseViewModel
    {
        LIBRARYEntities db = new LIBRARYEntities();
        private ObservableCollection<USERS> users;

        public ObservableCollection<USERS> Users
        {
            get { return users; }
            set 
            { 
                users = value;
                OnPropertyChanged("Users");
            }
        }
        private BaseViewModel currentUserControl;

        public BaseViewModel CurrentUserControl
        {
            get { return currentUserControl; }
            set 
            { 
                currentUserControl = value;
                OnPropertyChanged("CurrentUserControl");
            }
        }
        public ICommand RemoveCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }

        public AdminsVM()
        {
            RemoveCommand = new DelegateCommand(RemoveUser);
            ChangeCommand = new DelegateCommand(ChangeUser);

            Users = new ObservableCollection<USERS>(db.USERS.Where(n => n.ACCOUNT == "Редактор"));
        }
        private void ChangeUser(object obj)
        {
            USERS changedUser = Users.FirstOrDefault(n => n.USER_ID == (int)obj);
            var user = db.USERS.FirstOrDefault(n => n.USER_ID == changedUser.USER_ID);
            user = changedUser;
            user.ACCOUNT = changedUser.ACCOUNT;
            var sub = db.SUBSCRIPTIONS.FirstOrDefault(n => n.SUBSCRIPTION_ID == changedUser.SUBSCRIPTION);
            db.SaveChanges();
            if (sub != null)
            {
                sub.SUBSCRIPTION_DATE = changedUser.SUBSCRIPTION_DATE;
                sub.LENGTH = changedUser.SUBSCRIPTION_LENGTH;
            }
            else
            {
                SUBSCRIPTIONS subscr = new SUBSCRIPTIONS();
                subscr.SUBSCRIPTION_DATE = changedUser.SUBSCRIPTION_DATE;
                subscr.LENGTH = changedUser.SUBSCRIPTION_LENGTH;
                db.SUBSCRIPTIONS.Add(subscr);
                changedUser.SUBSCRIPTION = subscr.SUBSCRIPTION_ID;
            }
            db.SaveChangesAsync().GetAwaiter();
        }

        private void RemoveUser(object obj)
        {
            USERS userToRemove = db.USERS.FirstOrDefault(n => n.USER_ID == (int)obj);
            Users.Remove(userToRemove);
            var shelf = db.YOUR_BOOKS.Where(n => n.USER_ID == userToRemove.USER_ID);
            db.YOUR_BOOKS.RemoveRange(shelf);
            var basket = db.BASKETS.Where(n => n.USER_ID == userToRemove.USER_ID);
            db.BASKETS.RemoveRange(basket);
            var sub = db.SUBSCRIPTIONS.FirstOrDefault(n => n.SUBSCRIPTION_ID == userToRemove.SUBSCRIPTION);
            db.YOUR_BOOKS.RemoveRange(shelf);
            db.USERS.Remove(userToRemove);
            db.SaveChangesAsync().GetAwaiter();
        }
    }
}
