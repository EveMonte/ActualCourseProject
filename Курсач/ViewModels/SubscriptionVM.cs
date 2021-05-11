using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class SubscriptionVM : BaseViewModel
    {
        #region Data & Commands
        LIBRARYEntities db = new LIBRARYEntities();
        private USERS currentUser;
        public ICommand YearSubscriptionCommand { get; private set; }
        public ICommand MonthSubscriptionCommand { get; private set; }
        #endregion

        //Constructor
        public SubscriptionVM()
        {
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            YearSubscriptionCommand = new DelegateCommand(YearSubscription);
            MonthSubscriptionCommand = new DelegateCommand(MonthSubscription);
        }

        #region Commands' Logic
        private void MonthSubscription(object obj)
        {
            if(currentUser.CREDIT_CARD != null)
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new ConfirmSubscriptionVM(1);
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
            }
            else
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AddCreditCardVM();
            }
        }

        private void YearSubscription(object obj)
        {
            if (currentUser.CREDIT_CARD != null)
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new ConfirmSubscriptionVM(12);
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
