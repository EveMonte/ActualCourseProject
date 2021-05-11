namespace Курсач.ViewModels
{
    public class FullInfoViewModel : BaseViewModel
    {
        private BOOKS currentBook;
        public BOOKS CurrentBook
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
        public FullInfoViewModel()
        {

        }
    }
}
