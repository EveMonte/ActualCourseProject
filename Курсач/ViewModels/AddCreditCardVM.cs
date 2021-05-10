using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class AddCreditCardVM : BaseViewModel
    {
        [Required]
        private string creditCard;
        public string CREDIT_CARD
        {
            get
            {
                return creditCard;
            }
            set
            {
                creditCard = value;
                OnPropertyChanged("CREDIT_CARD");
            }
        }
        [Required]
        private string validity;
        public string Validity
        {
            get
            {
                return validity;
            }
            set
            {
                validity = value;
                OnPropertyChanged("Validity");
            }
        }
        [Required]
        private int cvv;
        public int CVV
        {
            get
            {
                return cvv;
            }
            set
            {
                cvv = value;
                OnPropertyChanged("CVV");
            }
        }
        private string CreditCard;
        LIBRARYEntities db = new LIBRARYEntities();
        USERS currentUser;
        public ICommand CloseUserPageCommand { get; private set; }
        public ICommand AddCardCommand { get; private set; }
        public AddCreditCardVM()
        {
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            CloseUserPageCommand = new DelegateCommand(Close);
            AddCardCommand = new DelegateCommand(AddCard);
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Visible";
        }

        private void AddCard(object obj)
        {
            var user = db.USERS.FirstOrDefault(n => n.USER_ID == currentUser.USER_ID);
            user.CREDIT_CARD = CREDIT_CARD;
            db.SaveChangesAsync().GetAwaiter();
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser.CREDIT_CARD = CREDIT_CARD;
            Close(new object());

        }

        private void Close(object obj)
        {
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.AddCreditCardViewModel = null;
            WorkFrameSingleTone.GetInstance().WorkframeViewModel.Visibility = "Collapsed";
        }
    }
}
