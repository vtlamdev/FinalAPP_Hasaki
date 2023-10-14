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
    public partial class TrangChu : ContentPage
    {
        public TrangChu()
        {
            InitializeComponent();
            hienthiproduct();
            hienthihang();
        }

        private void TIKTDH_Clicked(object sender, EventArgs e)
        {
            if (currentNguoiDung.MAKH == 0)
            {
                Navigation.PushAsync(new DangNhap());
            }
            else
            {
                Navigation.PushAsync(new DonHang(1));
            }
        }
        private void TIgiohang_Clicked(object sender, EventArgs e)
        {

            if (currentNguoiDung.MAKH == 0)
            {
                Navigation.PushAsync(new DangNhap());
            }
            else
            {
                Navigation.PushAsync(new GioHang());
            }

        }
        async void hienthihang()
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "GetHang");
            var subjectlistConverted = JsonConvert.DeserializeObject<List<Hang>>(subjectlist);
            Thuonghieu_noibat.ItemsSource = subjectlistConverted;     
        }

        async void hienthiproduct()
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "GetSPByLoai?maloai=1");
            var subjectlistConverted = JsonConvert.DeserializeObject<List<Product>>(subjectlist);
            Homeproduct1.ItemsSource = subjectlistConverted;
            var subjectlist2 = await httpClient.GetStringAsync(IPaddress.url + "GetSPByLoai?maloai=2");
            var subjectlistConverted2 = JsonConvert.DeserializeObject<List<Product>>(subjectlist2);
            Homeproduct2.ItemsSource = subjectlistConverted2;
        }

        private void Ca_nhan_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SPByLoai(6));
        }

        private void Da_mat_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SPByLoai(2));
        }

        private void Toc_da_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SPByLoai(4));
        }

        private void Co_the_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SPByLoai(5));
        }

        private void My_pham_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SPByLoai(1));
        }

        private void Nuoc_hoa_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SPByLoai(7));
        }

        private void Chuc_nang_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SPByLoai(8));
        }

        private void trang_diem_tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SPByLoai(3));
        }

        private void Homeproduct1_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            Product product = (Product)e.CurrentSelection.FirstOrDefault();
            if(product!=null)
            {
                Navigation.PushAsync(new ChiTiet(product));
            }         
            Homeproduct1.SelectedItem = null;
        }

        private void Homeproduct2_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            Product product = (Product)e.CurrentSelection.FirstOrDefault();
            if (product != null)
            {
                Navigation.PushAsync(new ChiTiet(product));
            }
            Homeproduct2.SelectedItem = null;
        }

        private void Thuonghieu_noibat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Hang hang = (Hang)e.CurrentSelection.FirstOrDefault();
            if (hang != null)
            {
                Navigation.PushAsync(new ThuongHieu(hang));
            }
            Thuonghieu_noibat.SelectedItem = null;
        }

        private void TimKiem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());
        }
    }
}