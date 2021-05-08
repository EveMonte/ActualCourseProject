using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class SubscriptionVM : BaseViewModel
    {
        LIBRARYEntities db = new LIBRARYEntities();
        private USERS currentUser;
        public ICommand YearSubscriptionCommand { get; private set; }
        public ICommand MonthSubscriptionCommand { get; private set; }

        public SubscriptionVM()
        {
            YearSubscriptionCommand = new DelegateCommand(YearSubscription);
            MonthSubscriptionCommand = new DelegateCommand(MonthSubscription);
        }

        private void MonthSubscription(object obj)
        {
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            var a = db.SUBSCRIPTIONS.FirstOrDefault(n => n.SUBSCRIPTION_ID == currentUser.SUBSCRIPTION);
            if (db.SUBSCRIPTIONS.Where(n => n.SUBSCRIPTION_ID == currentUser.SUBSCRIPTION).FirstOrDefault() == null)
            {
                SUBSCRIPTIONS sub = new SUBSCRIPTIONS();
                sub.SUBSCRIPTION_DATE = DateTime.Now;
                sub.LENGTH = 2;
                db.SUBSCRIPTIONS.Add(sub);
                USERS user = db.USERS.Where(n => n.USER_ID == currentUser.USER_ID).FirstOrDefault();
                user.SUBSCRIPTION = sub.SUBSCRIPTION_ID;
                db.SaveChangesAsync();
            }
            else if((DateTime.Now.Month - a.SUBSCRIPTION_DATE.Month) + 12 * (DateTime.Now.Year - a.SUBSCRIPTION_DATE.Year) < a.LENGTH)
            {
                a.LENGTH += 1;
                db.SaveChangesAsync();
            }
            else
            {
                a.LENGTH = 1;
                a.SUBSCRIPTION_DATE = DateTime.Now;
                db.SaveChangesAsync();
            }
        }

        private void YearSubscription(object obj)
        {
            currentUser = WorkFrameSingleTone.GetInstance().WorkframeViewModel.currentUser;
            var a = db.SUBSCRIPTIONS.FirstOrDefault(n => n.SUBSCRIPTION_ID == currentUser.SUBSCRIPTION);
            if (db.SUBSCRIPTIONS.Where(n => n.SUBSCRIPTION_ID == currentUser.SUBSCRIPTION).FirstOrDefault() == null)
            {
                SUBSCRIPTIONS sub = new SUBSCRIPTIONS();
                sub.SUBSCRIPTION_DATE = DateTime.Now;
                sub.LENGTH = 13;
                db.SUBSCRIPTIONS.Add(sub);
                USERS user = db.USERS.Where(n => n.USER_ID == currentUser.USER_ID).FirstOrDefault();
                user.SUBSCRIPTION = sub.SUBSCRIPTION_ID;
                db.SaveChangesAsync();
            }
            else if ((DateTime.Now.Month - a.SUBSCRIPTION_DATE.Month) + 12 * (DateTime.Now.Year - a.SUBSCRIPTION_DATE.Year) < a.LENGTH)
            {
                a.LENGTH += 12;
                db.SaveChangesAsync();
            }
            else
            {
                a.LENGTH = 12;
                a.SUBSCRIPTION_DATE = DateTime.Now;
                db.SaveChangesAsync();
            }
        }
    }
}
