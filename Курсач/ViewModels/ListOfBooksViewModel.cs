﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class ListOfBooksViewModel : BaseViewModel
    {
        #region Data
        private USERS User = new USERS();
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

        private double opacityAnimationUp = 1;

        public double OpacityAnimationUp
        {
            get { return opacityAnimationUp; }
            set 
            { 
                opacityAnimationUp = value;
                OnPropertyChanged("OpacityAnimationUp");
            }
        }

        private double opacityAnimationDown = 0;

        public double OpacityAnimationDown
        {
            get { return opacityAnimationDown; }
            set
            {
                opacityAnimationDown = value;
                OnPropertyChanged("OpacityAnimationDown");
            }
        }

        private string imageSourceUp;

        public string ImageSourceUp
        {
            get { return imageSourceUp; }
            set 
            { 
                imageSourceUp = value;
                OnPropertyChanged("ImageSourceUp");
            }
        }

        bool animation = true;
        int phase = 0;

        private string imageSourceDown;

        public string ImageSourceDown
        {
            get { return imageSourceDown; }
            set
            {
                imageSourceDown = value;
                OnPropertyChanged("ImageSourceDown");
            }
        }

        List<ADVERTISEMENT> ListOfAdvertisement;
        #endregion

        #region Commands
        public ICommand OpenFullInfo { get; private set; }
        public ICommand FindByGenreCommand { get; private set; }
        #endregion

        #region Commands' Logic
        private void FindByGenre(object obj)
        {
            Books = new ObservableCollection<BOOKS>(Books.Where(n => n.GENRE == SelectedGenre.GENRE_ID));
        }

        private void OpenFullInfoUserControl(object obj) // Open page with extended info
        {
            foreach (GENRES genre in App.db.GENRES.ToList()) //we are looking for our book in GENRES...
            {
                if (genre.GENRE_ID == SelectedBook.GENRE)
                    SelectedBook.Genre = genre.GENRE; //... and when we find it we write it in the notmapped property
            }
            SelectedBook.NUMBEROFVOICES = App.db.MARKS.Where(n => n.BOOK_ID == SelectedBook.BOOK_ID).Count(); //counting marks to write in notmapped property
            MARKS mark = App.db.MARKS.FirstOrDefault(n => (n.USER_ID == User.USER_ID) && (n.BOOK_ID == SelectedBook.BOOK_ID));
            SelectedBook.Mark = mark != null ? (int)mark.MARK : 0;
            FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel()).FullInfoViewModel.CurrentBook = SelectedBook;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.CurrentPageViewModel = new AdditionalInfoViewModel();
        }
        #endregion

        //Constructor
        public ListOfBooksViewModel(USERS user)
        {
            User = user;
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                Books = new ObservableCollection<BOOKS>(library.BOOKS);
                var shelfBooks = App.db.YOUR_BOOKS.Where(n => n.USER_ID == user.USER_ID);
                foreach(var book in shelfBooks)
                {
                    var bookToRemove = Books.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID);
                    if (bookToRemove != null)
                    {
                        Books.Remove(bookToRemove);
                    }
                }
                var basketBooks = App.db.BASKETS.Where(n => n.USER_ID == user.USER_ID);
                foreach (var book in Books)
                {
                    if (basketBooks.FirstOrDefault(n => n.BOOK_ID == book.BOOK_ID) != null)
                    {
                        book.IsInBasket = 1;
                    }
                    else
                    {
                        book.IsInBasket = 0;
                    }
                }
                Genres = new ObservableCollection<GENRES>(library.GENRES.OrderBy(n => n.GENRE));
            }
            foreach (BOOKS book in Books) //check books. If book is available by subscription, we place band
            {
                if (book.CATEGORY == "Подписка")
                {
                    book.Subscription = 1;
                }
                else
                {
                    book.Subscription = 0;
                }
                book.FormattedPrice = ConvertDecimal.RemoveZeroes(book.PRICE);
            }
            OpenFullInfo = new DelegateCommand(OpenFullInfoUserControl);
            Items = CollectionViewSource.GetDefaultView(Books);
            Items.Filter = Search;
            FindByGenreCommand = new DelegateCommand(FindByGenre);

            ListOfAdvertisement = App.db.ADVERTISEMENT.ToList();

            ImageSourceUp = ListOfAdvertisement[0].IMAGE_SOURCE;
            ImageSourceDown = ListOfAdvertisement[1].IMAGE_SOURCE;


            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 2);
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (animation)
            {
                if(phase >= 50)
                {
                    if (OpacityAnimationUp < 0.05)
                    {
                        phase = 0;
                        animation = !animation;
                    }
                    else
                    {
                        OpacityAnimationUp -= 0.05;
                        OpacityAnimationDown += 0.05;

                    }
                }
            }
            else
            {
                if (phase >= 50)
                {
                    if (OpacityAnimationUp > 0.95)
                    {
                        phase = 0;
                        animation = !animation;
                    }
                    else
                    {
                        OpacityAnimationUp += 0.05;
                        OpacityAnimationDown -= 0.05;
                    }
                }
            }
            phase++;
            Console.WriteLine(phase);
        }
        #region Filter
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ListOfBooksViewModel), new PropertyMetadata("", TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ListOfBooksViewModel;
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
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ListOfBooksViewModel), new PropertyMetadata(null));
        private bool Search(object obj)
        {
            bool result = true;
            BOOKS current = obj as BOOKS;

            if (current != null && !string.IsNullOrWhiteSpace(Text) && !current.TITLE.ToLower().Contains(Text.ToLower()) && !current.AUTHOR.ToLower().Contains(Text.ToLower()))
            {
                result = false;
            }
            return result;
        }
        #endregion
    }
}
