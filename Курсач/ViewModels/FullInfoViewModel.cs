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
        public FullInfoViewModel()
        {

        }
    }
}
