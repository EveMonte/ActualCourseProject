using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using Курсач.Singleton;
using ToastNotifications.Messages;

namespace Курсач.ViewModels
{
    public class AddCreditCardVM : BaseViewModel
    {
        #region Data
        Notifier notifier;

        [Required]
        private string creditCard;
        public string CREDIT_CARD
        {
            get
            {
                return creditCard;
            }
            set
            {
                creditCard = value;
                OnPropertyChanged("CREDIT_CARD");
            }
        }
        [Required]
        private string validity;
        public string Validity
        {
            get
            {
                return validity;
            }
            set
            {
                validity = value;
                OnPropertyChanged("Validity");
            }
        }
        [Required]
        private string cvv;
        public string CVV
        {
            get
            {
                return cvv;
            }
            set
            {
                cvv = value;
                OnPropertyChanged("CVV");
            }
        }
        LIBRARYEntities db = new LIBRARYEntities();
        USERS currentUser;
        #endregion

        #region Commands
        public ICommand CloseUserPageCommand { get; private set; } //Close User Control when user click arrow back or 
        public ICommand AddCardCommand { get; private set; } // Add new card to user or change
        #endregion

        //Constructor
        public AddCreditCardVM()
        {
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser; //Get current user

            //Delegate Command
            CloseUserPageCommand = new DelegateCommand(Close);
            AddCardCommand = new DelegateCommand(AddCard);
            ///////////////////////////////////////////////
            
            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });

            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible"; //activate dark area
        }

        #region Commands' Logic
        private void AddCard(object obj) // Change credit card of current user in DB
        {
            var user = db.USERS.FirstOrDefault(n => n.USER_ID == currentUser.USER_ID);
            user.CREDIT_CARD = CREDIT_CARD;
            db.SaveChangesAsync().GetAwaiter();
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser.CREDIT_CARD = CREDIT_CARD;

            Close(obj); // Close User control
            notifier.ShowSuccess("Карта успешно добавлена");
        }

        private void Close(object obj) // Close user control
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null; //viewmodel in content control = null
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed"; // deactivate dark area
        }
        #endregion
    }
}
