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
using Курсач.Methods;

namespace Курсач.Components
{
    /// <summary>
    /// Логика взаимодействия для BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        SaltedHash saltedHash;
        private bool flag;
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox), 
                new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is BindablePasswordBox bindablePasswordBox)
            {
                bindablePasswordBox.UpdatePassword();
            }
        }

        private void UpdatePassword()
        {
            if (!flag)
            {
                pswrdBx.Password = Password;
            }
        }

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void pswrdBx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            flag = true;
            Password = pswrdBx.Password;//(new SaltedHash(pswrdBx.Password)).Hash;
            flag = false;
        }
    }
}
