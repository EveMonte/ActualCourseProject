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
using System.Windows.Shapes;
using Курсач.Singleton;
using Курсач.ViewModels;

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private bool hidden = true;
        public AdminWindow()
        {
            InitializeComponent();
            DataContext = AdminWindowSingleTone.GetInstance().AdminVM;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (hidden)
            {
                GridLengthConverter conv = new GridLengthConverter();
                col.Width = (GridLength)conv.ConvertFrom(180);
                hidden = false;
            }
            else
            {
                GridLengthConverter conv = new GridLengthConverter();
                col.Width = (GridLength)conv.ConvertFrom(60);
                hidden = true;
            }
        }
    }
}
