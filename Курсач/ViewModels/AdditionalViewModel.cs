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
            FullInfoViewModel.book = SelectedBook;
            FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel());
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.CurrentPageViewModel = new AdditionalInfoViewModel();
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
                    if (FullInfoViewModelSingleTone.GetInstance().FullInfoViewModel.CurrentBook.AUTHOR == book.AUTHOR)
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
