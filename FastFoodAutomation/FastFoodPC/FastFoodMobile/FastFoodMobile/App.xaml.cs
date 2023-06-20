using FastFoodMobile.Pages;
using Xamarin.Essentials;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var accessToken = Preferences.Get("accessToken", string.Empty);
            if (string.IsNullOrEmpty(accessToken))
            {
                MainPage = new NavigationPage(new SignupPage());
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
