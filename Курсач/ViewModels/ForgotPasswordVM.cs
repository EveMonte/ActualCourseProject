using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class ForgotPasswordVM : BaseViewModel
    {
        Notifier notifier;
        private string email;

        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public ICommand SendMessageCommand { get; private set; }
        public ICommand SendNewMessageCommand { get; private set; }
        public ICommand OpenSignInCommand { get; private set; }
        public ForgotPasswordVM()
        {
            OpenSignInCommand = new DelegateCommand(OpenSignIn);
            SendNewMessageCommand = new DelegateCommand(SendMessage);
            SendMessageCommand = new DelegateCommand(SendMessage);

            MainWindow thisWin = null;
            foreach (Window win in Application.Current.Windows)
            {
                if (win is MainWindow)
                {
                    thisWin = win as MainWindow;
                }
            }

            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: thisWin,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        private void SendMessage(object obj)
        {
            string Code = MessageSender.GenerateCode();
            string message = String.Format($"С Вашего аккаунта поступил запрос на восстановление пароля. Если он" +
                $" поступил не от вас, то незамедлительно свяжитесь с администрацией BookВарь.\nЕсли это Ваш запрос," +
                $" то введите код, расположенный ниже, в качестве пароля во время следующего Вашего входа. При входе в приложение сразу поменяйти пароль!\n{Code}");
            try
            {
                var user = App.db.USERS.FirstOrDefault(n => n.EMAIL == Email);
                if (user != null)
                {
                    MessageSender.SendEmailAsync(Email, Code, message, "Восстановление пароля").GetAwaiter();
                    SaltedHash sh = new SaltedHash(Code);
                    user.PASSWORD = sh.Hash + sh.Salt;
                    App.db.SaveChangesAsync().GetAwaiter();
                    notifier.ShowSuccess("Сообщение отправлено. Проверьте Вашу почту");
                    MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new RegisterViewModel();
                }
                else
                {
                    notifier.ShowWarning("Пользователя с таким email не существует.\nПроверьте правильность написания email и повторите попытку");
                }
            }
            catch(Exception ex)
            {
                notifier.ShowError($"Произошла ошибка\n" +
                    $"{ex.Message}" +
                    $"\nИзвините за причиненные неудобства");
            }
        }


        private void OpenSignIn(object obj)
        {
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new RegisterViewModel();
        }
    }
}
