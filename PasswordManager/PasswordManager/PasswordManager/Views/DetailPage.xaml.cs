using PasswordManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasswordManager.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : Rg.Plugins.Popup.Pages.PopupPage {
        public DetailPage() {
            InitializeComponent();
            
        }
        public DetailPage(int id) {
            InitializeComponent();
            BindingContext = new DetailsViewModel(id);
        }
    }
}