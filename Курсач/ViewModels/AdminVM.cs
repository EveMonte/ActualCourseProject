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
    public class AdminVM : BaseViewModel
    {
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
        private BaseViewModel currentPageViewModel;

        public BaseViewModel CurrentPageViewModel
        {
            get { return currentPageViewModel; }
            set 
            { 
                currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        private string visibility = "Collapsed";

        public string Visibility
        {
            get { return visibility; }
            set 
            { 
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        private string buttonVisibility = "Collapsed";

        public string ButtonVisibility
        {
            get { return buttonVisibility; }
            set
            {
                buttonVisibility = value;
                OnPropertyChanged("ButtonVisibility");
            }
        }

        private int blur = 0;
        public int Blur
        {
            get { return blur; }
            set
            {
                blur = value;
                OnPropertyChanged("Blur");
            }
        }


        private ObservableCollection<ADVERTISEMENT> ads;

        public ObservableCollection<ADVERTISEMENT> Ads
        {
            get { return ads; }
            set 
            { 
                ads = value;
                OnPropertyChanged("Ads");
            }
        }

        public ICommand OpenBooksCommand { get; private set; }
        public ICommand OpenAdminsCommand { get; private set; }
        public ICommand OpenUsersCommand { get; private set; }
        public ICommand OpenUserCommand { get; private set; }
        public ICommand OpenSettingsCommand { get; private set; }
        public ICommand OpenAdvertisementCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public AdminVM()
        {
            Ads = new ObservableCollection<ADVERTISEMENT>(App.db.ADVERTISEMENT);
            Books = new ObservableCollection<BOOKS>(App.db.BOOKS);
            if (App.currentUser.ACCOUNT == "Администратор")
                ButtonVisibility = "Visible";
            OpenBooksCommand = new DelegateCommand(OpenBooks);
            OpenAdminsCommand = new DelegateCommand(OpenAdmins);
            OpenUsersCommand = new DelegateCommand(OpenUsers);
            OpenUserCommand = new DelegateCommand(OpenUser);
            OpenSettingsCommand = new DelegateCommand(OpenSettings);
            OpenAdvertisementCommand = new DelegateCommand(OpenAds);
            CurrentPageViewModel = new ListOfBooksAdminVM(Books);
            CloseCommand = new DelegateCommand(Close);

        }

        private void OpenAds(object obj)
        {
            CurrentPageViewModel = new AdvertismentsVM(Ads);
        }

        private void OpenUser(object obj)
        {
            CurrentPageViewModel = new UserPageVM();
        }

        private void OpenSettings(object obj)
        {
            CurrentPageViewModel = new SettingsVM();
        }

        private void OpenUsers(object obj)
        {
            CurrentPageViewModel = new UsersPageVM();
        }

        private void OpenAdmins(object obj)
        {
            CurrentPageViewModel = new AdminsVM();
        }
        private void Close(object obj)
        {
            AddCreditCardViewModel = null;
            Visibility = "Collapsed";
            Blur = 0;
        }
        private void OpenBooks(object obj)
        {
            CurrentPageViewModel = new ListOfBooksAdminVM(Books);
        }
    }
}
