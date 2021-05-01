using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Курсач
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string databaseName = "LibraryDataBase.db";
        static string filePath = @"C:\Users\User\Desktop\Курсааааач\Курсач\Курсач\LibraryDataBase.db";
        public static string databasePath = System.IO.Path.Combine(filePath, databaseName);
        protected override void OnStartup(StartupEventArgs e)
        {
            var langCode = Курсач.Properties.Settings.Default.languageCode;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langCode);
            base.OnStartup(e);
        }
    }
}
