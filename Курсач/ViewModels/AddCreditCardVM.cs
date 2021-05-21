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
using System.Text.RegularExpressions;

namespace Курсач.ViewModels
{
    public class AddCreditCardVM : BaseViewModel
    {
        #region Data
        Notifier notifier;

        private string creditCard = "";
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
        private string validity = "";
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
        private string cvv = "";
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

        private string cardNumberValidation;

        public string CardNumberValidation
        {
            get { return cardNumberValidation; }
            set 
            { 
                cardNumberValidation = value;
                OnPropertyChanged("CardNumberValidation");
            }
        }

        private string validityValidation;

        public string ValidityValidation
        {
            get { return validityValidation; }
            set 
            { 
                validityValidation = value;
                OnPropertyChanged("ValidityValidation");
            }
        }

        private string cvvValidation;

        public string CVVValidation
        {
            get { return cvvValidation; }
            set 
            { 
                cvvValidation = value;
                OnPropertyChanged("CVVValidation");
            }
        }


        USERS currentUser;
        #endregion

        #region Commands
        public ICommand CloseUserPageCommand { get; private set; } //Close User Control when user click arrow back or 
        public ICommand AddCardCommand { get; private set; } // Add new card to user or change
        #endregion

        //Constructor
        public AddCreditCardVM()
        {
            if(WorkFrameSingleTone.GetInstance().WorkframeViewModel != null)
            {
                currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Blur = 3;
            }
            else
                currentUser = AdminWindowSingleTone.GetInstance().AdminVM.currentUser;

            //Delegate Command
            CloseUserPageCommand = new DelegateCommand(Close);
            AddCardCommand = new DelegateCommand(AddCard);
            ///////////////////////////////////////////////

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

             //activate dark area
        }

        #region Commands' Logic
        private void AddCard(object obj) // Change credit card of current user in DB
        {
            try
            {
                bool flag = true;
                Regex creditCardNumber = new Regex(@"\d{16}");
                Regex creditCardCVV = new Regex(@"\d{3}");
                Regex creditCardValidity = new Regex(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$");
                var user = App.db.USERS.FirstOrDefault(n => n.USER_ID == currentUser.USER_ID);
                if (!creditCardNumber.IsMatch(CREDIT_CARD))
                {
                    CardNumberValidation = "Номер карты не корректен!";
                    flag = false;
                }
                else
                {
                    CardNumberValidation = "";
                }
                if (!creditCardCVV.IsMatch(CVV))
                {
                    CVVValidation = "Введенный CVV не корректен!";
                    flag = false;
                }
                else
                {
                    CVVValidation = "";
                }
                if (!creditCardValidity.IsMatch(Validity))
                {
                    ValidityValidation = "Срок действия карты не корректен!";
                    flag = false;
                }
                else
                {
                    ValidityValidation = "";
                }
                if (!flag)
                {
                    return;
                }
                user.CREDIT_CARD = CREDIT_CARD;
                App.db.SaveChangesAsync().GetAwaiter();
                if (WorkFrameSingleTone.GetInstance().WorkframeViewModel != null)
                {
                    WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser.CREDIT_CARD = CREDIT_CARD;
                }
                else
                {
                    AdminWindowSingleTone.GetInstance().AdminVM.currentUser.CREDIT_CARD = CREDIT_CARD;
                }
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
            Close(obj); // Close User control
            notifier.ShowSuccess("Карта успешно добавлена");
        }

        private void Close(object obj) // Close user control
        {
            if (WorkFrameSingleTone.GetInstance().WorkframeViewModel != null)
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null; //viewmodel in content control = null
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed"; // deactivate dark area
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Blur = 0; // deactivate dark area
            }
        }
        #endregion
    }
}
