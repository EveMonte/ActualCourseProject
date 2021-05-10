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
    public class ConfirmPurchase : BaseViewModel
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
        public ICommand BuyTheBookCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        private BOOKS currentBook;
        private USERS currentUser;
        public ConfirmPurchase(int book_id)
        {
            currentBook = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == book_id);
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            BuyTheBookCommand = new DelegateCommand(BuyTheBook);
            CancelCommand = new DelegateCommand(Cancel);
            InfoText = String.Format($"Подтвердите покупку книги \"{currentBook.TITLE}\", {currentBook.AUTHOR}. С вашей карты будет списано {ConvertDecimal.RemoveZeroes(currentBook.PRICE)}.");
        }

        private void Cancel(object obj)
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed";

        }

        private void BuyTheBook(object obj)
        {
            if (db.YOUR_BOOKS.FirstOrDefault(n => (n.BOOK_ID == currentBook.BOOK_ID) && (n.USER_ID == currentUser.USER_ID)) == null)
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
            }
            Cancel(obj);
        }
    }
}
