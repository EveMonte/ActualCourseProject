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
using System.Data.Entity;
using System.Data.SQLite;
using System.ComponentModel;
using Курсач.ViewModels;
using System.IO;
using System.Windows.Threading;
using Курсач.Singleton;

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
            WorkFrameSingleTone.GetInstance();
            DataContext = WorkFrameSingleTone.GetInstance().WorkframeViewModel;
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
