using System;
using System.Windows;
using System.Windows.Input;
using Курсач.Methods;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class SendMessageViewModel : BaseViewModel
    {
        public USERS newUser;
        LIBRARYEntities db;
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
            if (code == generatedCode)
            {
                db = new LIBRARYEntities();
                db.USERS.Add(newUser);
                db.SaveChanges();
                WorkFrameSingleTone.GetInstance(new WorkframeViewModel(newUser));

                Workframe workframe = new Workframe();
                workframe.Show();
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        window.Close();
                        break;
                    }
                }

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
