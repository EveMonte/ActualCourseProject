using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    class YourBookViewModel : BaseViewModel
    {
        private int mark;
        public int Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
                OnPropertyChanged("Mark");
            }                
        }
        private BOOKS currentBook;
        LIBRARYEntities db = new LIBRARYEntities();
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
        public YourBookViewModel()
        {
            /*currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            string command; 
            command = String.Format($"SELECT COUNT(*) FROM MARKS WHERE BOOK_ID = {book.BOOK_ID}");
            var a = db.Database.SqlQuery<int>(command);
            foreach (var b in a)
            {
                int s = b;
                CurrentBook.NUMBEROFVOICES = s;
            }
            CurrentBook.RATING = book.RATING;
            command = String.Format($"SELECT top(1) * FROM MARKS WHERE BOOK_ID = {book.BOOK_ID} AND USER_ID = {currentUser.USER_ID}");
            var marks = db.Database.SqlQuery<MARKS>(command);
            foreach (var b in marks)
            {
                Mark = (int)b.MARK;
            }*/
        }


    }
}
