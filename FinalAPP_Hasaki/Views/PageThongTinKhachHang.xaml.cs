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
namespace FinalAPP_Hasaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageThongTinKhachHang : ContentPage
    {
        public PageThongTinKhachHang()
        {
            InitializeComponent();
            qlnguoidung(currentNguoiDung.MAKH);
            this.Title = "Cập nhập thông tin tài khoản";
        }
        async void qlnguoidung(int makh)
        {

            HttpClient httpClient = new HttpClient();
            var khachhang = await httpClient.GetStringAsync(IPaddress.url + "GetInfoKhachHang?makh=" + currentNguoiDung.MAKH.ToString());
            var khachhang_Converted = JsonConvert.DeserializeObject<List<ThongTinKhachHang>>(khachhang);
            listview_thongtinkhachhang.ItemsSource = khachhang_Converted;
        }

        private async void button_cap_nhap_thongtin(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            ThongTinKhachHang nd = new ThongTinKhachHang()
            {
            };
            string jsonlh = JsonConvert.SerializeObject(nd);
            StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync(IPaddress.url + "DangKy", httcontent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            nd = JsonConvert.DeserializeObject<ThongTinKhachHang>(kqtv);
            if (nd.DCHI != null)
            {
                await DisplayAlert("Thông báo", "Cập nhập thông tin thành công", "ok");
            }
            else
                await DisplayAlert("Thông báo", "Đã có lỗi xảy ra vui lòng chờ trong giây lát.", "ok");
        }

        private void tapped_page_doiten(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageSetTen());
        }

        private void tapped_page_doidiachi(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page_Set_DiaChi());
        }

        private void tapped_page_cccd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page_Set_CCCD());
        }

        private void tapped_doimatkhau(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DoiMatKhau());
        }

        private void taped_allthongtin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowThongTin());
        }

        private void tapped_thongtin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowThongTin());
        }
    }
}