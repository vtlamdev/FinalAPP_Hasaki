using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalAPP_Hasaki.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;
namespace FinalAPP_Hasaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DangNhap : ContentPage
    {
        public DangNhap()
        {
            InitializeComponent();
            this.Title = "Đăng Nhập";
        }
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
        }
        private async void Dang_nhap_clicked(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync(IPaddress.url + "DangNhap?SODIENTHOAI=" + Entry_Sodienthoai.Text);
            var nd = JsonConvert.DeserializeObject<NguoiDung>(kq);
            if(nd.MAKH>0)
            {
                bool isPasswordMatched = VerifyPassword(Entry_Matkhau.Text, nd.MATKHAUHASH, nd.MATKHAUSALT);                
                if (isPasswordMatched == true)
                {
                    currentNguoiDung.MAKH = nd.MAKH;
                    currentNguoiDung.MATKHAUHASH = nd.MATKHAUHASH;
                    currentNguoiDung.MATKHAUSALT = nd.MATKHAUSALT;
                    currentNguoiDung.SDT = nd.SODIENTHOAI;
                    await DisplayAlert("Thông báo", "Đăng nhập thành công", "OK");
                    Application.Current.MainPage = new MainPage();
                    await Shell.Current.GoToAsync(state: "//TrangChu");
                }
                else
                {
                    await DisplayAlert("Thông báo", "Tên đăng nhập hoặt mật khẩu không chính xác", "OK");
                }
            }               
            else
            {
                await DisplayAlert("Thông báo", "Tên đăng nhập hoặt mật khẩu không chính xác", "OK");
            }    
               

        }

        private void Dang_Ky_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DangKy());
        }

        private void tapped_getcode(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GetCode());
        }
    }
}