using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Курсач.Commands;
using Курсач.Models;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
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
        List<USERS> listOfUsers;
        public string firstPassword = "";
        public string FirstPassword
        {
            get { return firstPassword; }
            set
            {
                firstPassword = value;
                OnPropertyChanged("firstPassword");
            }
        }
        public string secondPassword = "";
        public string SecondPassword
        {
            get { return secondPassword; }
            set
            {
                secondPassword = value;
                OnPropertyChanged("Password");
            }
        }
        public string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        public ICommand RegistrationCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }

        // Если пользователя с таким email не существует, введенные пароли совпадают и длина паролей больше 6 символов
        bool CanExecute(object parametr)
        {
            using (LIBRARYEntities db = new LIBRARYEntities())
            {
                var users = db.USERS;
                foreach (USERS u in users)
                {
                    if (u.EMAIL != Email && firstPassword == secondPassword && firstPassword.Length > 6)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        private static string generatedCode;
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
                MessageBox.Show("Письмо отправлено");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при попытке отправить сообщение: {ex.Message}");
            }
        }

        // Вызываем отправление письма и открываем user control для ввода кода
        void Execute(object parametr)
        {
            generatedCode = GenerateCode();
            SendEmailAsync(Email, generatedCode).GetAwaiter();
            SendMessageViewModelSingleton.GetInstance(new SendMessageViewModel());
            SendMessageViewModelSingleton.GetInstance().SendMessageViewModel.newUser = new USERS { ACCOUNT = "User", EMAIL = Email, NAME = Name, PASSWORD = FirstPassword, SECOND_NAME = SecondName };
            SendMessageViewModelSingleton.GetInstance().SendMessageViewModel.generatedCode = generatedCode;

            MainWindowViewModelSingleton.GetInstance().MainFrameViewModel.SelectedViewModel = //new SendMessageViewModel();
                SendMessageViewModelSingleton.GetInstance().SendMessageViewModel;
            /*foreach (Employee employee in employees)
            {
                if (employee.Email == user.email)
                {
                    user.TypeOfUser = "Administrator";
                    MessageBox.Show("Hello admin!");
                }
            }*/
            //SendMessageViewModelSingleton.GetInstance().SendMessageViewModel.newUser = new User { Email = Email, Name = Name, Password = firstPassword, SecondName = SecondName, Subscription }

        }
        public RegistrationViewModel()
        {
            using (LIBRARYEntities library = new LIBRARYEntities())
            {
                listOfUsers = library.USERS.ToList<USERS>();
            }
            RegistrationCommand = new DelegateCommand(Execute, CanExecute);
        }
    }
}
