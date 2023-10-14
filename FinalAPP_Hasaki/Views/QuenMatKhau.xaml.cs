using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalAPP_Hasaki.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;
namespace FinalAPP_Hasaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuenMatKhau : ContentPage
    {     
        public static int getmakh;
        public QuenMatKhau()
        {
            InitializeComponent();
            this.Title = "Quên mật khẩu";
        }
        public QuenMatKhau(int makh)
        {
            InitializeComponent();
            getmakh = makh;
        }
        public static HashSalt GenerateSaltedHash(int size, string password)
        {
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            HashSalt hashSalt = new HashSalt { Hash = hashPassword, Salt = salt };
            return hashSalt;
        }
        private async void click_quenmatkhau(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            if (entry_matkhaumoi.Text == entry_matkhaumoi_.Text)
            {
                HashSalt hashSalt = GenerateSaltedHash(64, entry_matkhaumoi.Text);
                NguoiDung nd = new NguoiDung { MAKH = getmakh, MATKHAUHASH = hashSalt.Hash, MATKHAUSALT = hashSalt.Salt };
                string jsonlh = JsonConvert.SerializeObject(nd);
                StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
                HttpResponseMessage kq = await http.PostAsync(IPaddress.url + "DoiMatKhau", httcontent);
                var kqtv = await kq.Content.ReadAsStringAsync();
                nd = JsonConvert.DeserializeObject<NguoiDung>(kqtv);
                if (nd.MAKH > 0)
                {
                    await Shell.Current.GoToAsync(state: "//DangNhap");
                }
                else
                    await DisplayAlert("Thông báo", "Đã có lỗi xảy ra vui lòng thử lại sau", "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Mật khẩu mới không khớp", "OK");

            }
        }
    }
}