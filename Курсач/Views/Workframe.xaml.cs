using System.Windows;
using Курсач.Singleton;
using Курсач.ViewModels;

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для Workframe.xaml
    /// </summary>
    public partial class Workframe : Window
    {
        bool hidden = true;
        public Workframe()
        {
            InitializeComponent();
            this.Loaded += WorkFrame_Loaded;
        }

        private void WorkFrame_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = WorkFrameSingleTone.GetInstance(new WorkframeViewModel()).WorkframeViewModel;
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
