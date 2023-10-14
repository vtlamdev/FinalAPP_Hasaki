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
    public partial class Page_Set_CCCD : ContentPage
    {
        public Page_Set_CCCD()
        {
            InitializeComponent();
            this.Title = "Sửa CCCD";
        }
    
    private async void update_cccd(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync(IPaddress.url + "updateCCCD?cccd=" + Set_cccd.Text + "&makh=" + currentNguoiDung.MAKH.ToString());
            await DisplayAlert("Thông báo", "Cập nhập thành công", "OK");
        }
    }
}