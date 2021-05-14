using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class AdditionalInfoViewModel : BaseViewModel
    {
        #region Data
        private Notifier notifier;
        LIBRARYEntities db = new LIBRARYEntities();
        private ObservableCollection<BOOKS> books;
        public ObservableCollection<BOOKS> Books // Similar books
        {
            get
            {
                return books;
            }
            private set
            {
                books = value;
                OnPropertyChanged("Books");
            }
        }
        BOOKS selectedBook;
        public BOOKS currentBook;
        USERS currentUser;
        private BOOKS CurrentBook // Which book is currently active in Additional info page
        {
            get
            {
                return currentBook;
            }
            set
            {
                currentBook = value;
                OnPropertyChanged("CurrentBook");
            }
        }
        public BOOKS SelectedBook // WHich book is selected in Additional Info page
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
        #endregion

        #region Commands
        public ICommand OpenFullInfo { get; private set; } // When you click on another book in similar books open its full info
        public ICommand MarkCommand { get; private set; } // Rate the book
        public ICommand AddToBasketCommand { get; private set; } // Adds book to your basket
        public ICommand BuyCommand { get; private set; } // Open User control where you can confirm or cancel purchase
        public ICommand AddToYourBooksCommand { get; private set; } // Add book to your shelf
        #endregion

        #region Commands' Logic
        //change book to another
        private void OpenFullInfoUserControl(object obj)
        {
            foreach (GENRES genre in db.GENRES.ToList()) //we are looking for our book in GENRES...
            {
                if (genre.GENRE_ID == SelectedBook.GENRE)
                    SelectedBook.Genre = genre.GENRE; //... and when we find it we write it in the notmapped property
            }

            SelectedBook.NUMBEROFVOICES = db.MARKS.Where(n => n.BOOK_ID == SelectedBook.BOOK_ID).Count(); //counting marks to write in notmapped property
            SelectedBook.RATING = SelectedBook.RATING;
            MARKS mark = db.MARKS.FirstOrDefault(n => (n.USER_ID == currentUser.USER_ID) && (n.BOOK_ID == SelectedBook.BOOK_ID));
            SelectedBook.Mark = mark != null ? (int)mark.MARK : 0;
            CurrentBook = SelectedBook;

            CreateSimilarBooks(); // create collection of similar books

            if (CurrentBook.CATEGORY == "Подписка") // Manage visibility of the buttons
            {
                CurrentBook.Subscription = 1;
                if (currentUser.SUBSCRIPTION != null)
                {
                    CurrentBook.UserWithSubscription = "Visible";
                    CurrentBook.UserWithoutSubscription = "Collapsed";
                }
                else
                {
                    CurrentBook.UserWithSubscription = "Collapsed";
                    CurrentBook.UserWithoutSubscription = "Visible";
                }
            }
            else
            {
                CurrentBook.Subscription = 0;
                CurrentBook.UserWithSubscription = "Collapsed";
                CurrentBook.UserWithoutSubscription = "Visible";

            }

            SelectedBook = null; // reset selection

            FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel()).FullInfoViewModel.CurrentBook = CurrentBook; // renew info on current page
        }

        //Add book to basket lol :)
        private void AddBookToBasket(object obj)
        {
            if(db.BASKETS.FirstOrDefault(n => (n.USER_ID == currentUser.USER_ID) && (n.BOOK_ID == (int)obj)) == null)
            {
                if (db.YOUR_BOOKS.FirstOrDefault(n => (n.USER_ID == currentUser.USER_ID) && (n.BOOK_ID == (int)obj)) == null)
                {
                    BASKETS newBasketBook = new BASKETS();
                    newBasketBook.BOOK_ID = (int)obj;
                    newBasketBook.USER_ID = currentUser.USER_ID;
                    db.BASKETS.Add(newBasketBook);
                    db.SaveChangesAsync().GetAwaiter();
                    notifier.ShowSuccess("Книга успешно добавлена в корзину");
                }
                else
                {
                    notifier.ShowWarning("Вы уже приобрели эту книгу");
                }
            }
            else
            {
                notifier.ShowWarning("Эта книга уже у вас в корзине");
            }
        }

        //Rate the book
        private void Rate(object obj)
        {
            MARKS m = db.MARKS.Where(n => (n.BOOK_ID == CurrentBook.BOOK_ID) && (n.USER_ID == currentUser.USER_ID)).FirstOrDefault();
            if (m != null) //if our current user already rated this book we change value of its mark
            {
                CurrentBook.Mark = (int)obj;
                m.MARK = CurrentBook.Mark;
            }
            else //if there is no marks for this book we create a new one
            {
                MARKS mark = new MARKS();
                mark.BOOK_ID = CurrentBook.BOOK_ID;
                mark.MARK = (int)obj;
                CurrentBook.Mark = (int)obj;
                mark.USER_ID = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser.USER_ID;
                db.MARKS.Add(mark);
                CurrentBook.NUMBEROFVOICES++;
            }

            db.SaveChanges(); // save changes to DB
            CurrentBook.RATING = ((decimal)db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Sum(n => n.MARK) / (decimal)db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Count()); // recount rating of this book
            var book = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID); // get this book from the DB
            book.RATING = CurrentBook.RATING; // change its rating
            db.SaveChangesAsync().GetAwaiter(); // and save changes async

            FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook = new BOOKS(); // to trigger OnPropertyChanged and update info in FullInfoUserControl
            FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook = CurrentBook;
        }

        //Create similar books to listbox on the bottom of the page
        private void CreateSimilarBooks()
        {
            Books = new ObservableCollection<BOOKS>(); //cleaning observable collection
            foreach (BOOKS book in db.BOOKS) //fill observable collection with new similar books
            {
                if ((CurrentBook.AUTHOR == book.AUTHOR && CurrentBook.TITLE != book.TITLE) || (CurrentBook.GENRE == book.GENRE && CurrentBook.TITLE != book.TITLE)) // if books have the same author or the same genre add it
                {
                    if (book.CATEGORY == "Подписка") // manage the visibility of red band
                    {
                        book.Subscription = 1;
                    }
                    else
                    {
                        book.Subscription = 0;
                    }
                    Books.Add(book);
                }
            }
        }

        //Add new book to your shelf
        private void AddToYourBooks(object obj)
        {
            if(db.YOUR_BOOKS.FirstOrDefault(n => (n.USER_ID == currentUser.USER_ID) && (n.BOOK_ID == (int)obj)) == null)
            {
                YOUR_BOOKS newBook = new YOUR_BOOKS();
                newBook.BOOK_ID = (int)obj;
                newBook.USER_ID = currentUser.USER_ID;
                db.YOUR_BOOKS.Add(newBook);
                db.SaveChangesAsync().GetAwaiter();
            }
        }

        private void BuyTheBook(object obj) // open user control where you can confirm or cancel purchase
        {
            if (db.YOUR_BOOKS.FirstOrDefault(n => (n.BOOK_ID == (int)obj) && (n.USER_ID == currentUser.USER_ID)) == null)
            {
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new ConfirmPurchase((int)obj);
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
            }
        }
        #endregion

        //Constructor
        public AdditionalInfoViewModel()
        {
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser; //get current user
            CurrentBook = FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook; // get current book

            if (CurrentBook.CATEGORY == "Подписка") // manage visibility of the buttons
            {
                CurrentBook.Subscription = 1;
                if (currentUser.SUBSCRIPTION != null)
                {
                    CurrentBook.UserWithSubscription = "Visible";
                    CurrentBook.UserWithoutSubscription = "Collapsed";
                }
                else
                {
                    CurrentBook.UserWithSubscription = "Collapsed";
                    CurrentBook.UserWithoutSubscription = "Visible";
                }
            }
            else
            {
                CurrentBook.Subscription = 0;
                CurrentBook.UserWithSubscription = "Collapsed";
                CurrentBook.UserWithoutSubscription = "Visible";
            }

            CreateSimilarBooks();

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

            //DelegateCommand
            OpenFullInfo = new DelegateCommand(OpenFullInfoUserControl);
            MarkCommand = new DelegateCommand(Rate);
            AddToBasketCommand = new DelegateCommand(AddBookToBasket);
            BuyCommand = new DelegateCommand(BuyTheBook);
            AddToYourBooksCommand = new DelegateCommand(AddToYourBooks);
        }
    }
}
