using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    class BasketVM : BaseViewModel
    {
        #region Data
        Notifier notifier;

        private ObservableCollection<BOOKS> books;

        public ObservableCollection<BOOKS> Books // Books in your basket
        {
            get
            {
                return books;
            }
            set
            {
                books = value;
                OnPropertyChanged("Books");
            }
        }
        public ObservableCollection<GENRES> Genres { get; private set; } // List of genres for combobox
        private int mark;
        public int Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
                OnPropertyChanged("Mark");
            }
        }
        GENRES selectedGenre;
        public GENRES SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                selectedGenre = value;
                OnPropertyChanged("SelectedGenre");
            }
        }
        public ICommand OpenFullInfo { get; private set; }
        BOOKS selectedBook;
        public BOOKS SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelecteApp.dbook");
            }
        }
        #endregion

        #region Commands
        public ICommand DeleteCommand { get; private set; } // Delete book from basket
        public ICommand MarkCommand { get; private set; } // Rate the book
        public ICommand FindByGenreCommand { get; private set; } // find books of concrete genre
        public ICommand BuyCommand { get; private set; } // open user control where you can confirm or cancel purchase
        public ICommand ClearCommand { get; private set; }


        #endregion

        //Constructor
        public BasketVM()
        {
            //Delegate command
            BuyCommand = new DelegateCommand(BuyTheBook);
            DeleteCommand = new DelegateCommand(DeleteBook);
            MarkCommand = new DelegateCommand(SetMark);
            ClearCommand = new DelegateCommand(ClearFilter);

            ////////////////////////////////////////////

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

            Books = new ObservableCollection<BOOKS>();

            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                var basketBooks = App.db.BASKETS.Where(n => n.USER_ID == App.currentUser.USER_ID);
                foreach (BASKETS book in basketBooks) // get information about books in the basket
                {
                    BOOKS b = library.BOOKS.Where(n => n.BOOK_ID == book.BOOK_ID).FirstOrDefault(); // for each book in the basket get its info from BOOKS
                    var marks = App.db.MARKS.FirstOrDefault(n => (n.BOOK_ID == book.BOOK_ID) && (n.USER_ID == App.currentUser.USER_ID)); // find its user's mark
                    if(marks != null)
                        b.Mark = (int)marks.MARK; // set it
                    var rating = App.db.MARKS.Where(n => n.BOOK_ID == book.BOOK_ID); // get all marks of this book
                    decimal sum = 0;
                    foreach (var m in rating)
                    {
                        sum += (decimal)m.MARK;
                    }
                    b.NUMBEROFVOICES = rating.Count(); // count it

                    if (b.NUMBEROFVOICES != 0)
                    {
                        b.RATING = sum / b.NUMBEROFVOICES; // get rating
                    }
                    var genres = App.db.GENRES.Where(n => n.GENRE_ID == b.GENRE); // get string value of genre
                    foreach (var m in genres)
                    {
                        b.Genre = m.GENRE;
                    }
                    Books.Add(b);
                }
                library.SaveChanges();
                Genres = new ObservableCollection<GENRES>(library.GENRES.OrderBy(n => n.GENRE)); 
            }

            //Filter
            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
            FindByGenreCommand = new DelegateCommand(FindByGenre);
        }

        #region Commands' Logic

        private void ClearFilter(object obj) //удалить все фильтры
        {
            Text = "";
            try
            {
                var basketBooks = App.db.BASKETS.Where(n => n.USER_ID == App.currentUser.USER_ID);
                Books = new ObservableCollection<BOOKS>();
                foreach(var book in basketBooks)
                {
                    Books.Add(App.db.BOOKS.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID));
                }
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }
        private void BuyTheBook(object obj) // buy book, if user don't have credit card let him add it
        {
            if (App.db.USERS.FirstOrDefault(n => n.USER_ID == App.currentUser.USER_ID).CREDIT_CARD != null)
            {
                if (App.db.YOUR_BOOKS.FirstOrDefault(n => (n.BOOK_ID == (int)obj) && (n.USER_ID == App.currentUser.USER_ID)) == null)
                {
                    WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new BaseDialogWindowVM(new ConfirmPurchase((int)obj));
                }
                else
                {
                    notifier.ShowWarning("Вы уже приобрели эту книгу");
                }
            }
            else
            {
                notifier.ShowWarning("Для того чтобы купить книгу, необходимо добавить карту");
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new BaseDialogWindowVM(new AddCreditCardVM());
            }
        }

        private void SetMark(object obj) // rate the book
        {
            int mark = 0;
            BOOKS CurrentBook = Books.FirstOrDefault(n => n.BOOK_ID == (int)obj);
            MARKS m = App.db.MARKS.Where(n => (n.BOOK_ID == (int)obj) && (n.USER_ID == App.currentUser.USER_ID)).FirstOrDefault();
            if (m != null) //if our current user already rated this book we change value of its mark
            {
                foreach (BOOKS b in Books)
                {
                    if (b.BOOK_ID == (int)obj)
                    {
                        mark = b.Mark;
                        break;
                    }
                }
                m.MARK = mark;
            }
            else //if there is no marks for this book we create a new one
            {
                MARKS newMark = new MARKS();
                newMark.BOOK_ID = (int)obj;
                newMark.MARK = CurrentBook.Mark;
                CurrentBook.Mark = (int)obj;
                newMark.USER_ID = App.currentUser.USER_ID;
                App.db.MARKS.Add(newMark);
                CurrentBook.NUMBEROFVOICES++;
            }
            App.db.SaveChanges(); // save changes
            CurrentBook.RATING = ((decimal)App.db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Sum(n => n.MARK) / (decimal)App.db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Count()); // recount rating of this book
            var book = App.db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID); // get this book from the App.db
            book.RATING = CurrentBook.RATING; // change its rating
            App.db.SaveChangesAsync().GetAwaiter(); // and save changes async
            ObservableCollection<BOOKS> newBooks = Books;
            Books = new ObservableCollection<BOOKS>();
            Books = newBooks;
        }

        private void DeleteBook(object obj) // delete book from basket
        {
            try
            {
                BASKETS basketToDelete = new BASKETS();
                BOOKS bookToDelete = new BOOKS();
                foreach (BOOKS book in books)
                {
                    if (book.BOOK_ID == (int)obj)
                    {
                        basketToDelete = App.db.BASKETS.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID);
                        bookToDelete = book;
                        break;

                    }
                }
                App.db.BASKETS.Remove(basketToDelete);
                App.db.SaveChangesAsync().GetAwaiter();
                Books.Remove(bookToDelete);
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }

        private void FindByGenre(object obj)
        {
            Books = new ObservableCollection<BOOKS>(Books.Where(n => n.GENRE == SelectedGenre.GENRE_ID));
            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
        }
        #endregion

        #region Filter
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BasketVM), new PropertyMetadata("", TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BasketVM;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.Search;
            }
        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(BasketVM), new PropertyMetadata(null));
        private bool Search(object obj)
        {
            bool result = true;
            BOOKS current = obj as BOOKS;

            if (current != null && !string.IsNullOrWhiteSpace(Text) && !current.TITLE.ToLower().Contains(Text.ToLower()) && !current.AUTHOR.ToLower().Contains(Text.ToLower()))
            {
                result = false;
            }
            return result;
        }
        #endregion
    }
}
