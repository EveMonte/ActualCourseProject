using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class ConfirmSubscriptionVM : BaseViewModel
    {
        #region Data
        private string infoText; // text in dialog window
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
        private USERS currentUser;
        private Notifier notifier;
        private int Month; // length of subscription

        #endregion

        #region Commands
        public ICommand BuyCommand { get; private set; } // confirm subscription
        public ICommand CancelCommand { get; private set; } // cancel dialog window
        #endregion

        //Constructor
        public ConfirmSubscriptionVM(int type)
        {
            Workframe thisWin = null;
            foreach (Window win in Application.Current.Windows)
            {
                if (win is Workframe)
                {
                    thisWin = win as Workframe;
                }
            }

            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: thisWin,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });

            Month = type; // set length
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser; // get current user

            //Delegate command
            BuyCommand = new DelegateCommand(BuyTheSubscription);
            CancelCommand = new DelegateCommand(Cancel);

            string firstText = type == 1 ? "1 месяц" : "12 месяцев";
            string secondText = type == 1 ? "8.99" : "85.99";

            InfoText = String.Format($"Подтвердите покупку абонемента на {firstText}. С вашей карты будет списано {secondText}$."); // set text
        }

        private void Cancel(object obj) // cancel window
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed";
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Blur = 0;
        }

        private void BuyTheSubscription(object obj) // buy the subscription
        {
            try
            {
                var a = App.db.SUBSCRIPTIONS.FirstOrDefault(n => n.SUBSCRIPTION_ID == currentUser.SUBSCRIPTION);
                if (App.db.SUBSCRIPTIONS.Where(n => n.SUBSCRIPTION_ID == currentUser.SUBSCRIPTION).FirstOrDefault() == null)
                {
                    SUBSCRIPTIONS sub = new SUBSCRIPTIONS();
                    sub.SUBSCRIPTION_DATE = DateTime.Now;
                    sub.LENGTH = ++Month;
                    App.db.SUBSCRIPTIONS.Add(sub);
                    USERS user = App.db.USERS.Where(n => n.USER_ID == currentUser.USER_ID).FirstOrDefault();
                    user.SUBSCRIPTION = sub.SUBSCRIPTION_ID;
                    App.db.SaveChanges();
                    sub = null;
                }
                else if ((DateTime.Now.Month - a.SUBSCRIPTION_DATE.Month) + 12 * (DateTime.Now.Year - a.SUBSCRIPTION_DATE.Year) < a.LENGTH)
                {
                    a.LENGTH += 1;
                    App.db.SaveChanges();
                }
                else
                {
                    a.LENGTH = Month;
                    a.SUBSCRIPTION_DATE = DateTime.Now;
                    App.db.SaveChanges();
                }

                WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser.SUBSCRIPTION = App.db.USERS.FirstOrDefault(n => n.USER_ID == currentUser.USER_ID).SUBSCRIPTION;

                string money = Month < 3 ? "8.99" : "85.99";

                string message = String.Format($"Здравствуйте, {currentUser.NAME}. " +
                    $"Вы только что оформили BookВарь:абонемент  за {money}$. " +
                    $"Наслаждайтесь прочтением множества книг доступных по подписке целый(ых) {Month} месяц(ев)!");
                MessageSender.SendEmailAsync(currentUser.EMAIL, "", message, "Оформление подписки").GetAwaiter();
                notifier.ShowSuccess("Подписка успешно оформлена");
            }
            catch(Exception ex)
            {
                notifier.ShowError($"Произошла ошибка\n" +
                    $"{ex.Message}" +
                    $"\nИзвините за причиненные неудобства");
            }
            Cancel(obj);
        }
    }
}
