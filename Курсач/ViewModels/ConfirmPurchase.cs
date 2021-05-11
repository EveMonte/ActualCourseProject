using System;
using System.Linq;
using System.Windows.Input;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class ConfirmPurchase : BaseViewModel
    {
        #region Data
        LIBRARYEntities db = new LIBRARYEntities();
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
        public ICommand BuyTheBookCommand { get; private set; } // Confirm purchase
        public ICommand CancelCommand { get; private set; } // cancel dialog window
        #endregion

        //Constructor
        public ConfirmPurchase(int book_id)
        {
            currentBook = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == book_id); //get data
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;

            //Delegate command
            BuyTheBookCommand = new DelegateCommand(BuyTheBook);
            CancelCommand = new DelegateCommand(Cancel);
            ////////////////////////////////////////////

            InfoText = String.Format($"Подтвердите покупку книги \"{currentBook.TITLE}\", {currentBook.AUTHOR}. С вашей карты будет списано {ConvertDecimal.RemoveZeroes(currentBook.PRICE)}."); // set text in dialog window
        }

        private void Cancel(object obj) // cancel dialog window
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed";

        }

        private void BuyTheBook(object obj) // buy the book
        {

            if (currentUser.CREDIT_CARD != null)
            {
                YOUR_BOOKS newBook = new YOUR_BOOKS();
                newBook.BOOK_ID = currentBook.BOOK_ID;
                newBook.USER_ID = currentUser.USER_ID;
                db.YOUR_BOOKS.Add(newBook);

                BASKETS bookToDelete = db.BASKETS.FirstOrDefault(n => (n.USER_ID == currentUser.USER_ID) && (n.BOOK_ID == currentBook.BOOK_ID));

                if (bookToDelete != null)
                {
                    db.BASKETS.Remove(bookToDelete);
                }

                BOOKS yourBook = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == currentBook.BOOK_ID);
                db.SaveChangesAsync().GetAwaiter();
                string message = String.Format($"Здравствуйте, {currentUser.NAME}. Вы только что приобрели книгу \"{yourBook.TITLE}\" за {ConvertDecimal.RemoveZeroes(yourBook.PRICE)}. Наслаждайтесь прочтением!");
                MessageSender.SendEmailAsync(currentUser.EMAIL, "", message, "Покупка книги").GetAwaiter();
            }
            else
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AddCreditCardVM();
            }


            Cancel(obj); // cancel
        }
    }
}
