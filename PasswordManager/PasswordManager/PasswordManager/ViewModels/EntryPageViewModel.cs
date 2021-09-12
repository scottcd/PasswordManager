using PasswordManager.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PasswordManager.Models;
using PasswordManager.Helpers;

namespace PasswordManager.ViewModels {
    public class EntryPageViewModel {
        private int Id = -1;
        public string PlaceHolderAccount { get; set; }
        public string PlaceHolderEmail { get; set; }
        public string PlaceHolderUsername { get; set; }
        public string PlaceHolderPassword { get; set; }
        public string TextAccount { get; set; } = "";
        public string TextEmail{ get; set; } = "";
        public string TextUsername { get; set; } = "";
        public string TextPassword { get; set; } = "";

        public EntryPageViewModel() {
            PlaceHolderAccount = "Account";
            PlaceHolderEmail = "Email";
            PlaceHolderUsername = "Username";
            PlaceHolderPassword = "Password";
        }
        public EntryPageViewModel(int id) {
            Id = id;
            //System.Diagnostics.Debug.WriteLine($"\n\n {id} \n\n");
            var t = DBService.GetCredential(id);
            var cred = t.Result;

            var acc = Encryption.DecryptData(cred.Account, "oiDsLkX21QrDSAkYrQkx");
            var email = Encryption.DecryptData(cred.Email, "oiDsLkX21QrDSAkYrQkx");
            var user = Encryption.DecryptData(cred.Username, "oiDsLkX21QrDSAkYrQkx");
            var pw = Encryption.DecryptData(cred.Password, "oiDsLkX21QrDSAkYrQkx");


            TextAccount = $"{acc}";
            TextEmail = $"{email}";
            TextUsername = $"{user}";
            TextPassword = $"{pw}";
        }

        public Command EditCommand => new Command(async () => await EditList());

        public async Task EditList() {


            if (Id==-1) {
                var acc = Encryption.EncryptData(TextAccount, "oiDsLkX21QrDSAkYrQkx");
                var email = Encryption.EncryptData(TextEmail, "oiDsLkX21QrDSAkYrQkx");
                var user = Encryption.EncryptData(TextUsername, "oiDsLkX21QrDSAkYrQkx");
                var pw = Encryption.EncryptData(TextPassword, "oiDsLkX21QrDSAkYrQkx");

                Task t = DBService.AddCredential(acc, email, user, pw);
                
                MessagingCenter.Send<object>(this, "Refresh");
            }
            else {
                var acc = Encryption.EncryptData(TextAccount, "oiDsLkX21QrDSAkYrQkx");
                var email = Encryption.EncryptData(TextEmail, "oiDsLkX21QrDSAkYrQkx");
                var user = Encryption.EncryptData(TextUsername, "oiDsLkX21QrDSAkYrQkx");
                var pw = Encryption.EncryptData(TextPassword, "oiDsLkX21QrDSAkYrQkx");

                Task t = DBService.UpdateCredential(Id, acc, email, user, pw);
                
                MessagingCenter.Send<object>(this, "Refresh");
            }
            
            await PopupNavigation.Instance.PopAsync();
        }

    }
}
