using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач.ViewModels;

namespace Курсач.Singleton
{
    public class MainWindowViewModelSingleton
    {
        private static MainWindowViewModelSingleton _instance;
        public MainFrameViewModel MainFrameViewModel { get; set; }
        private MainWindowViewModelSingleton(MainFrameViewModel mainView)
        {
            MainFrameViewModel = mainView;
        }
        public static MainWindowViewModelSingleton GetInstance(MainFrameViewModel mainFrameViewModel = null)
        {
            return _instance ?? (_instance = new MainWindowViewModelSingleton(mainFrameViewModel));
        }
    }
}
