using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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
            Items = CollectionViewSource.GetDefaultView(Users);
            Items.Filter = Search;
        }
        private void ChangeUser(object obj)
        {
            USERS changedUser = Users.FirstOrDefault(n => n.USER_ID == (int)obj);
            var user = db.USERS.FirstOrDefault(n => n.USER_ID == changedUser.USER_ID);
            user = changedUser;
            user.ACCOUNT = changedUser.ACCOUNT;
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
            var marks = db.MARKS.Where(n => n.USER_ID == userToRemove.USER_ID);
            var newMarks = db.MARKS.RemoveRange(marks);
            db.USERS.Remove(userToRemove);
            db.SaveChangesAsync().GetAwaiter();
        }
        #region Filter
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AdminsVM), new PropertyMetadata("", TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as AdminsVM;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.Search;
            }
        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(AdminsVM), new PropertyMetadata(null));
        private bool Search(object obj)
        {
            bool result = true;
            USERS current = obj as USERS;

            if (current != null && !string.IsNullOrWhiteSpace(Text) && !current.EMAIL.ToLower().Contains(Text.ToLower()) && !current.NAME.ToLower().Contains(Text.ToLower()) && !current.SECOND_NAME.ToLower().Contains(Text.ToLower()))
            {
                result = false;
            }
            return result;
        }
        #endregion

    }
}
