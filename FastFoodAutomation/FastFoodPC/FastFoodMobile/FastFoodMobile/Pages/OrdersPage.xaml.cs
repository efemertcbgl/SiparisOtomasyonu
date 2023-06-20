using FastFoodMobile.Models;
using FastFoodMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Resource;

namespace FastFoodMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public ObservableCollection<OrderByUser> OrdersCollection;
        public OrdersPage()
        {
            InitializeComponent();
            OrdersCollection = new ObservableCollection<OrderByUser>();
            GetOrderItems();

        }
        public async void GetOrderDetail(int orderId)
        {
            var orders = await ApiService.GetOrderDetails(orderId);
            var orderDetails = orders[0].orderDetails;
        }
        private async void GetOrderItems()
        {
           var orders = await ApiService.GetOrdersByUser(Preferences.Get("userId", 0));
           foreach (var order in orders) 
           {
               OrdersCollection.Add(order);
           }
           LvOrders.ItemsSource = OrdersCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void LvOrders_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedOrder = (OrderByUser)e.Item;
            int orderId = selectedOrder.id;
            Navigation.PushModalAsync(new OrderDetailPage(orderId));
            LvOrders.SelectedItem = null;
        }
    }
}