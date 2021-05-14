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
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class ListOfBooksAdminVM : BaseViewModel
    {
        #region Data
        private USERS User = new USERS();
        LIBRARYEntities db = new LIBRARYEntities();
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
        public ICommand SaveBookCommand { get; private set; }

        #endregion

        #region Commands' Logic
        private void FindByGenre(object obj)
        {
            Books = new ObservableCollection<BOOKS>(Books.Where(n => n.GENRE == SelectedGenre.GENRE_ID));
        }

        private void OpenFullInfoUserControl(object obj) // Open page with extended info
        {
            foreach (GENRES genre in db.GENRES.ToList()) //we are looking for our book in GENRES...
            {
                if (genre.GENRE_ID == SelectedBook.GENRE)
                    SelectedBook.Genre = genre.GENRE; //... and when we find it we write it in the notmapped property
            }
            SelectedBook.NUMBEROFVOICES = db.MARKS.Where(n => n.BOOK_ID == SelectedBook.BOOK_ID).Count(); //counting marks to write in notmapped property
            MARKS mark = db.MARKS.FirstOrDefault(n => (n.USER_ID == User.USER_ID) && (n.BOOK_ID == SelectedBook.BOOK_ID));
            SelectedBook.Mark = mark != null ? (int)mark.MARK : 0;
            var baskets = db.BASKETS.Where(n => n.BOOK_ID == SelectedBook.BOOK_ID);
            Users = new ObservableCollection<USERS>();
            foreach (var basket in baskets)
            {
                foreach (var user in db.USERS.Where(n => n.USER_ID == basket.USER_ID))
                    Users.Add(user);
            }
            AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new FullInfoAdminVM(SelectedBook, Users);

        }
        #endregion

        //Constructor
        public ListOfBooksAdminVM()
        {
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                Books = new ObservableCollection<BOOKS>(library.BOOKS);
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
                book.FormattedPrice = Convert.ToString(decimal.Round(decimal.Parse(Convert.ToString(book.PRICE).Replace(".", ",")), 2)).Replace(",", ".") + '$';
            }
            OpenFullInfo = new DelegateCommand(OpenFullInfoUserControl);
            AddBookCommand = new DelegateCommand(AddBook);
            RemoveBookCommand = new DelegateCommand(RemoveBook, CanRemoveBook);
            SaveBookCommand = new DelegateCommand(SaveBooks);
            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
            FindByGenreCommand = new DelegateCommand(FindByGenre);
        }
        private void SaveBooks(object obj)//Save books from datagrid to database
        {
            foreach (BOOKS book in Books)
            {
                var newBook = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID);
                if (newBook == null)
                    db.BOOKS.Add(book);
                else
                    foreach(var dbBook in db.BOOKS)
                        if(Books.FirstOrDefault(n => n.BOOK_ID == dbBook.BOOK_ID) == null)
                            db.BOOKS.Remove(dbBook);
            }
            db.SaveChangesAsync().GetAwaiter();

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
            Books.Add(new BOOKS { AUTHOR = "Author", COVER = "", DESCRIPTION = "Description", GENRE = 1, TITLE = "Title", PAGES = 0, RATING = 0, CATEGORY = "Покупка", PRICE = 0, YEAR = 2021, LINK= "",  });
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
