using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class YourBooksViewModel : BaseViewModel
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
        public ICommand FindByGenreCommand { get; private set; }
        public ICommand MarkCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        #endregion

        //Constructor
        public YourBooksViewModel()
        {
            DeleteCommand = new DelegateCommand(DeleteBook);
            DownloadCommand = new DelegateCommand(DownloadBook);
            MarkCommand = new DelegateCommand(RateTheBook);
            ClearCommand = new DelegateCommand(ClearFilter);

            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            Books = new ObservableCollection<BOOKS>();
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                string command = String.Format($"SELECT * " +
                                               $"FROM YOUR_BOOKS WHERE USER_ID = {currentUser.USER_ID}");
                var h = (library.Database.SqlQuery<YOUR_BOOKS>(command));
                foreach(YOUR_BOOKS book in h)
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

        #region Commands' Logic

        private void ClearFilter(object obj) //удалить все фильтры
        {
            Text = "";
            try
            {
                var basketBooks = App.db.YOUR_BOOKS.Where(n => n.USER_ID == currentUser.USER_ID);
                Books = new ObservableCollection<BOOKS>();
                foreach (var book in basketBooks)
                {
                    Books.Add(App.db.BOOKS.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        private void RateTheBook(object obj)
        {
            int mark = 0;
            BOOKS CurrentBook = Books.FirstOrDefault(n => n.BOOK_ID == (int)obj);
            MARKS m = db.MARKS.Where(n => (n.BOOK_ID == (int)obj) && (n.USER_ID == currentUser.USER_ID)).FirstOrDefault();
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
                newMark.USER_ID = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser.USER_ID;
                db.MARKS.Add(newMark);
                CurrentBook.NUMBEROFVOICES++;
            }
            db.SaveChanges(); // save changes to DB
            CurrentBook.RATING = ((decimal)db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Sum(n => n.MARK) / (decimal)db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Count()); // recount rating of this book
            var book = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID); // get this book from the DB
            book.RATING = CurrentBook.RATING; // change its rating
            db.SaveChangesAsync().GetAwaiter(); // and save changes async
            ObservableCollection<BOOKS> newBooks = Books;
            Books = new ObservableCollection<BOOKS>();
            Books = newBooks;
        }

        private void DownloadBook(object obj)
        {
            BOOKS book = db.BOOKS.Where<BOOKS>(n => n.BOOK_ID == (int)obj).FirstOrDefault();
            new Process
            {
                StartInfo = new ProcessStartInfo($"{book.LINK}")
                {
                UseShellExecute = true
                }
            }.Start();
        }

        private void DeleteBook(object obj)
        {
            foreach(BOOKS book in books)
            {
                if(book.BOOK_ID == (int)obj)
                {
                    Books.Remove(book);
                    db.Database.ExecuteSqlCommand($"DELETE FROM YOUR_BOOKS WHERE BOOK_ID = {(int)obj}");
                    db.SaveChanges();
                    break;
                }
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
            DependencyProperty.Register("Text", typeof(string), typeof(YourBooksViewModel), new PropertyMetadata("", TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as YourBooksViewModel;
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
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(YourBooksViewModel), new PropertyMetadata(null));
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
