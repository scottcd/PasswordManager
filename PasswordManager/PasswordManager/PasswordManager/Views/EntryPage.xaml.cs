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
    public partial class EntryPage : Rg.Plugins.Popup.Pages.PopupPage {
        public EntryPage() {
            InitializeComponent();
            BindingContext = new EntryPageViewModel();
        }
        public EntryPage(int id) {
            InitializeComponent();
            BindingContext = new EntryPageViewModel(id);
        }
    }
}