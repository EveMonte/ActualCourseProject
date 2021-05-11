using System;
using System.Windows;
using System.Windows.Input;

namespace Курсач.ViewModels
{
    public class MainFrameViewModel : BaseViewModel
    {
        #region Data
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

        //Конструктор
        public MainFrameViewModel()
        {
            SelectedViewModel = new RegisterViewModel();
            ToggleCommand = new DelegateCommand(ChangeTheme);         
        }
    }
}
