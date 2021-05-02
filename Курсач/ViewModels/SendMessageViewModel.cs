using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Курсач.Models;
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
            }
        }
        private void SendNewMessage(object obj)
        {
            generatedCode = GenerateCode();
            SendEmailAsync(newUser.EMAIL, generatedCode).GetAwaiter();
        }
        public SendMessageViewModel(USERS user)
        {
            newUser = user;
            SendMessageCommand = new DelegateCommand(OpenWorkframe);
            OpenRegistrationCommand = new DelegateCommand(OpenRegistration);
            SendNewMessageCommand = new DelegateCommand(SendNewMessage);
            generatedCode = GenerateCode();
            SendEmailAsync(newUser.EMAIL, generatedCode).GetAwaiter();
        }

        #region Some Logic
        private string GenerateCode() //Генерация кода
        {
            string code = "";
            string forGeneration = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                code += forGeneration[random.Next(forGeneration.Length)];
            }
            return code;
        }
        //Асинхронная отправка сообщения на почту
        private static async Task SendEmailAsync(string email, string code)
        {
            try
            {
                MailAddress from = new MailAddress("bookvar.official@gmail.com", "Администрация онлайн-библиотеки Bookварь");
                MailAddress to = new MailAddress(email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Код подтверждения";
                m.Body = $"Введите символьный код, расположенный ниже, в приложение:\n{code}\nНикому не давайте его!";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("bookvar.official@gmail.com", "rm.dthnb");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при попытке отправить сообщение: {ex.Message}");
            }
        }
        #endregion
    }
}
