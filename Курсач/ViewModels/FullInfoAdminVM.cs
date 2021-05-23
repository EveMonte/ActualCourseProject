using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        private ObservableCollection<GENRES> genres;
        public ObservableCollection<GENRES> Genres 
        {
            get
            {
                return genres;
            }
            set
            {
                genres = value;
                OnPropertyChanged("Genres");
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

        private Notifier notifier;
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
        private string newGenre;

        public string NewGenre
        {
            get { return newGenre; }
            set 
            { 
                newGenre = value;
                OnPropertyChanged("NewGenre");
            }
        }


        public ICommand ConfirmCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand ChangeCoverCommand { get; private set; }
        public ICommand AddNewGenreCommand { get; private set; }
        public ICommand RemoveGenreCommand { get; private set; }
        public FullInfoAdminVM(BOOKS currentBook, ObservableCollection<USERS> users)
        {
            CurrentBook = currentBook;
            Users = new ObservableCollection<USERS>(users);
            Genres = new ObservableCollection<GENRES>(App.db.GENRES.OrderBy(n => n.GENRE));
            SelectedGenre = Genres.FirstOrDefault(n => n.GENRE_ID == CurrentBook.GENRE);
            ConfirmCommand = new DelegateCommand(SaveChanges);
            RemoveCommand = new DelegateCommand(RemoveBook);
            ChangeCoverCommand = new DelegateCommand(ChangeCover);
            AddNewGenreCommand = new DelegateCommand(AddNewGenre);
            RemoveGenreCommand = new DelegateCommand(RemoveGenre);

            AdminWindow thisWin = null;
            foreach (Window win in Application.Current.Windows)
            {
                if (win is AdminWindow)
                {
                    thisWin = win as AdminWindow;
                }
            }

            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: thisWin,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        private void RemoveGenre(object obj)
        {
            try
            {
                GENRES genreToRemove = App.db.GENRES.FirstOrDefault(n => n.GENRE_ID == (int)obj);
                foreach(BOOKS book in App.db.BOOKS.Where(n => n.GENRE == genreToRemove.GENRE_ID))
                {
                    book.GENRE = null;
                }
                App.db.GENRES.Remove(genreToRemove);
                Genres.Remove(genreToRemove);
                App.db.SaveChangesAsync().GetAwaiter();
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }

        private void AddNewGenre(object obj)
        {
            try
            {
                GENRES newGenre = new GENRES();
                newGenre.GENRE = NewGenre;
                if (App.db.GENRES.FirstOrDefault(n => n.GENRE == NewGenre) == null)
                {
                    App.db.GENRES.Add(newGenre);
                    App.db.SaveChangesAsync().GetAwaiter();
                    Genres.Add(newGenre);
                    NewGenre = "";
                }
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }

        private void ChangeCover(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"C:\Users\User\Desktop\Курсааааач\Media";
            if (openFileDialog.ShowDialog() == true)
                CurrentBook.COVER = Path.GetFullPath(openFileDialog.FileName);
            AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new FullInfoAdminVM(CurrentBook, Users);
        }

        private void RemoveBook(object obj)
        {
            var book = App.db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID);
            AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new ListOfBooksAdminVM(AdminWindowSingleTone.GetInstance().AdminVM.Books);
            AdminWindowSingleTone.GetInstance().AdminVM.Books.Remove(book);
            notifier.ShowSuccess("Книга успешно удалена");
        }

        private void SaveChanges(object obj)
        {
            if (SelectedGenre != null)
            {
                CurrentBook.GENRE = App.db.GENRES.FirstOrDefault(n => n.GENRE == SelectedGenre.GENRE).GENRE_ID;
            }
            if(CurrentBook.CATEGORY != null && CurrentBook.AUTHOR != null && CurrentBook.DESCRIPTION != null && CurrentBook.GENRE != null && CurrentBook.LINK != null  && CurrentBook.TITLE != null)
            {
                AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new ListOfBooksAdminVM(AdminWindowSingleTone.GetInstance().AdminVM.Books);
                notifier.ShowSuccess("Изменения успешно сохранены");
            }
            else
            {
                notifier.ShowWarning("Пожалуйста, заполните все поля");
            }
        }
    }
}
