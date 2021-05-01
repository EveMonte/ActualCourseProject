using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data.Entity;
using Курсач;
using System.Data.Entity.Validation;
using Ubiety.Dns.Core;

namespace Курсач.Models
{

    [Table("Books")]

    public class Book : INotifyPropertyChanged
    {
        #region Data
        private string title;
        private string author;
        private long numberOfPages;
        private string genre;
        private string description;
        private string cover;
        private string category;
        private decimal rating;
        private long numberOfVoices;

        [Required(ErrorMessage = "Name is necessary field")]
        public string TITLE
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("TITLE");
            }
        }
        [Required(ErrorMessage = "Author is necessary field")]
        public string AUTHOR
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged("AUTHOR");
            }
        }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid number of pages!")]
        public long PAGES
        {
            get { return numberOfPages; }
            set
            {
                numberOfPages = value;
                OnPropertyChanged("PAGES");
            }
        }
        [Required(ErrorMessage = "Genre is necessary field")]
        public string GENRE
        {
            get { return genre; }
            set
            {
                genre = value;
                OnPropertyChanged("GENRE");
            }
        }
        public string DESCRIPTION
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("DESCRIPTION");
            }
        }
        public string COVER
        {
            get { return cover; }
            set
            {
                cover = value;
                OnPropertyChanged("COVER");
            }
        }
        public decimal RATING
        {
            get { return rating; }
            set
            {
                rating = value;
                OnPropertyChanged("RATING");
            }
        }
        public long NUMBEROFVOICES
        {
            get { return numberOfVoices; }
            set
            {
                numberOfVoices = value;
                OnPropertyChanged("NUMBEROFVOICES");
            }
        }
        #endregion
        public static List<Book> listOfBooks;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        /*public static List<Book> GetBooksFromDataBase()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Books.Load();
                db.Books.Local.ToBindingList();
                listOfBooks = db.Books.ToList();
                return db.Books.ToList();
            }
        }

        public static void SetBooksToDataBase(ObservableCollection<Book> obsBooks)
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Books.RemoveRange(db.Books);
                foreach (Book book in obsBooks)
                {
                    db.Books.Add(book);
                }
                var results = new List<ValidationResult>();
                listOfBooks = obsBooks.ToList();
                string errors = "";
                foreach (Book book in obsBooks)
                {
                    var context = new ValidationContext(book);
                    if (!Validator.TryValidateObject(book, context, results, true))
                    {
                        foreach (var error in results)
                        {
                            errors += error.ErrorMessage + "\n";
                        }

                    }
                }
                if (errors != "")
                    MessageBox.Show(errors);
                else
                {
                    db.SaveChanges();
                    MessageBox.Show("Изменения сохранены в Базу Данных");
                }
            }
        }*/
    }

}


