using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач.Models;

namespace Курсач.ViewModels
{
    public class FullInfoViewModel : BaseViewModel
    {
        public static BOOKS book;
        private Book currentBook;
        LIBRARYEntities db = new LIBRARYEntities();
        public Book CurrentBook
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
            CurrentBook = new Book();
            CurrentBook.TITLE = book.TITLE;
            CurrentBook.AUTHOR = book.AUTHOR;
            CurrentBook.COVER = book.COVER;
            CurrentBook.DESCRIPTION = book.DESCRIPTION;
            string command = String.Format($"SELECT * " +
                $"FROM GENRES");
            var h = (db.Database.SqlQuery<GENRES>(command));
            foreach(GENRES genre in h)
            {
                if (genre.GENRE_ID == book.GENRE)
                    CurrentBook.GENRE = genre.GENRE;
            }
            command = String.Format($"SELECT COUNT(*) FROM MARKS WHERE BOOK_ID = {book.BOOK_ID}");
            var a = db.Database.SqlQuery<int>(command);
            foreach(var b in a)
            {
                int s = b;
                CurrentBook.NUMBEROFVOICES = s;
            }
            CurrentBook.RATING = book.RATING;
        }
    }
}
