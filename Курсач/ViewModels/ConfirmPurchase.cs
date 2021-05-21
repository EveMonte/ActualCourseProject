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
    public class ConfirmPurchase : BaseViewModel
    {
        #region Data
        Notifier notifier;

        private string infoText; //Text in dialog window
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
        private BOOKS currentBook;
        private USERS currentUser;

        #endregion

        #region Commands
        public ICommand BuyCommand { get; private set; } // Confirm purchase
        public ICommand CancelCommand { get; private set; } // cancel dialog window
        #endregion

        //Constructor
        public ConfirmPurchase(int book_id)
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

            try
            {
                currentBook = App.db.BOOKS.FirstOrDefault(n => n.BOOK_ID == book_id); //get data
            }
            catch(Exception ex)
            {
                notifier.ShowError($"Произошла ошибка.\n{ex.Message}\nИзвините за причиненные неудобства");
            }
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;

            //Delegate command
            BuyCommand = new DelegateCommand(BuyTheBook);
            CancelCommand = new DelegateCommand(Cancel);
            ////////////////////////////////////////////

            InfoText = String.Format($"Подтвердите покупку книги \"{currentBook.TITLE}\", {currentBook.AUTHOR}. С вашей карты будет списано {ConvertDecimal.RemoveZeroes(currentBook.PRICE)}."); // set text in dialog window
        }

        private void Cancel(object obj) // cancel dialog window
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed";
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Blur = 0;
        }

        private void BuyTheBook(object obj) // buy the book
        {
            try
            {
                YOUR_BOOKS newBook = new YOUR_BOOKS();
                newBook.BOOK_ID = currentBook.BOOK_ID;
                newBook.USER_ID = currentUser.USER_ID;
                App.db.YOUR_BOOKS.Add(newBook);

                BASKETS bookToDelete = App.db.BASKETS.FirstOrDefault(n => (n.USER_ID == currentUser.USER_ID) && (n.BOOK_ID == currentBook.BOOK_ID));

                if (bookToDelete != null)
                {
                    App.db.BASKETS.Remove(bookToDelete);
                }

                BOOKS yourBook = App.db.BOOKS.FirstOrDefault(n => n.BOOK_ID == currentBook.BOOK_ID);
                App.db.SaveChangesAsync().GetAwaiter();
                string message = String.Format($"Здравствуйте, {currentUser.NAME}. Вы только что приобрели книгу \"{yourBook.TITLE}\" за {ConvertDecimal.RemoveZeroes(yourBook.PRICE)}. Наслаждайтесь прочтением!");
                MessageSender.SendEmailAsync(currentUser.EMAIL, "", message, "Покупка книги").GetAwaiter();
                Cancel(obj);
                notifier.ShowSuccess("Книга добавлена на полку");
            }
            catch(Exception ex)
            {
                notifier.ShowError($"Произошла ошабка.\n" +
                    $"{ex.Message}" +
                    $"\nИзвините за причиненные неудобства");
            }
        }

    }
}
