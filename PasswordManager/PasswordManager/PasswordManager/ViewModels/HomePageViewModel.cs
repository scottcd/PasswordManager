using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using PasswordManager.Models;
using PasswordManager.Views;
using System.Runtime.CompilerServices;
using System.ComponentModel;
//using PasswordManager.Services;
using Command = Xamarin.Forms.Command;
using Rg.Plugins.Popup.Services;
using PasswordManager.Services;
using PasswordManager.Helpers;

namespace PasswordManager.ViewModels {
    public class HomePageViewModel : INotifyPropertyChanged {
        public Command SearchCommand => new Command(() =>  SearchList());
        public Command AddCommand => new Command(async () => await AddList());
        public Command EditCommand => new Command<int>(async (x) => await EditList(x));
        public Command RemoveCommand => new Command<int>(async (x) => await RemoveList(x));
        public ObservableCollection<Credentials> CredentialsList { get; set; }

        private string searchTerm;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isRefreshing;
        public bool IsRefreshing {
            get { return isRefreshing; }
            set {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public string SearchTerm {
            get { return searchTerm; }
            set {
                searchTerm = value;
                OnPropertyChanged();
            }
        }
        public INavigation Navigation { get; set; }
        public HomePageViewModel() {
            IsRefreshing = true;
            CredentialsList = new ObservableCollection<Credentials>();
            Task v = GetList();

            MessagingCenter.Subscribe<object>(this, "Refresh",  async (sender) => { 
                System.Diagnostics.Debug.WriteLine($"\n\nAdded one to the DB!\n\n");
                await GetList();
            });
            
            isRefreshing = false;
            IsRefreshing = false;
        }

        public async Task GetList() {
            IsRefreshing = true;
            await Task.Delay(1000);
            CredentialsList.Clear();
            
            var creds = await DBService.GetCredentials();
            
            foreach (var item in creds) {
                item.Account = Encryption.DecryptData(item.Account, "oiDsLkX21QrDSAkYrQkx");
                item.Email = Encryption.DecryptData(item.Email, "oiDsLkX21QrDSAkYrQkx");
                item.Username = Encryption.DecryptData(item.Username, "oiDsLkX21QrDSAkYrQkx");
                item.Password = Encryption.DecryptData(item.Password, "oiDsLkX21QrDSAkYrQkx");

                CredentialsList.Add(item);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CredentialsList)));
            isRefreshing = false;
            IsRefreshing = false;
        }

        public void SearchList() {
            var suggestions = CredentialsList.Where(c => c.Account.Contains(SearchTerm));
            CredentialsList = new ObservableCollection<Credentials>(suggestions);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CredentialsList)));
        }

        public async Task AddList() {
            await PopupNavigation.Instance.PushAsync(new EntryPage());
            System.Diagnostics.Debug.WriteLine("\n\nAdd Button Pressed\n\n");
        }
        public async Task EditList(int id) {
            await PopupNavigation.Instance.PushAsync(new EntryPage(id));
        }
        public async Task RemoveList(int id) {
            await DBService.RemoveCredential(id);
            await GetList();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
