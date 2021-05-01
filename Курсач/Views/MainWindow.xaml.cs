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
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Data.Entity;
using System.Data.SQLite;
using Курсач.Models;
using Курсач.ViewModels;
using Курсач.Singleton;

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainFrameViewModel mainFrameViewModel;
        public MainWindow()
        {
            mainFrameViewModel = new MainFrameViewModel();
            MainWindowViewModelSingleton.GetInstance(mainFrameViewModel);
            this.Loaded += MainWindow_Loaded;
            InitializeComponent();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = mainFrameViewModel;
        }


        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
