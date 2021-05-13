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

        #endregion

        #region Commands
        public ICommand OpenWorkFrameCommand { get; private set; } // Команда открытия рабочего окна
        public ICommand RegistrationCommand { get; private set; } // Команда открытия user control'а с формой регистрации
        #endregion


        //Конструктор
        public MainFrameViewModel()
        {
            SelectedViewModel = new RegisterViewModel();
        }
    }
}
