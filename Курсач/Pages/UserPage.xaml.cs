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
using Курсач.ViewModels;

namespace Курсач.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            //DataContext = new UserPageVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            passwordStk.Visibility = Visibility.Visible;
        }
        private void Button_Click_Email(object sender, RoutedEventArgs e)
        {
            //emailStk.Visibility = Visibility.Visible;
        }
    }
}
