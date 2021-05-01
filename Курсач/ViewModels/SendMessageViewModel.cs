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
        public string generatedCode;


        public void Execute(object parameter)
        {
            if (code == generatedCode)
            {
                db = new LIBRARYEntities();
                db.USERS.Add(newUser);
                db.SaveChanges();
                Workframe workframe = new Workframe();
                workframe.Show();
            }
        }
        public ICommand SendMessageCommand { get; private set; }

        public SendMessageViewModel()
        {
            SendMessageCommand = new DelegateCommand(Execute);

        }
    }
}
