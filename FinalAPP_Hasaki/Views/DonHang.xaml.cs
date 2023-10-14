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
    public partial class DonHang : ContentPage
    {
        public DonHang()
        {
            InitializeComponent();
        }
        public DonHang(int a)
        {
            InitializeComponent();
            if(a==10)
            {
                hienthitatca();
            }   
            else
            {
                hienthidonhang(a);
            }    
     

        }

        private void tapped_tatca(object sender, EventArgs e)
        {
            hienthitatca();
        }

        private void tapped_cho_thanh_toan(object sender, EventArgs e)
        {
            hienthidonhang(2);
        }

        private void tapped_dang_xu_ly(object sender, EventArgs e)
        {
            hienthidonhang(0);

        }

        private void tapped_thanh_cong(object sender, EventArgs e)
        {
            hienthidonhang(1);
        }

        private void tapped_da_huy(object sender, EventArgs e)
        {
            hienthidonhang(-1);
        }
        async void hienthidonhang(int trangthai)
        {
            if(currentNguoiDung.MAKH==0)
            {
                Application.Current.MainPage = new MainPage();
                await Shell.Current.GoToAsync(state: "//DangNhap");

            }
            else
            {
                HttpClient httpClient = new HttpClient();
                //192.168.1.13
                var product_info_trangthai = await httpClient.GetStringAsync(IPaddress.url + "Show_Trangthai_Info?trangthai=" + trangthai.ToString() + "&makh=" + currentNguoiDung.MAKH.ToString());
                var product_info_trangthai_Converted = JsonConvert.DeserializeObject<List<Info_trangthai>>(product_info_trangthai);
                info_trangthai.ItemsSource = product_info_trangthai_Converted;
            }    
            
        }
        async void hienthitatca()
        {
            if (currentNguoiDung.MAKH == 0)
            {
                Application.Current.MainPage = new MainPage();
                await Shell.Current.GoToAsync(state: "//DangNhap");

            }
            else
            {
                HttpClient httpClient = new HttpClient();
                //192.168.1.13
                var product_info_trangthai = await httpClient.GetStringAsync(IPaddress.url + "Show_Trangthai_TatCa?makh=" + currentNguoiDung.MAKH.ToString());
                var product_info_trangthai_Converted = JsonConvert.DeserializeObject<List<Info_trangthai>>(product_info_trangthai);
                info_trangthai.ItemsSource = product_info_trangthai_Converted;
            }
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            MenuItem menuitem = (MenuItem)sender;
            Info_trangthai info= (Info_trangthai)menuitem.CommandParameter;
            if(info.TRANGTHAI==2)
            {
                HttpClient httpClient = new HttpClient();
                //192.168.1.13
                var product_info_trangthai = await httpClient.GetStringAsync(IPaddress.url + "HuyHoaDon?sohd=" + info.SOHD + "&makh=" + currentNguoiDung.MAKH.ToString());
                await DisplayAlert("Thông báo", "Bạn đã xóa hóa đơn hóa đơn: " + info.SOHD, "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Không thể hủy hóa đơn do đang giao", "OK");
            }
            
      
        }
    }
}