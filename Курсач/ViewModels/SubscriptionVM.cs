using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class SubscriptionVM : BaseViewModel
    {
        #region Data & Commands
        public ICommand YearSubscriptionCommand { get; private set; }
        public ICommand MonthSubscriptionCommand { get; private set; }
        #endregion

        //Constructor
        public SubscriptionVM()
        {
            YearSubscriptionCommand = new DelegateCommand(YearSubscription);
            MonthSubscriptionCommand = new DelegateCommand(MonthSubscription);
        }

        #region Commands' Logic
        private void MonthSubscription(object obj)
        {
            if(App.currentUser.CREDIT_CARD != null)
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new BaseDialogWindowVM(new ConfirmSubscriptionVM(1));
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
            }
            else
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AddCreditCardVM();
            }
        }

        private void YearSubscription(object obj)
        {
            if (App.currentUser.CREDIT_CARD != null)
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new BaseDialogWindowVM(new ConfirmSubscriptionVM(12));
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
            }
            else
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AddCreditCardVM();
            }
        }
        #endregion
    }
}
