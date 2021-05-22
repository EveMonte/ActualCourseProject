using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class AdvertismentsVM : BaseViewModel
    {
        Notifier notifier;
        private ObservableCollection<ADVERTISEMENT> ads;

        public ObservableCollection<ADVERTISEMENT> Ads
        {
            get { return ads; }
            set 
            { 
                ads = value;
                OnPropertyChanged("Ads");
            }
        }

        public ICommand OpenFileDialogCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand AddAdvCommand { get; private set; }
        public ICommand GetAdsCommand { get; private set; }
        public ICommand SetAdsCommand { get; private set; }

        public AdvertismentsVM(ObservableCollection<ADVERTISEMENT> Ads)
        {
            this.Ads = Ads;
            OpenFileDialogCommand = new DelegateCommand(ChangeAdv);
            RemoveCommand = new DelegateCommand(RemoveAdv);
            AddAdvCommand = new DelegateCommand(AddAdv);
            GetAdsCommand = new DelegateCommand(GetAds);
            SetAdsCommand = new DelegateCommand(SetAds);

            Workframe thisWin = null;
            foreach (Window win in Application.Current.Windows)
            {
                if (win is Workframe)
                {
                    thisWin = win as Workframe;
                }
            }

            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        private void SetAds(object obj)
        {
            try
            {
                List<ADVERTISEMENT> listToDelete = new List<ADVERTISEMENT>();
                foreach (ADVERTISEMENT adv in Ads)
                {
                    var newAdv = App.db.ADVERTISEMENT.FirstOrDefault(n => n.ADV_ID == adv.ADV_ID);
                    if (newAdv == null)
                        App.db.ADVERTISEMENT.Add(adv);
                    else
                    {
                        newAdv.IMAGE_SOURCE = adv.IMAGE_SOURCE;
                        newAdv.LINK = adv.LINK;
                    }
                }
                foreach (ADVERTISEMENT adv in App.db.ADVERTISEMENT)
                {
                    if (Ads.FirstOrDefault(n => n.ADV_ID == adv.ADV_ID) == null)
                    {
                        listToDelete.Add(adv);
                    }
                }
                App.db.ADVERTISEMENT.RemoveRange(listToDelete);
                listToDelete = null;
                App.db.SaveChangesAsync().GetAwaiter();
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }

        private void GetAds(object obj)
        {
            Ads = new ObservableCollection<ADVERTISEMENT>(App.db.ADVERTISEMENT);
            AdminWindowSingleTone.GetInstance().AdminVM.Ads = Ads;
        }

        private void AddAdv(object obj)
        {
            Ads.Add(new ADVERTISEMENT());
        }

        private void RemoveAdv(object obj)
        {
            try
            {
                ADVERTISEMENT advToDelete = Ads.FirstOrDefault(n => n.ADV_ID == (int)obj);
                Ads.Remove(advToDelete);
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }

        private void ChangeAdv(object obj)
        {
            try
            {
                ADVERTISEMENT currentAds = Ads.FirstOrDefault(n => n.ADV_ID == (int)obj);
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = @"C:\Users\User\Desktop\Курсааааач\Media";
                if (openFileDialog.ShowDialog() == true)
                    currentAds.IMAGE_SOURCE = Path.GetFullPath(openFileDialog.FileName);
                AdminWindowSingleTone.GetInstance().AdminVM.CurrentPageViewModel = new AdvertismentsVM(Ads);
            }
            catch(Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }
    }
}
