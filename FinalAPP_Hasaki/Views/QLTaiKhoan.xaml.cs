using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FinalAPP_Hasaki.Models;
using System.Net.Http;
using Newtonsoft.Json;
namespace FinalAPP_Hasaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QLTaiKhoan : ContentPage
    {
        public QLTaiKhoan()
        {         
                InitializeComponent();
                qlnguoidung(currentNguoiDung.MAKH);
        }
        async void qlnguoidung(int makh)
        {
            if(makh==0)
            {
                Application.Current.MainPage = new MainPage();
                await Shell.Current.GoToAsync(state: "//DangNhap");

            }
            else
            {
                HttpClient httpClient = new HttpClient();
                var khachhang = await httpClient.GetStringAsync(IPaddress.url + "GetInfoKhachHang?makh=" + currentNguoiDung.MAKH.ToString());
                var khachhang_Converted = JsonConvert.DeserializeObject<List<ThongTinKhachHang>>(khachhang);
                QLtaikhoan_listview.ItemsSource = khachhang_Converted;
            }    
           
        }
        private void click_to_donhang(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonHang(10));
        }

        private void tapped_dangcho(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonHang(2));
        }

        private void tapped_thanhcong(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonHang(1));
        }

        private void tapped_dahuy(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonHang(-1));
        }

        private void tapped_dangxuly(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonHang(0));
        }

        private void tapped_tatca(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DonHang(1));
        }

        private void tapped_thongtintaikhoan(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageThongTinKhachHang());
           
        }

        private void tapped_capnhapdiachi(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page_Set_DiaChi());
        }

        private async void tapped_dangxuat(object sender, EventArgs e)
        {
            currentNguoiDung.MAKH = 0;
            Application.Current.MainPage = new MainPage();
            await Shell.Current.GoToAsync(state: "//DangNhap");

        }
    }
}