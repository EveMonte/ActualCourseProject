using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Курсач.ViewModels
{
    public class FullInfoAdminVM : BaseViewModel
    {
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
            ConfirmCommand = new DelegateCommand(SaveChanges);
            RemoveCommand = new DelegateCommand(RemoveBook);
        }

        private void RemoveBook(object obj)
        {
            var book = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID);
            db.BOOKS.Remove(book);
            db.SaveChangesAsync().GetAwaiter();
        }

        private void SaveChanges(object obj)
        {
            var book = db.BOOKS.FirstOrDefault(n => n.BOOK_ID == CurrentBook.BOOK_ID);
            book.AUTHOR = CurrentBook.AUTHOR;
            book.TITLE = CurrentBook.TITLE;
            book.COVER = CurrentBook.COVER;
            book.LINK = CurrentBook.LINK;
            book.CATEGORY = CurrentBook.CATEGORY;
            db.SaveChangesAsync().GetAwaiter();
        }
    }
}
