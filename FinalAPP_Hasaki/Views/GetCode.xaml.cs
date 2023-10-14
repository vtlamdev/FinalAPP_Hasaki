using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalAPP_Hasaki.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.Net.Mail;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;
namespace FinalAPP_Hasaki.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetCode : ContentPage
    {
        string randomCode;
        public static string to;
        public static int getmakh;
        public GetCode()
        {
            InitializeComponent();
            this.Title = "Quên mật khẩu";
        }

        private async void click_getcode(object sender, EventArgs e)
        {
            HttpClient http = new HttpClient();
            Forgot_Password fg = new Forgot_Password { gmail = Email_entry.Text };
            string jsonlh = JsonConvert.SerializeObject(fg);
            StringContent httcontent = new StringContent(jsonlh, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await http.PostAsync(IPaddress.url + "ForgotPassword", httcontent);
            var kqtv = await kq.Content.ReadAsStringAsync();
            fg = JsonConvert.DeserializeObject<Forgot_Password>(kqtv);
            await DisplayAlert("Thông báo", fg.makh.ToString(), "ok");
            if (fg.makh>0)
            {
                getmakh = fg.makh;
                string from, pass, messageBody;
                Random rand = new Random();
                randomCode = (rand.Next(999999)).ToString();
                MailMessage message = new MailMessage();
                to = (Email_entry.Text).ToString();
                from = "uitcarshop@gmail.com";
                pass = "joucvfymmvauhjxm";
                messageBody = "Mã code để thay đổi mật khẩu của bạn là: " + randomCode;
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Body = messageBody;
                message.Subject = "Code thay đổi mật khẩu";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);
                try
                {
                    smtp.Send(message);
                    await DisplayAlert("Thông báo", "Vui lòng kiểm tra Email", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Thông báo", ex.Message, "OK");
                }
            }    
            else
            {
                await DisplayAlert("Thông báo", "Email Không tồn tại vui lòng thử lại sau" , "OK");
            }      
        }

        private  async void click_check_code(object sender, EventArgs e)
        {
            if(randomCode== Code_entry.Text.ToString())
            {
                to = Email_entry.Text;
                Navigation.PushAsync(new QuenMatKhau(getmakh));
            }    
            else
            {
                await DisplayAlert("Thông báo", "Mã code không đúng", "OK");
            }    
        }
    }
}