using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PasswordManager.ViewModels;
using PasswordManager.Models;
using Rg.Plugins.Popup.Services;

namespace PasswordManager.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage {
        HomePageViewModel container;
        public HomePage() {
            InitializeComponent();
            container = BindingContext as HomePageViewModel;

            BindingContext = container;
        }

        private void HandleSearch(object sender, TextChangedEventArgs e) {
            MyListView.BeginRefresh();
            
            if (string.IsNullOrWhiteSpace(e.NewTextValue)) {
                MyListView.ItemsSource = container.CredentialsList;
            }
            else {
                MyListView.ItemsSource = container.CredentialsList.Where(c =>   c.Account.ToLower().Contains(e.NewTextValue.ToLower()) || 
                                                                                c.Username.ToLower().Contains(e.NewTextValue.ToLower()) ||
                                                                                c.Email.ToLower().Contains(e.NewTextValue.ToLower()));
            }

            MyListView.EndRefresh();
        }

        private async void ItemDetails(object sender, SelectedItemChangedEventArgs e) {
            if (((ListView)sender).SelectedItem != null) {
                Credentials cred = (Credentials)e.SelectedItem;
                await PopupNavigation.Instance.PushAsync(new DetailPage(cred.Id));
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}