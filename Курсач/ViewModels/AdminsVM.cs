using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AdminsVM()
        {
            Users = new ObservableCollection<USERS>(db.USERS.Where(n => n.ACCOUNT == "Редактор"));
        }
    }
}
