using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class AdditionalInfoViewModel : BaseViewModel
    {
        LIBRARYEntities db = new LIBRARYEntities();
        private ObservableCollection<BOOKS> books;
        public ObservableCollection<BOOKS> Books
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
        public ICommand OpenFullInfo { get; private set; }
        public ICommand MarkCommand { get; private set; }
        BOOKS selectedBook;
        public BOOKS currentBook;
        USERS currentUser;
        private BOOKS CurrentBook
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
        public BOOKS SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
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
            CurrentBook = SelectedBook;
            CreateSimilarBooks();
            SelectedBook = null;
            CurrentBook = CurrentBook;
        }
        public AdditionalInfoViewModel()
        {
            CurrentBook = FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook;
            CreateSimilarBooks();
            OpenFullInfo = new DelegateCommand(OpenFullInfoUserControl);
            MarkCommand = new DelegateCommand(SetMark);
        }

        private void SetMark(object obj)
        {
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            MARKS m = db.MARKS.Where(n => (n.BOOK_ID == CurrentBook.BOOK_ID) && (n.USER_ID == currentUser.USER_ID)).FirstOrDefault();
            if (m != null)
            {
                CurrentBook.Mark = (int)obj;
                m.MARK = CurrentBook.Mark;
                //MARKS mrk = db.MARKS.FirstOrDefault(n => n == m);
                //mrk.MARK = (int)obj;
            }
            else
            {
                MARKS mark = new MARKS();
                mark.BOOK_ID = CurrentBook.BOOK_ID;
                mark.MARK = (int)obj;
                CurrentBook.Mark = (int)obj;
                mark.USER_ID = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser.USER_ID;
                db.MARKS.Add(mark);
                CurrentBook.NUMBEROFVOICES++;
            }
            db.SaveChanges();
            CurrentBook.RATING = (decimal)(db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Sum(n => n.MARK) / db.MARKS.Where(n => n.BOOK_ID == CurrentBook.BOOK_ID).Count());
            var book = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID);
            book.RATING = CurrentBook.RATING;
            db.SaveChangesAsync().GetAwaiter();
            FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook = new BOOKS();
            FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook = CurrentBook;
        }

        private void CreateSimilarBooks()
        {
            Books = new ObservableCollection<BOOKS>();
            foreach (BOOKS book in Books)
            {
                Books.Remove(book);
            }
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                foreach (BOOKS book in library.BOOKS)
                {
                    if ((CurrentBook.AUTHOR == book.AUTHOR && CurrentBook.TITLE != book.TITLE) || (CurrentBook.GENRE == book.GENRE && CurrentBook.TITLE != book.TITLE))
                    {
                        Books.Add(book);
                    }
                }
                //Books = new ObservableCollection<BOOKS>(library.BOOKS);
            }
        }
    }
}
