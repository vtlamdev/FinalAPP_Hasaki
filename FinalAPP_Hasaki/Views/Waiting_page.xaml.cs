using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalAPP_Hasaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Waiting_page : ContentPage
    {
        public Waiting_page()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GoTrangChu();
        }
        private async void GoTrangChu()
        {
            await Task.Delay(4000);
            await Shell.Current.GoToAsync(state: "//TrangChu");
        }
    }
}