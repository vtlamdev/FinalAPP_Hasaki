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
    public partial class DatHang : ContentPage
    {
        public DatHang()
        {
            InitializeComponent();
            HienThiDiaChi();
            HienThiGioHang();
            KhoiTaoPicker();
            XuatTotal();
        }
        async public void XuatTotal()
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "TongTienGH?MAKH=" + currentNguoiDung.MAKH.ToString());
            var subjectlistConverted = JsonConvert.DeserializeObject<List<TongTien_GH>>(subjectlist);
            lstTongTien_GH.ItemsSource = subjectlistConverted;
        }
        async public void HienThiDiaChi()
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "GetInfoKhachHang?MAKH=" + currentNguoiDung.MAKH.ToString());
            var subjectlistConverted = JsonConvert.DeserializeObject<List<ThongTinKhachHang>>(subjectlist);
            lstdiachi.ItemsSource = subjectlistConverted;
        }
        async public void HienThiGioHang()
        {
            HttpClient httpClient = new HttpClient();
            var subjectlist = await httpClient.GetStringAsync(IPaddress.url + "SelectGioHang?MAKH=" + currentNguoiDung.MAKH.ToString());
            var subjectlistConverted = JsonConvert.DeserializeObject<List<classGioHang>>(subjectlist);
            lstttdonhang.ItemsSource = subjectlistConverted;
        }
        private void lstdiachi_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new Page_Set_DiaChi());
        }

        async private void dathang_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            await httpClient.GetStringAsync(IPaddress.url + "DatHang?MAKH=" + currentNguoiDung.MAKH.ToString());
            await DisplayAlert("Thông báo", "Bạn đã đặt hàng thành công", "OK");
            await Shell.Current.GoToAsync(state: "//TrangChu");
        }
        void KhoiTaoPicker()
        {
            string[] PTTT = new string[]
            {"Thanh toán tiền khi nhận hàng (COD)", "Thanh toán trực tuyến VNPAY"};
            picpttt.ItemsSource = PTTT;
        }

        async private void picpttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pic = (Picker)sender;
            int dongchon = pic.SelectedIndex;
            if(dongchon > 0)
            {
                await DisplayAlert("Thông báo", "Bạn đã chọn phương thức giao hàng " + (string)pic.SelectedItem, "OK");
            }
        }
    }
}