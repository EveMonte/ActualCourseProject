using System;
using System.Windows;
using Курсач.ViewModels;
using Курсач.Singleton;

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = MainWindowViewModelSingleton.GetInstance(new MainFrameViewModel()).MainFrameViewModel;
            InitializeComponent();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
