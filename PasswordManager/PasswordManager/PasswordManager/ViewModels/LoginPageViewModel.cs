using Xamarin.Essentials;
using PasswordManager.Models;
using PasswordManager.Views;
using System.Runtime.CompilerServices;
using System.ComponentModel;
//using PasswordManager.Services;
using Command = Xamarin.Forms.Command;
using Xamarin.Forms;

namespace PasswordManager.ViewModels {
    public class LoginPageViewModel {
        public Command ReturnCommand => new Command(() => Authenticate());
        public event PropertyChangedEventHandler PropertyChanged;

        private string text;
        public string Text {
            get { return text; }
            set {
                text = value;
                OnPropertyChanged();
            }
        }

        public void Authenticate() {
            if (text.Equals("nelsonmandela")) {
                App.Current.MainPage =new HomePage();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
