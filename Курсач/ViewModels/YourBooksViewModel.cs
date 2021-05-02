﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class YourBooksViewModel : BaseViewModel
    {
        #region Data
        public ObservableCollection<BOOKS> Books { get; set; }
        public ObservableCollection<GENRES> Genres { get; private set; }
        public IQueryable<BOOKS> coll { get; set; }
        public USERS currentUser;
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
        public ICommand OpenFullInfo { get; private set; }
        BOOKS selectedBook;
        public BOOKS SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
        #endregion

        public ICommand FindByGenreCommand { get; private set; }
        public YourBooksViewModel()
        {
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            Books = new ObservableCollection<BOOKS>();
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                string command = String.Format($"SELECT * " +
                                               $"FROM YOUR_BOOKS WHERE USER_ID = {currentUser.USER_ID}");
                var h = (library.Database.SqlQuery<YOUR_BOOKS>(command));
                foreach(YOUR_BOOKS book in h)
                {
                    BOOKS b = library.BOOKS.Where(n => n.BOOK_ID == book.BOOK_ID).FirstOrDefault();
                    command = String.Format($"SELECT top(1) * FROM MARKS WHERE BOOK_ID = {book.BOOK_ID} AND USER_ID = {currentUser.USER_ID}");
                    var marks = library.Database.SqlQuery<MARKS>(command);
                    foreach (var m in marks)
                    {
                        b.Mark = (int)m.MARK;
                    }
                    command = String.Format($"SELECT * FROM MARKS WHERE BOOK_ID = {book.BOOK_ID}");
                    var rating = library.Database.SqlQuery<MARKS>(command);
                    decimal sum = 0;
                    foreach (var m in rating)
                    {
                        sum += (decimal)m.MARK;
                    }
                    b.NUMBEROFVOICES = rating.Count();

                    if (b.NUMBEROFVOICES != 0)
                    {
                        b.RATING = sum / b.NUMBEROFVOICES;
                    }
                    command = String.Format($"SELECT * " +
                                               $"FROM GENRES WHERE GENRE_ID = {b.GENRE}");
                    var genres = library.Database.SqlQuery<GENRES>(command);

                    foreach (var m in genres)
                    {
                        b.Genre = m.GENRE;
                    }                     
                    Books.Add(b);
                }
                //Books = new ObservableCollection<BOOKS>(library.BOOKS);
                library.SaveChanges();
                Genres = new ObservableCollection<GENRES>(library.GENRES.OrderBy(n => n.GENRE));
            }
            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
            FindByGenreCommand = new DelegateCommand(FindByGenre);
        }

        private void FindByGenre(object obj)
        {
            Books = new ObservableCollection<BOOKS>(Books.Where(n => n.GENRE == SelectedGenre.GENRE_ID));
        }

        private void OpenFullInfoUserControl(object obj)
        {
            FullInfoViewModel.book = SelectedBook;
            FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel());
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.CurrentPageViewModel = new AdditionalInfoViewModel();
        }
        #region Filter
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(YourBooksViewModel), new PropertyMetadata("", TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as YourBooksViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.Search;
            }
        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(YourBooksViewModel), new PropertyMetadata(null));
        private bool Search(object obj)
        {
            bool result = true;
            BOOKS current = obj as BOOKS;

            if (current != null && !string.IsNullOrWhiteSpace(Text) && !current.TITLE.Contains(Text) && !current.AUTHOR.Contains(Text))
            {
                result = false;
            }
            return result;
        }
        #endregion
    }
}
