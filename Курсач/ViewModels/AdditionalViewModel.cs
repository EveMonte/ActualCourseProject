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
        public ObservableCollection<BOOKS> Books { get; private set; }
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
            SelectedBook = null;
            //FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel());
            //WorkFrameSingleTone.GetInstance().WorkframeViewModel.CurrentPageViewModel = new AdditionalInfoViewModel();
        }
        public AdditionalInfoViewModel()
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
                    if (FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel()).FullInfoViewModel.CurrentBook.AUTHOR == book.AUTHOR)
                    {
                        Books.Add(book);
                    }
                }
                //Books = new ObservableCollection<BOOKS>(library.BOOKS);
            }
            OpenFullInfo = new DelegateCommand(OpenFullInfoUserControl);
        }
    }
}
