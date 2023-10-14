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
    public partial class DangKy : ContentPage
    {
        public DangKy()
        {
            InitializeComponent();
			this.Title = "Đăng Ký";
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

		private async void dangky_buttonclick(object sender, EventArgs e)
        {
			HttpClient http = new HttpClient();
			if (Matkhau_entry.Text != Matkhau_confirm_entry.Text)
			{
				await DisplayAlert("Thông báo", "Mật khẩu nhập lại không đúng", "OK");
				return;
			}
			else if(Email_entry.Text==null || Matkhau_entry.Text==null || SDT_entry.Text==null)
            {
				await DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ", "OK");
				return;
			}
			HashSalt hashSalt = GenerateSaltedHash(64, Matkhau_entry.Text);
			NguoiDung nd = new NguoiDung {MATKHAUHASH = hashSalt.Hash,MATKHAUSALT=hashSalt.Salt, SODIENTHOAI = SDT_entry.Text, EMAIL = Email_entry.Text };
			string jsonlh = JsonConvert.SerializeObject(nd);
			StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
			HttpResponseMessage kq = await http.PostAsync(IPaddress.url + "DangKy", httcontent);
			var kqtv = await kq.Content.ReadAsStringAsync();
			nd = JsonConvert.DeserializeObject<NguoiDung>(kqtv);
			if (nd.MAKH > 0)
            {
				await DisplayAlert("Thông báo", "Tạo tài khoản thành công. ", "OK");
                await Shell.Current.GoToAsync(state: "//DangNhap");
            }				
			else
				await DisplayAlert("Thông báo", "Số điện thoại " +nd.SODIENTHOAI +" Đã tồn tại.", "OK");

		}

        private void Dang_Nhap_Clicked(object sender, EventArgs e)
        {
			Navigation.PushAsync(new DangNhap());
        }
    }
}