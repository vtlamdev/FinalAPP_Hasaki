using FinalAPP_Hasaki.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalAPP_Hasaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThuongHieu : ContentPage
    {
        public ThuongHieu()
        {
            InitializeComponent();
            hienthihang();
        }
        public ThuongHieu(Hang hang)
        {
            InitializeComponent();
            hienthihang();
            hienthiproduct(hang.MAHANG);
        }
        async void hienthiproduct(int mahang)
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "GetSPByHang?mahang=" + mahang.ToString());
            var subjectlistConverted = JsonConvert.DeserializeObject<List<Product>>(subjectlist);
            Homeproduct.ItemsSource = subjectlistConverted;
        }
        async void hienthihang()
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "GetHang");
            var subjectlistConverted = JsonConvert.DeserializeObject<List<Hang>>(subjectlist);
            Thuonghieu_noibat.ItemsSource = subjectlistConverted;
        }

        private void Thuonghieu_noibat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Hang hang = (Hang)e.CurrentSelection.FirstOrDefault();
            if (hang != null)
            {
                hienthiproduct(hang.MAHANG);
            }
            Thuonghieu_noibat.SelectedItem = null;
        }

        private void Homeproduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = (Product)e.CurrentSelection.FirstOrDefault();
            if (product != null)
            {
                Navigation.PushAsync(new ChiTiet(product));
            }
            Homeproduct.SelectedItem = null;
        }
    }
}