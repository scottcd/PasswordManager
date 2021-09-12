using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasswordManager.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();

            /*
                if (password !exist)
                    text = new user msg  
                    new user input
                    save hash
                else
                    text = welcome back
                    compare hash
             */
        }
    }
}