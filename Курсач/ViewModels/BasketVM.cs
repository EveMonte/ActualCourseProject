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
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    class BasketVM : BaseViewModel
    {
        #region Data
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
        public ObservableCollection<GENRES> Genres { get; private set; }
        public IQueryable<BOOKS> coll { get; set; }
        LIBRARYEntities db = new LIBRARYEntities();
        public USERS currentUser;
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
                OnPropertyChanged("SelectedBook");
            }
        }
        #endregion

        #region Commands
        public ICommand DeleteCommand { get; private set; }
        public ICommand DownloadCommand { get; private set; }
        public ICommand MarkCommand { get; private set; }
        public ICommand FindByGenreCommand { get; private set; }
        public ICommand BuyCommand { get; private set; }
        #endregion
        public BasketVM()
        {
            BuyCommand = new DelegateCommand(BuyTheBook);
            DeleteCommand = new DelegateCommand(DeleteBook);
            MarkCommand = new DelegateCommand(SetMark);
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            Books = new ObservableCollection<BOOKS>();
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                string command = String.Format($"SELECT * " +
                                               $"FROM BASKETS WHERE USER_ID = {currentUser.USER_ID}");
                var h = (library.Database.SqlQuery<BASKETS>(command));
                foreach (BASKETS book in h)
                {
                    BOOKS b = library.BOOKS.Where(n => n.BOOK_ID == book.BOOK_ID).FirstOrDefault();
                    command = String.Format($"SELECT top(1) * FROM MARKS WHERE BOOK_ID = {book.BOOK_ID} AND USER_ID = {currentUser.USER_ID}");
                    var marks = library.Database.SqlQuery<MARKS>(command);
                    foreach (var m in marks)
                    {
                        b.Mark = (int)m.MARK;
                    }
                    command = String.Format($"SELECT * FROM MARKS WHERE BOOK_ID = {book.BOOK_ID}");
                    var rating = library.Database.SqlQuery<MARKS>(command);
                    decimal sum = 0;
                    foreach (var m in rating)
                    {
                        sum += (decimal)m.MARK;
                    }
                    b.NUMBEROFVOICES = rating.Count();

                    if (b.NUMBEROFVOICES != 0)
                    {
                        b.RATING = sum / b.NUMBEROFVOICES;
                    }
                    command = String.Format($"SELECT * " +
                                               $"FROM GENRES WHERE GENRE_ID = {b.GENRE}");
                    var genres = library.Database.SqlQuery<GENRES>(command);

                    foreach (var m in genres)
                    {
                        b.Genre = m.GENRE;
                    }
                    Books.Add(b);
                }
                library.SaveChanges();
                Genres = new ObservableCollection<GENRES>(library.GENRES.OrderBy(n => n.GENRE));
            }
            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
            FindByGenreCommand = new DelegateCommand(FindByGenre);
        }

        private void BuyTheBook(object obj)
        {
            if(db.YOUR_BOOKS.FirstOrDefault(n => (n.BOOK_ID == (int)obj) && (n.USER_ID == currentUser.USER_ID)) == null)
            {
                if (currentUser.CREDIT_CARD != null)
                {
                    YOUR_BOOKS newBook = new YOUR_BOOKS();
                    newBook.BOOK_ID = (int)obj;
                    newBook.USER_ID = currentUser.USER_ID;
                    db.YOUR_BOOKS.Add(newBook);
                    BASKETS bookToDelete = db.BASKETS.FirstOrDefault(n => (n.USER_ID == currentUser.USER_ID) && (n.BOOK_ID == (int)obj));
                    if (bookToDelete != null)
                    {
                        db.BASKETS.Remove(bookToDelete);
                    }
                    BOOKS yourBook = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == (int)obj);
                    db.SaveChangesAsync().GetAwaiter();
                    Books.Remove(yourBook);
                    string message = String.Format($"Здравствуйте, {currentUser.NAME}. Вы только что приобрели книгу \"{yourBook.TITLE}\" за {yourBook.PRICE}$. Наслаждайтесь чтением!");
                    MessageSender.SendEmailAsync(currentUser.EMAIL, "", message, "Покупка книги").GetAwaiter();
                }
                else
                {
                    WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AddCreditCardVM();
                }
            }            
        }

        private void SetMark(object obj)
        {
            MARKS mark = new MARKS();
        }

        private void DeleteBook(object obj)
        {
            foreach (BOOKS book in books)
            {
                if (book.BOOK_ID == (int)obj)
                {
                    db.Database.ExecuteSqlCommand($"DELETE FROM BASKETS WHERE BOOK_ID = {(int)obj}");
                    db.SaveChanges();
                    Books.Remove(book);
                    break;
                }
            }
        }

        private void FindByGenre(object obj)
        {
            Books = new ObservableCollection<BOOKS>(Books.Where(n => n.GENRE == SelectedGenre.GENRE_ID));
        }

        private void OpenFullInfoUserControl(object obj)
        {
            FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel());
            FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook = SelectedBook;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.CurrentPageViewModel = new AdditionalInfoViewModel();
        }
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
