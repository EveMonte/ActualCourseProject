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

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        RegistrationViewModel registrationViewModel;
        public RegisterControl()
        {
            InitializeComponent();
            registrationViewModel = new RegistrationViewModel();
            DataContext = registrationViewModel;
        }

        private void password1Box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            registrationViewModel.FirstPassword = password1Box.Password;
        }

        private void password2Box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            registrationViewModel.SecondPassword = password2Box.Password;

        }
    }
}
