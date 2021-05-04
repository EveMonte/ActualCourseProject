using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач;
using Курсач.Models;
using System.Data.SQLite;
using System.Data.Entity;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;
using Курсач.Commands;

namespace Курсач.ViewModels
{
    public class MainFrameViewModel : BaseViewModel
    {
        #region Data
        User currentUser;
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        private bool themeChoice;
        public bool ThemeChoice
        {
            get { return themeChoice; }
            set 
            { 
                themeChoice = value;
                OnPropertyChanged("ThemeChoice");            
            }
        }
        #endregion

        #region Commands
        public ICommand OpenWorkFrameCommand { get; private set; } // Команда открытия рабочего окна
        public ICommand ToggleCommand { get; private set; } // Команда переключения темы
        public ICommand RegistrationCommand { get; private set; } // Команда открытия user control'а с формой регистрации
        #endregion

        void ChangeTheme(object obj) //Смена темы
        {
            Uri uri;
            if (!themeChoice)
            {
                // определяем путь к файлу ресурсов
                uri = new Uri("Styles\\lightTheme.xaml", UriKind.Relative);
            }
            else
            {
                uri = new Uri("Styles\\darkTheme.xaml", UriKind.Relative);
            }
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            //Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        //Конструктор
        public MainFrameViewModel()
        {
            SelectedViewModel = new RegisterViewModel();
            ToggleCommand = new DelegateCommand(ChangeTheme);         
        }
    }
}
