using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System;

namespace Курсач.ViewModels
{
    public class WorkframeViewModel : BaseViewModel
    {
        #region Data
        public USERS currentUser;
        private BaseViewModel _currentPage;
        public BaseViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPageViewModel));
            }
        }
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        BOOKS selectedBook;
        //object selectedBook;
        public ObservableCollection<BOOKS> Books { get; private set; }
        public BOOKS SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
        private string visibility = "Collapsed";
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }
        private BaseViewModel addCreditCardViewModel;
        public BaseViewModel AddCreditCardViewModel
        {
            get
            {
                return addCreditCardViewModel;
            }
            set
            {
                addCreditCardViewModel = value;
                OnPropertyChanged("AddCreditCardViewModel");
            }
        }
        #endregion

        #region Commands
        public ICommand OpenListOfBooksCommand { get; private set; }
        public ICommand OpenYourBooksCommand { get; private set; }
        public ICommand OpenBasketCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }
        public ICommand OpenUserCommand { get; private set; }
        public ICommand SubscriptionCommand { get; private set; }
        #endregion

        public WorkframeViewModel(USERS user)
        {
            currentUser = user;
            CurrentPageViewModel = new ListOfBooksViewModel(currentUser);
            OpenUserCommand = new DelegateCommand(OpenUser);
            OpenListOfBooksCommand = new DelegateCommand(OpenListOfBooks);
            OpenYourBooksCommand = new DelegateCommand(OpenYourBooks);
            OpenBasketCommand = new DelegateCommand(OpenBasket);
            SubscriptionCommand = new DelegateCommand(OpenSubscription);
            SettingsCommand = new DelegateCommand(OpenSettings);
        }


        #region Commands' Logic
        private void OpenSubscription(object obj)
        {
            CurrentPageViewModel = new SubscriptionVM();
        }
        private void OpenSettings(object obj)
        {
            CurrentPageViewModel = new SettingsVM();
        }

        private void OpenUser(object obj)
        {
            CurrentPageViewModel = new UserPageVM(currentUser);
        }

        private void OpenBasket(object obj)
        {
            CurrentPageViewModel = new BasketVM();
        }

        private void OpenYourBooks(object obj)
        {
            CurrentPageViewModel = new YourBooksViewModel();
        }

        private void OpenListOfBooks(object obj)
        {
            CurrentPageViewModel = new ListOfBooksViewModel(currentUser);
        }

        private void SaveBooks(object obj)//Save books from datagrid to database
        {
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var users = db.USERS;
                db.BOOKS.RemoveRange(db.BOOKS);
                foreach (BOOKS book in Books)
                {
                    db.BOOKS.Add(book);
                }
                db.SaveChanges();
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
        void ChangeCover(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"C:\Users\User\Desktop\Курсааааач\Media";
            if (openFileDialog.ShowDialog() == true)
                selectedBook.COVER = Path.GetFullPath(openFileDialog.FileName);
        }
        private void AddBook(object obj) //Add new row to datagrid
        {
            Books.Add(new BOOKS { AUTHOR = "Author", COVER = "", DESCRIPTION = "Description", GENRE = 1, TITLE = "Title", PAGES = 0, RATING = 0, CATEGORY = "Подписка", PRICE = 0, YEAR = 2021 });
        }
        #endregion
    }
}
