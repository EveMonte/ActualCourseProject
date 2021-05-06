using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Курсач.ViewModels
{
    class UserPageVM : BaseViewModel
    {
        public BaseViewModel changePassword;

        private BaseViewModel ChangePassword
        {
            get
            {
                return changePassword;
            }
            set
            {
                changePassword = value;
                OnPropertyChanged("ChangePassword");
            }
        }
        public ICommand OpenPasswordBlockCommand { get; private set; }
        public UserPageVM()
        {
            OpenPasswordBlockCommand = new DelegateCommand(OpenPasswordBlock);

        }

        private void OpenPasswordBlock(object obj)
        {
            ChangePassword = new PasswordBlockVM();
        }
    }
}
