using PasswordManager.Helpers;
using PasswordManager.Models;
using PasswordManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PasswordManager.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private Credentials cred;
        private string account = "", email = "", username = "", password = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Account {
            get {
                return account;
            }
            set {
                if (account != value) {
                    account = value;
                }
                OnPropertyChanged();
            }
        }
        public string Email {
            get {
                return email;
            }
            set {
                if (email != value) {
                    email = value;
                }
                OnPropertyChanged();
            }
        }
        public string Username {
            get {
                return username;
            }
            set {
                if (username != value) {
                    username = value;
                }
                OnPropertyChanged();
            }
        }
        public string Password {
            get {
                return password;
            }
            set {
                if (password != value) {
                    password = value;
                }
                OnPropertyChanged();
            }
        }

        public DetailsViewModel() { 
        }
        public DetailsViewModel(int id) {
            Id = id;

            cred = DBService.GetCredential(id).Result;

            Account = Encryption.DecryptData(cred.Account, "oiDsLkX21QrDSAkYrQkx");
            Email = Encryption.DecryptData(cred.Email, "oiDsLkX21QrDSAkYrQkx");
            Username = Encryption.DecryptData(cred.Username, "oiDsLkX21QrDSAkYrQkx");
            Password = Encryption.DecryptData(cred.Password, "oiDsLkX21QrDSAkYrQkx");
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
