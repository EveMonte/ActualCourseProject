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
using Курсач.Models;
using System.IO;
using Microsoft.Win32;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class WorkframeViewModel : BaseViewModel
    {
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
        #region Commands
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand OpenFullInfo { get; private set; }
        public ICommand SaveChangesCommand { get; private set; }
        public ICommand ChangeCoverCommand { get; private set; }
        public ICommand OpenListOfBooksCommand { get; private set; }
        public ICommand OpenYourBooksCommand { get; private set; }
        #endregion

        #region Command's Logic
        public WorkframeViewModel(USERS user)
        {
            currentUser = user;
            CurrentPageViewModel = new ListOfBooksViewModel();
            AddCommand = new DelegateCommand(AddBook);
            RemoveCommand = new DelegateCommand(RemoveBook, CanRemoveBook);
            SaveChangesCommand = new DelegateCommand(SaveBooks);
            ChangeCoverCommand = new DelegateCommand(ChangeCover);
            OpenListOfBooksCommand = new DelegateCommand(OpenListOfBooks);
            OpenYourBooksCommand = new DelegateCommand(OpenYourBooks);
        }

        private void OpenYourBooks(object obj)
        {
            CurrentPageViewModel = new YourBooksViewModel();
        }

        private void OpenListOfBooks(object obj)
        {
            CurrentPageViewModel = new ListOfBooksViewModel();
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

        /*public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/

        

    }
}
