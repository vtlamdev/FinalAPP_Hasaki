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
    public partial class Search : ContentPage
    {
        public Search()
        {
            InitializeComponent();
            this.Title = "Tìm kiếm";
        }

        private async void clicked_timkiem(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "Search?keyword=" + entry_search.Text);
            var subjectlistConverted = JsonConvert.DeserializeObject<List<Product>>(subjectlist);
            search.ItemsSource = subjectlistConverted;
        }

        private void search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = (Product)e.CurrentSelection.FirstOrDefault();
            if (product != null)
            {
                Navigation.PushAsync(new ChiTiet(product));
            }
            search.SelectedItem = null;
        }
    }
}