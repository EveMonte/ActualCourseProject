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
        public FullInfoViewModel()
        {
            //CurrentBook = book;
         
        }
    }
}
