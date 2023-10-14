using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalAPP_Hasaki.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FinalAPP_Hasaki.Models;
namespace FinalAPP_Hasaki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderContentView : ContentView
    {
        public string sdt { get; set; } = "Chào " + currentNguoiDung.SDT;
        public HeaderContentView()
        {           
            InitializeComponent();
            HiddenLabel();
            BindingContext = this;           
        }      
        public void HiddenLabel()
        {
            if(currentNguoiDung.MAKH != 0)
            {
                lbdangnhap.IsVisible = false;
                lbdangky.IsVisible = false;
                lbsdt.IsVisible = true;
            }
        }      
        private void Link_dang_nhap(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DangNhap());
            Shell.Current.FlyoutIsPresented = false;
        }
        private void Link_dang_ky(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DangKy());
            Shell.Current.FlyoutIsPresented = false;
        }
        private void Link_tai_khoan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new  QLTaiKhoan());
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}