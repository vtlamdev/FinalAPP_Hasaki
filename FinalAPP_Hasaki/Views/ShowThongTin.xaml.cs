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
    public partial class ShowThongTin : ContentPage
    {
        public ShowThongTin()
        {
            InitializeComponent();
            show();
            this.Title = "Thông tin cá nhân";
        }
        async void show()
        {
            HttpClient httpClient = new HttpClient();
            var khachhang = await httpClient.GetStringAsync(IPaddress.url + "GetInfoKhachHang?makh=" + currentNguoiDung.MAKH.ToString());
            var khachhang_Converted = JsonConvert.DeserializeObject<List<ThongTinKhachHang>>(khachhang);
            showthongtin.ItemsSource = khachhang_Converted;
        }
    }
}