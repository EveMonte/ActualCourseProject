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
    public class ListOfBooksViewModel : BaseViewModel
    {
        #region Data
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

        public ICommand FindByGenreCommand { get; private set; }
        public ListOfBooksViewModel()
        {
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                Books = new ObservableCollection<BOOKS>(library.BOOKS);
                Genres = new ObservableCollection<GENRES>(library.GENRES.OrderBy(n => n.GENRE));
            }
            foreach(BOOKS book in Books)
            {
                if(book.CATEGORY == "Подписка")
                {
                    book.Subscription = 1;
                }
                else
                {
                    book.Subscription = 0;
                }
            }
            OpenFullInfo = new DelegateCommand(OpenFullInfoUserControl);
            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
            FindByGenreCommand = new DelegateCommand(FindByGenre);
        }

        private void FindByGenre(object obj)
        {
            Books = new ObservableCollection<BOOKS>(Books.Where(n => n.GENRE == SelectedGenre.GENRE_ID));
        }

        private void OpenFullInfoUserControl(object obj)
        {
            FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel());
            string command = String.Format($"SELECT * " +
                $"FROM GENRES");
            var h = (db.Database.SqlQuery<GENRES>(command));
            foreach (GENRES genre in h)
            {
                if (genre.GENRE_ID == SelectedBook.GENRE)
                    SelectedBook.Genre = genre.GENRE;
            }
            command = String.Format($"SELECT COUNT(*) FROM MARKS WHERE BOOK_ID = {SelectedBook.BOOK_ID}");
            var a = db.Database.SqlQuery<int>(command);
            foreach (var b in a)
            {
                int s = b;
                SelectedBook.NUMBEROFVOICES = s;
            }
            SelectedBook.RATING = SelectedBook.RATING;
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
            DependencyProperty.Register("Text", typeof(string), typeof(ListOfBooksViewModel), new PropertyMetadata("", TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ListOfBooksViewModel;
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
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ListOfBooksViewModel), new PropertyMetadata(null));
        private bool Search(object obj)
        {
            bool result = true;
            BOOKS current = obj as BOOKS;

            if (current != null && !string.IsNullOrWhiteSpace(Text) && !current.TITLE.Contains(Text) && !current.AUTHOR.Contains(Text))
            {
                result = false;
            }
            return result;
        }
        #endregion
    }
}
