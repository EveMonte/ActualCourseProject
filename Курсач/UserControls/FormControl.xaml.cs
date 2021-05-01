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
    /// Логика взаимодействия для FormControl.xaml
    /// </summary>
    public partial class FormControl : UserControl
    {
        RegisterViewModel registerViewModel;
        public FormControl()
        {
            registerViewModel = new RegisterViewModel();
            InitializeComponent();
            DataContext = registerViewModel;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb.SelectedIndex == 0)
            {
                Properties.Settings.Default.languageCode = "ru-RU";
            }
            else
                Properties.Settings.Default.languageCode = "en-US";
            Properties.Settings.Default.Save();

        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            registerViewModel.Password = passwordTextBox.Password;

        }
    }
}
