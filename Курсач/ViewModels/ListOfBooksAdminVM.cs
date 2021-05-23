using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ListOfBooksAdminVM : BaseViewModel
    {
        #region Data

        Notifier notifier;
        private ObservableCollection<BOOKS> books;
        public ObservableCollection<BOOKS> Books
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
        private ObservableCollection<USERS> users;
        public ObservableCollection<USERS> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }
        public ObservableCollection<GENRES> Genres { get; private set; }
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
        BOOKS selectedBook;
        public BOOKS SelectedBook
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
        public ICommand OpenFullInfo { get; private set; }
        public ICommand FindByGenreCommand { get; private set; }
        public ICommand AddBookCommand { get; private set; }
        public ICommand RemoveBookCommand { get; private set; }
        public ICommand GetBooksCommand { get; private set; }
        public ICommand SetBooksCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }


        #endregion

        #region Commands' Logic
        private void FindByGenre(object obj)
        {
            string newText = new string(Text.ToCharArray());
            Books = new ObservableCollection<BOOKS>(Books.Where(n => n.GENRE == SelectedGenre.GENRE_ID));
            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
        }

        private void OpenFullInfoUserControl(object obj) // Open page with extended info
        {
            foreach (GENRES genre in App.db.GENRES.ToList()) //we are looking for our book in GENRES...
            {
                if (genre.GENRE_ID == SelectedBook.GENRE)
                    SelectedBook.Genre = genre.GENRE; //... and when we find it we write it in the notmapped property
            }
            SelectedBook.NUMBEROFVOICES = App.db.MARKS.Where(n => n.BOOK_ID == SelectedBook.BOOK_ID).Count(); //counting marks to write in notmapped property
            MARKS mark = App.db.MARKS.FirstOrDefault(n => (n.USER_ID == App.currentUser.USER_ID) && (n.BOOK_ID == SelectedBook.BOOK_ID));
            SelectedBook.Mark = mark != null ? (int)mark.MARK : 0;
            var baskets = App.db.BASKETS.Where(n => n.BOOK_ID == SelectedBook.BOOK_ID);
            Users = new ObservableCollection<USERS>();
            foreach (var basket in baskets)
            {
                foreach (var user in App.db.USERS.Where(n => n.USER_ID == basket.USER_ID))
                    Users.Add(user);
            }
            AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new FullInfoAdminVM(SelectedBook, Users);

        }
        #endregion

        //Constructor
        public ListOfBooksAdminVM(ObservableCollection<BOOKS> Books)
        {
            this.Books = Books;
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                Genres = new ObservableCollection<GENRES>(library.GENRES.OrderBy(n => n.GENRE));
            }
            foreach (BOOKS book in Books) //check books. If book is available by subscription, we place band
            {
                if (book.CATEGORY == "Подписка")
                {
                    book.Subscription = 1;
                }
                else
                {
                    book.Subscription = 0;
                }
                book.FormattedPrice = ConvertDecimal.RemoveZeroes(book.PRICE);
            }
            OpenFullInfo = new DelegateCommand(OpenFullInfoUserControl);
            AddBookCommand = new DelegateCommand(AddBook);
            RemoveBookCommand = new DelegateCommand(RemoveBook, CanRemoveBook);
            SetBooksCommand = new DelegateCommand(SaveBooks);
            GetBooksCommand = new DelegateCommand(GetBooks);
            ClearCommand = new DelegateCommand(ClearFilter);

            AdminWindow thisWin = null;
            foreach (Window win in Application.Current.Windows)
            {
                if (win is AdminWindow)
                {
                    thisWin = win as AdminWindow;
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

            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
            FindByGenreCommand = new DelegateCommand(FindByGenre);
        }

        private void ClearFilter(object obj) //удалить все фильтры
        {
            Text = "";
            try
            {
                Books = new ObservableCollection<BOOKS>(App.db.BOOKS);
                var shelfBooks = App.db.YOUR_BOOKS.Where(n => n.USER_ID == App.currentUser.USER_ID);
                foreach (var book in shelfBooks)
                {
                    var bookToRemove = Books.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID);
                    if (bookToRemove != null)
                    {
                        Books.Remove(bookToRemove);
                    }
                }
                var basketBooks = App.db.BASKETS.Where(n => n.USER_ID == App.currentUser.USER_ID);
                foreach (var book in Books)
                {
                    if (basketBooks.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID) != null)
                    {
                        book.IsInBasket = 1;
                    }
                    else
                    {
                        book.IsInBasket = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }

        private void GetBooks(object obj)
        {
            Books = new ObservableCollection<BOOKS>(App.db.BOOKS);
            AdminWindowSingleTone.GetInstance().AdminVM.Books = Books;
            AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new ListOfBooksAdminVM(Books);
        }

        private void SaveBooks(object obj)//Save books to database
        {
            try
            {
                List<BOOKS> listToDelete = new List<BOOKS>();
                foreach (BOOKS book in Books)
                {
                    var newBook = App.db.BOOKS.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID);
                    if (newBook == null)
                        App.db.BOOKS.Add(book);
                    else
                    {
                        newBook.AUTHOR = book.AUTHOR;
                        newBook.TITLE = book.TITLE;
                        newBook.COVER = book.COVER;
                        newBook.LINK = book.LINK;
                        newBook.CATEGORY = book.CATEGORY;
                        newBook.PRICE = book.PRICE;
                        newBook.GENRE = book.GENRE;
                        newBook.DESCRIPTION = book.DESCRIPTION;
                    }
                }
                foreach (BOOKS book in App.db.BOOKS)
                {
                    if (Books.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID) == null)
                    {
                        listToDelete.Add(book);
                    }
                }
                if(listToDelete != null)
                    App.db.BOOKS.RemoveRange(listToDelete);
                listToDelete = null;
                App.db.SaveChangesAsync().GetAwaiter();
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }
        bool CanRemoveBook(object arg)
        {
            return (arg as BOOKS) != null;
        }

        void RemoveBook(object obj)//Remove selected row from datagrid
        {
            Books.Remove((BOOKS)obj);
        }
        private void AddBook(object obj) //Add new row to datagrid
        {
            Books.Add(new BOOKS {AUTHOR = "Автор", TITLE = "Название книги", DESCRIPTION = "Описание" });
        }
        #region Filter
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ListOfBooksAdminVM), new PropertyMetadata("", TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ListOfBooksAdminVM;
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
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ListOfBooksAdminVM), new PropertyMetadata(null));
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
