using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач.ViewModels;

namespace Курсач.Singleton
{
    public class SendMessageViewModelSingleton
    {
        private static SendMessageViewModelSingleton _instance;
        public SendMessageViewModel SendMessageViewModel { get; set; }
        private SendMessageViewModelSingleton(SendMessageViewModel mainView)
        {
            SendMessageViewModel = mainView;
        }
        public static SendMessageViewModelSingleton GetInstance(SendMessageViewModel sendMessageViewModel = null)
        {
            return _instance ?? (_instance = new SendMessageViewModelSingleton(sendMessageViewModel));
        }
    }
}
