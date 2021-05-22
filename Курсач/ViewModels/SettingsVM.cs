using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        private int newLang;

        public int NewLang
        {
            get { return newLang; }
            set 
            { 
                newLang = value;
                OnPropertyChanged("NewLang");
            }
        }
        private int themeChoice;

        public int ThemeChoice
        {
            get { return themeChoice; }
            set 
            { 
                themeChoice = value;
                OnPropertyChanged("ThemeChoice");
            }
        }

        public ICommand ChangeLangCommand { get; private set; }
        public ICommand ChangeThemeCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand OpenInfoCommand { get; private set; }
        public SettingsVM()
        {
            ChangeLangCommand = new DelegateCommand(ChangeLanguage);
            ChangeThemeCommand = new DelegateCommand(ChangeTheme);
            ExitCommand = new DelegateCommand(ExitApp);
            OpenInfoCommand = new DelegateCommand(OpenInfo);
            CultureInfo currLang = App.Language;

            //Заполняем меню смены языка:
        }

        private void OpenInfo(object obj)
        {
            if (App.currentUser.ACCOUNT == "Пользователь")
                WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = new AppInfoVM();
            else AdminWindowSingleTone.GetInstance().AdminVM.AddCreditCardViewModel = new AppInfoVM();
        }

        private void ExitApp(object obj)
        {
            var window = Application.Current.Windows[0];
            if (window != null)
                window.Close();
        }

        private void ChangeTheme(object obj)
        {
            Uri uri;
            if (ThemeChoice == 0)
            {
                // определяем путь к файлу ресурсов
                uri = new Uri("Themes\\lightTheme.xaml", UriKind.Relative);
            }
            else
            {
                uri = new Uri("Themes\\darkTheme.xaml", UriKind.Relative);
            }
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            //Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);        
        }

        private void ChangeLanguage(object obj)
        {
            if (NewLang == 0)
            {
                CultureInfo lang = new CultureInfo("ru-RU");
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
            else
            {
                CultureInfo lang = new CultureInfo("en-EN");
                if (lang != null)
                {
                    App.Language = lang;
                }
            }

        }
    }
}
