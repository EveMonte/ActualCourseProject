using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.ViewModels
{
    public class AbonementBookVM : BaseViewModel
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
        public AbonementBookVM()
        {

        }
    }
}
