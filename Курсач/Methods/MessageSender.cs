using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Methods
{
    public class MessageSender
    {
        //Асинхронная отправка сообщения на почту
        public static async Task SendEmailAsync(string email, string code, string message, string subject)
        {
            try
            {
                MailAddress from = new MailAddress("bookvar.official@gmail.com", "Администрация онлайн-библиотеки Bookварь");
                MailAddress to = new MailAddress(email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = subject;
                m.Body = message;
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
        public static string GenerateCode() //Генерация кода
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
    }
}
