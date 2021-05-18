using System;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace Курсач.ViewModels
{
    public class MainFrameViewModel : BaseViewModel
    {
        #region Data
        public Notifier notifier;
        public LIBRARYEntities db = new LIBRARYEntities();
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

            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
    }
}
