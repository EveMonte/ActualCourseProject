using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
            try
            {
                foreach (GENRES genre in App.db.GENRES.ToList()) //we are looking for our book in GENRES...
                {
                    if (genre.GENRE_ID == SelectedBook.GENRE)
                        SelectedBook.Genre = genre.GENRE; //... and when we find it we write it in the notmapped property
                }

                SelectedBook.NUMBEROFVOICES = App.db.MARKS.Where(n => n.BOOK_ID == SelectedBook.BOOK_ID).Count(); //counting marks to write in notmapped property
                SelectedBook.RATING = SelectedBook.RATING;
                MARKS mark = App.db.MARKS.FirstOrDefault(n => (n.USER_ID == App.currentUser.USER_ID) && (n.BOOK_ID == SelectedBook.BOOK_ID));
                SelectedBook.Mark = mark != null ? (int)mark.MARK : 0;
                CurrentBook = SelectedBook;
            }
            catch (Exception ex)
            {
                App.notifier.ShowError(ex.Message);
            }

            CreateSimilarBooks(); // create collection of similar books

            if (CurrentBook.CATEGORY == "Подписка") // Manage visibility of the buttons
            {
                CurrentBook.Subscription = 1;
                if (App.currentUser.SUBSCRIPTION != null)
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
            try
            {
                if (App.db.BASKETS.FirstOrDefault(n => (n.USER_ID == App.currentUser.USER_ID) && (n.BOOK_ID == (int)obj)) == null)
                {
                    if (App.db.YOUR_BOOKS.FirstOrDefault(n => (n.USER_ID == App.currentUser.USER_ID) && (n.BOOK_ID == (int)obj)) == null)
                    {
                        BASKETS newBasketBook = new BASKETS();
                        newBasketBook.BOOK_ID = (int)obj;
                        newBasketBook.USER_ID = App.currentUser.USER_ID;
                        App.db.BASKETS.Add(newBasketBook);
                        App.db.SaveChangesAsync().GetAwaiter();
                        try
                        {
                            App.notifier.ShowSuccess("Книга успешно добавлена в корзину");
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        App.notifier.ShowWarning("Вы уже приобрели эту книгу");
                    }
                }
                else
                {
                    App.notifier.ShowWarning("Эта книга уже у вас в корзине");
                }
            }
            catch (Exception ex)
            {
                App.notifier.ShowError(ex.Message);
            }
        }

        //Rate the book
        private void Rate(object obj)
        {
            try { 
            MARKS m = App.db.MARKS.Where(n => (n.BOOK_ID == CurrentBook.BOOK_ID) && (n.USER_ID == App.currentUser.USER_ID)).FirstOrDefault();
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
                mark.USER_ID = App.currentUser.USER_ID;
                App.db.MARKS.Add(mark);
                CurrentBook.NUMBEROFVOICES++;
            }

            App.db.SaveChanges(); // save changes to DB
            CurrentBook.RATING = ((decimal)App.db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Sum(n => n.MARK) / (decimal)App.db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Count()); // recount rating of this book
            var book = App.db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID); // get this book from the DB
            book.RATING = CurrentBook.RATING; // change its rating
            App.db.SaveChangesAsync().GetAwaiter(); // and save changes async
            }
            catch(Exception ex)
            {
                App.notifier.ShowError(ex.Message);
            }

            FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook = new BOOKS(); // to trigger OnPropertyChanged and update info in FullInfoUserControl
            FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook = CurrentBook;
        }

        //Create similar books to listbox on the bottom of the page
        private void CreateSimilarBooks()
        {
            Books = new ObservableCollection<BOOKS>(); //cleaning observable collection
            foreach (BOOKS book in App.db.BOOKS) //fill observable collection with new similar books
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
            if(App.db.YOUR_BOOKS.FirstOrDefault(n => (n.USER_ID == App.currentUser.USER_ID) && (n.BOOK_ID == (int)obj)) == null)
            {
                YOUR_BOOKS newBook = new YOUR_BOOKS();
                newBook.BOOK_ID = (int)obj;
                newBook.USER_ID = App.currentUser.USER_ID;
                App.db.YOUR_BOOKS.Add(newBook);
                App.db.SaveChangesAsync().GetAwaiter();
                App.notifier.ShowSuccess("Книга добавлена на полку");
            }
            else
            {
                try
                {
                    App.notifier.ShowWarning("Книга уже у вас на полке");

                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        private void BuyTheBook(object obj) // open user control where you can confirm or cancel purchase
        {
            if (App.currentUser.CREDIT_CARD != null)
            {
                if (App.db.YOUR_BOOKS.FirstOrDefault(n => (n.BOOK_ID == (int)obj) && (n.USER_ID == App.currentUser.USER_ID)) == null)
                {
                    WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new BaseDialogWindowVM(new ConfirmPurchase((int)obj));
                    WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
                    WorkFrameSingleTone.GetInstance().WorkframeViewModel.Blur = 3;
                }
                else
                {
                    App.notifier.ShowWarning("Вы уже приобрели эту книгу");
                }
            }
            else
            {
                App.notifier.ShowWarning("Для того чтобы купить книгу, необходимо добавить карту");
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AddCreditCardVM();
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.Blur = 3;
            }
        }
        #endregion

        //Constructor
        public AdditionalInfoViewModel()
        {
            CurrentBook = FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook; // get current book

            if (CurrentBook.CATEGORY == "Подписка") // manage visibility of the buttons
            {
                CurrentBook.Subscription = 1;
                if (App.currentUser.SUBSCRIPTION != null)
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

            //DelegateCommand
            OpenFullInfo = new DelegateCommand(OpenFullInfoUserControl);
            MarkCommand = new DelegateCommand(Rate);
            AddToBasketCommand = new DelegateCommand(AddBookToBasket);
            BuyCommand = new DelegateCommand(BuyTheBook);
            AddToYourBooksCommand = new DelegateCommand(AddToYourBooks);
        }
    }
}
