using System;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class SendMessageViewModel : BaseViewModel
    {
        public USERS newUser;
        public string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }
        private string generatedCode;



        public ICommand SendMessageCommand { get; private set; }
        public ICommand SendNewMessageCommand { get; private set; }
        public ICommand OpenRegistrationCommand { get; private set; }
        private void OpenRegistration(object obj)
        {
            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = new RegistrationViewModel();
            code = "";
        }
        public void OpenWorkframe(object parameter)
        {
            try 
            {
                if (code == generatedCode)
                {
                    App.db.USERS.Add(newUser);
                    App.db.SaveChangesAsync().GetAwaiter();
                    App.currentUser = newUser;
                    Workframe workframe = new Workframe();
                    App.CreateNotifier(workframe);
                    workframe.Show();
                    WorkFrameSingleTone.GetInstance(new WorkframeViewModel());
                    var windows = Application.Current.Windows;
                    foreach (Window window in windows)
                        if (window != null && window is MainWindow)
                            window.Close();
                }
                else 
                {
                    App.notifier.ShowWarning("Неправильный код");
                }
            }
            
            catch(Exception ex)
            {
                App.notifier.ShowError(ex.Message);
            }
        }
        private void SendNewMessage(object obj)
        {
            generatedCode = MessageSender.GenerateCode();
            string message = $"Введите символьный код, расположенный ниже, в приложение:\n{generatedCode}\nНикому не давайте его!";
            MessageSender.SendEmailAsync(newUser.EMAIL, generatedCode, message, "Код подтверждения").GetAwaiter();
        }

        //Constructors
        public SendMessageViewModel(USERS user)
        {
            newUser = user;
            SendMessageCommand = new DelegateCommand(OpenWorkframe);
            OpenRegistrationCommand = new DelegateCommand(OpenRegistration);
            SendNewMessageCommand = new DelegateCommand(SendNewMessage);
            generatedCode = MessageSender.GenerateCode();
            string message = $"Введите символьный код, расположенный ниже, в приложение:\n{generatedCode}\nНикому не давайте его!";
            MessageSender.SendEmailAsync(newUser.EMAIL, generatedCode, message, "Код подтверждения").GetAwaiter();
        }
    }
}
