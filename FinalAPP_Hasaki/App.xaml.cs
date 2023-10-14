using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FinalAPP_Hasaki.Models;
namespace FinalAPP_Hasaki
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if(currentNguoiDung.MAKH<0)
            {
                MainPage = new MainPage();
            }    
            else
            {
                MainPage = new MainPage();
            }    
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
