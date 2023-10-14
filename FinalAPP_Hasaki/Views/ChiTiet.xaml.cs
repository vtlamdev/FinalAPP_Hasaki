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
using Syncfusion.SfNumericUpDown.XForms;

namespace FinalAPP_Hasaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChiTiet : ContentPage
    {
        int masp;
        int value = 1;
        public ChiTiet(Product product)
        {
            InitializeComponent();
            masp = product.MASP;
            GetDetailsProduct(product.MASP);
        }
        async void GetDetailsProduct(int product_id)
        {
            HttpClient httpClient = new HttpClient();
            var product_details = await httpClient.GetStringAsync(IPaddress.url + "GetDetailSP?masp=" + product_id.ToString());
            var product_details_Converted = JsonConvert.DeserializeObject<List<Product>>(product_details);
            Product_details.ItemsSource= product_details_Converted;
        }
        async private void Button_Clicked(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            if(currentNguoiDung.MAKH == 0)
            {
                await DisplayAlert("TB", "Vui lòng đăng nhập!", "OK");
            }
            else
            {               
                await http.GetStringAsync(IPaddress.url + "ThemGioHang?MAKH=" + currentNguoiDung.MAKH.ToString() + "&MASP=" + masp.ToString() + "&soluong=" + value.ToString());
                await DisplayAlert("Thông báo", "Đã thêm sản phẩm vào giỏ hàng!", "OK");
            }                 
        }

        private void sfNumericUpDown_ValueChanged(object sender, ValueEventArgs e)
        {
            value = Convert.ToInt32(e.Value);
        }
    }
}