using FastFoodMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            if(!EntPassword.Text.Equals(EntConfirmPassword.Text))
            {
                await DisplayAlert("Şifre Uyuşmuyor", "Lütfen Şifreyi Doğru Yazdığınızdan Emin Olun.", "Tamam");
            }
            else
            {
                var response = await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
                if (response)
                {
                    await DisplayAlert("Başarılı", "Hesabınız Başarıyla Oluşturuldu", "Tamam");
                    await Navigation.PushModalAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("Hata", "Hesabınız Oluşturulurken Bir Sorun Oluştu", "Tamam");
                }
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}