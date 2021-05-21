using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class AppInfoVM : BaseViewModel
    {
        public ICommand CancelCommand { get; private set; } // cancel dialog window

        public AppInfoVM()
        {
            CancelCommand = new DelegateCommand(Cancel);
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Blur = 3;
        }
        private void Cancel(object obj) // cancel dialog window
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed";
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Blur = 0;
        }

    }
}
