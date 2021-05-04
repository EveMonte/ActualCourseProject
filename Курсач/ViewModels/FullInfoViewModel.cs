using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Models;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class FullInfoViewModel : BaseViewModel
    {
        public static BOOKS book;
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
        public ICommand MarkCommand { get; private set; }
        public FullInfoViewModel()
        {
            MarkCommand = new DelegateCommand(SetMark);
            //CurrentBook = book;
         
        }

        void SetMark(object obj)
        {
            int mark = (int)obj;
            string command = String.Format($"INSERT INTO MARKS(BOOK_ID, USER_ID, MARK)" +
                $"VALUES({CurrentBook.BOOK_ID} , {WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser}, {mark})");
            db.Database.ExecuteSqlCommand(command);
            command = String.Format($"SELECT COUNT(*) FROM MARKS WHERE BOOK_ID = {CurrentBook.BOOK_ID}");
            var count = db.Database.SqlQuery<int>(command);
            foreach(var i in count)
            {
                CurrentBook.NUMBEROFVOICES =(int)i;
            }
            db.SaveChangesAsync().GetAwaiter();
        }
    }
}
