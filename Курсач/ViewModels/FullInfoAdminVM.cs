using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class FullInfoAdminVM : BaseViewModel
    {
        /*private ObservableCollection<GENRES> genres;

        public ObservableCollection<GENRES> Genres
        {
            get { return genres; }
            set 
            { 
                genres = value;
                OnPropertyChanged("Genres");
            }
        }*/
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

        private Notifier notifier;
        LIBRARYEntities db = new LIBRARYEntities();
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
        public ICommand ConfirmCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public FullInfoAdminVM(BOOKS currentBook, ObservableCollection<USERS> users)
        {
            CurrentBook = currentBook;
            Users = new ObservableCollection<USERS>(users);
            Genres = new ObservableCollection<GENRES>(db.GENRES.OrderBy(n => n.GENRE));

            ConfirmCommand = new DelegateCommand(SaveChanges);
            RemoveCommand = new DelegateCommand(RemoveBook);

            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        private void RemoveBook(object obj)
        {
            var book = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID);
            db.BOOKS.Remove(book);
            db.SaveChangesAsync().GetAwaiter();
            AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new ListOfBooksAdminVM(AdminWindowSingleTone.GetInstance().AdminVM.Books);
            notifier.ShowSuccess("Изменения успешно сохранены");
        }

        private void SaveChanges(object obj)
        {
            var book = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID);
            book.AUTHOR = CurrentBook.AUTHOR;
            book.TITLE = CurrentBook.TITLE;
            book.COVER = CurrentBook.COVER;
            book.LINK = CurrentBook.LINK;
            book.CATEGORY = CurrentBook.CATEGORY;
            book.PRICE = CurrentBook.PRICE;
            book.GENRE = db.GENRES.FirstOrDefault(n => n.GENRE == SelectedGenre.GENRE).GENRE_ID;
            db.SaveChangesAsync().GetAwaiter();
            AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new ListOfBooksAdminVM(AdminWindowSingleTone.GetInstance().AdminVM.Books);
            notifier.ShowSuccess("Книга успешно удалена");

        }
    }
}
