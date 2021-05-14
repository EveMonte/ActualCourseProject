using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач.ViewModels;

namespace Курсач.Singleton
{
    class AdminWindowSingleTone
    {
        private static AdminWindowSingleTone _instance;
        public AdminVM AdminVM { get; set; }
        private AdminWindowSingleTone(AdminVM viewModel)
        {
            AdminVM = viewModel;
        }
        public static AdminWindowSingleTone GetInstance(AdminVM fullInfoViewModel = null)
        {
            return _instance ?? (_instance = new AdminWindowSingleTone(fullInfoViewModel));
        }
    }
}
