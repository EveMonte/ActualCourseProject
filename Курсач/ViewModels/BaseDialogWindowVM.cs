﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.ViewModels
{
    public class BaseDialogWindowVM : BaseViewModel
    {
        private BaseViewModel currentVM;

        public BaseViewModel CurrentVM
        {
            get { return currentVM; }
            set 
            { 
                currentVM = value;
                OnPropertyChanged("CurrentVM");
            }
        }
        public BaseDialogWindowVM(BaseViewModel vm)
        {
            CurrentVM = vm;
        }
    }
}
