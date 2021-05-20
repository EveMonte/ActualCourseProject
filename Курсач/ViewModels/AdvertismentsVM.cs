using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Курсач.Singleton;

namespace Курсач.ViewModels
{
    public class AdvertismentsVM : BaseViewModel
    {
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
                System.Windows.Forms.MessageBox.Show(ex.Message);
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
                System.Windows.Forms.MessageBox.Show(ex.Message);
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
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
