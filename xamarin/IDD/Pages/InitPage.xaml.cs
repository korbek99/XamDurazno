using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class InitPage : MasterDetailPage
    {
        public InitPage()
        {
            InitializeComponent();

            App._menu = Master.FindByName<MenuPage>("menuPage");
            App.Navigator = Detail.FindByName<NavigationPage>("NavigationHistory");

            menuPage.ListView.ItemSelected += ListView_ItemSelected;



     //       menuPage.BtnFace.Clicked += async (object sender, EventArgs e) =>
     //       {
     //           if(menuPage.BtnFace.Text == "Cerrar Sesión")
     //           {
     //               GeneralSetting.UserFBSettings = string.Empty;
     //               menuPage.IsLoggin(false);
     //           }
     //           else
     //           {
					//IsPresented = false;
					////await DisplayAlert("Facebook", "No publicaremos nada en tu facebook. Solamente utilizaremos tus datos personales para notificar los resultados del concurso.", "Ok");
					//await App.Navigator.PushAsync(new FacebookPage());
     //           }

     //       };
			//menuPage.BtnFace.Tapped += (object sender, EventArgs e) =>
   //         {
				//           if(menuPage.BtnFace.Text == "Cerrar Sesión")
				//           {
				//               GeneralSetting.UserFBSettings = string.Empty;
				//               menuPage.IsLoggin(false);
				//           }
				//           else
				//           {
				//IsPresented = false;
				////await DisplayAlert("Facebook", "No publicaremos nada en tu facebook. Solamente utilizaremos tus datos personales para notificar los resultados del concurso.", "Ok");
				//await App.Navigator.PushAsync(new FacebookPage());
				//           }

			//	App.GoToLogin();
				// await Navigation.PushAsync(new FacebookPage());

			//};
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
#if __IOS__
            IsGestureEnabled = false;
#endif
            LoadMain();
        }

        public async Task LoadMain()
        {
            await Task.Run(() =>
            {
				var isFB = GeneralSetting.GetUser();

                Device.BeginInvokeOnMainThread(() =>
                {
                    menuPage.IsLoggin(isFB);
                });

            });
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Item;
            if (item != null)
            {
                menuPage.ListView.SelectedItem = null;
                IsPresented = false;
                if (item.Title == "Home")
                {
                    var app = Application.Current as App;
                    var mainPage = (InitPage)app.MainPage;
                    App.Navigator = new NavigationPage(new MainPage()) { BarBackgroundColor = (Color)Application.Current.Resources["AzulColor"] };

                    mainPage.Detail = App.Navigator;
                }
                else if (item.Title == "Lugares que visitar")
                {
                    await App.Navigator.PushAsync(new TourPage());
                }
                else await App.Navigator.PushAsync((Page)Activator.CreateInstance(item.TargetType));

            }
        }
    }
}
