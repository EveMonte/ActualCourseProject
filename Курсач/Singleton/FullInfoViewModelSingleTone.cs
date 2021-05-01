using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач.ViewModels;

namespace Курсач.Singleton
{
    class FullInfoViewModelSingleTone
    {
        private static FullInfoViewModelSingleTone _instance;
        public FullInfoViewModel FullInfoViewModel { get; set; }
        private FullInfoViewModelSingleTone(FullInfoViewModel viewModel)
        {
            FullInfoViewModel = viewModel;
        }
        public static FullInfoViewModelSingleTone GetInstance(FullInfoViewModel fullInfoViewModel = null)
        {
            return _instance ?? (_instance = new FullInfoViewModelSingleTone(fullInfoViewModel));
        }
    }
}
