using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class AddCreditCardVM : BaseViewModel
    {
        public ICommand CloseUserPageCommand { get; private set; }
        public AddCreditCardVM()
        {
            CloseUserPageCommand = new DelegateCommand(Close);
        }

        private void Close(object obj)
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed";
        }
    }
}
