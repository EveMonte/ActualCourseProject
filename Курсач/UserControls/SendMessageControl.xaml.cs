using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Курсач.Singleton;
using Курсач.ViewModels;

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для SendMessageControl.xaml
    /// </summary>
    public partial class SendMessageControl : UserControl
    {
        public SendMessageControl()
        {
            InitializeComponent();
            //DataContext = SendMessageViewModelSingleton.GetInstance().SendMessageViewModel;
            //DataContext = new SendMessageViewModel();
        }

    }
}
