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
    public class ConfirmSubscriptionVM : BaseViewModel
    {
        LIBRARYEntities db = new LIBRARYEntities();
        private string infoText;
        public string InfoText
        {
            get
            {
                return infoText;
            }
            set
            {
                infoText = value;
                OnPropertyChanged("InfoText");
            }
        }
        public ICommand ConfirmSubscriptionCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private USERS currentUser;
        private int Month;
        public ConfirmSubscriptionVM(int type)
        {
            Month = type;
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            ConfirmSubscriptionCommand = new DelegateCommand(BuyTheSubscription);
            CancelCommand = new DelegateCommand(Cancel);
            string firstText = type == 1 ? "1 месяц" : "12 месяцев";
            string secondText = type == 1 ? "8.99" : "85.99";
            InfoText = String.Format($"Подтвердите покупку абонемента на {firstText}. С вашей карты будет списано {secondText}$.");
        }

        private void Cancel(object obj)
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed";

        }

        private void BuyTheSubscription(object obj)
        {
            var a = db.SUBSCRIPTIONS.FirstOrDefault(n => n.SUBSCRIPTION_ID == currentUser.SUBSCRIPTION);
            if (db.SUBSCRIPTIONS.Where(n => n.SUBSCRIPTION_ID == currentUser.SUBSCRIPTION).FirstOrDefault() == null)
            {
                SUBSCRIPTIONS sub = new SUBSCRIPTIONS();
                sub.SUBSCRIPTION_DATE = DateTime.Now;
                sub.LENGTH = ++Month;
                db.SUBSCRIPTIONS.Add(sub);
                USERS user = db.USERS.Where(n => n.USER_ID == currentUser.USER_ID).FirstOrDefault();
                user.SUBSCRIPTION = sub.SUBSCRIPTION_ID;
                db.SaveChanges();
            }
            else if ((DateTime.Now.Month - a.SUBSCRIPTION_DATE.Month) + 12 * (DateTime.Now.Year - a.SUBSCRIPTION_DATE.Year) < a.LENGTH)
            {
                a.LENGTH += 1;
                db.SaveChanges();
            }
            else
            {
                a.LENGTH = Month;
                a.SUBSCRIPTION_DATE = DateTime.Now;
                db.SaveChanges();
            }
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser.SUBSCRIPTION = db.USERS.FirstOrDefault(n => n.USER_ID == currentUser.USER_ID).SUBSCRIPTION;
            string money = Month < 3 ? "8.99" : "85.99";

            string message = String.Format($"Здравствуйте, {currentUser.NAME}. Вы только что оформили BookВарь:абонемент  за {money}$. Наслаждайтесь прочтением множества книг доступных по подписке целый(ых) {Month} месяц(ев)!");
            MessageSender.SendEmailAsync(currentUser.EMAIL, "", message, "Оформление подписки").GetAwaiter();
            Cancel(obj);
        }
    }
}
