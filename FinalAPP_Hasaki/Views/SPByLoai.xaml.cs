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
	public partial class SPByLoai : ContentPage
	{
        public SPByLoai(int maloai)
        {
            InitializeComponent();
            hienthiproduct(maloai);
        }
        private void Ca_nhan_tapped(object sender, EventArgs e)
        {
            hienthiproduct(6);
        }

        private void Da_mat_tapped(object sender, EventArgs e)
        {
            hienthiproduct(2);
        }

        private void Toc_da_tapped(object sender, EventArgs e)
        {
            hienthiproduct(4);
        }

        private void Co_the_tapped(object sender, EventArgs e)
        {
            hienthiproduct(5);
        }

        private void My_pham_tapped(object sender, EventArgs e)
        {
            hienthiproduct(1);
        }

        private void Nuoc_hoa_tapped(object sender, EventArgs e)
        {
            hienthiproduct(7);
        }

        private void Chuc_nang_tapped(object sender, EventArgs e)
        {
            hienthiproduct(8);
        }

        private void trang_diem_tapped(object sender, EventArgs e)
        {
            hienthiproduct(3);
        }

        private void SpByLoai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = (Product)e.CurrentSelection.FirstOrDefault();
            if (product != null)
            {
                Navigation.PushAsync(new ChiTiet(product));
            }
            SpByLoai.SelectedItem = null;
        }
        async void hienthiproduct(int maloai)
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "GetSPByLoai?maloai=" + maloai);
            var subjectlistConverted = JsonConvert.DeserializeObject<List<Product>>(subjectlist);
            SpByLoai.ItemsSource = subjectlistConverted;
        }
    }
}