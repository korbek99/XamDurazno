using System;
using System.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xamarin.Forms;

namespace IDD
{
	public partial class App : Application
	{
		public static MenuPage _menu { get; internal set; }
		//public static FestivalJson json { get; internal set; }
		public static UserFB userFB { get; internal set; }
		public static Configuracion configuracion { get; internal set; }
		public static Version Version { get; internal set; }
		public static string CurrentVersion;
		public static bool IsVersionMsg { get; internal set; }
		public static bool IsDatos { get; internal set; }

		public static double ScreenWidth;
		public static double ScreenHeight;

		public static NavigationPage Navigator { get; internal set; }

		public static Action HideLoginView
		{
			get
			{
				return new Action(() => Navigator.PopAsync());
			}
		}

		public App()
		{
			InitializeComponent();


			//TODO: borrar en deploy /solo para hacer pruebas
			//GeneralSetting.FirstTime = String.Empty;

			var r = GeneralSetting.FirstTime;


			GetConfig();


			//var email = "jmancilla.bild@outlook.com";

			//var idd_token =  await DependencyService.Get<IPushNotification>().RegisterDevice(email);


			//Console.WriteLine("token_idd_registrado__" + idd_token +"----");


			// RegistrarPushToken();

			if (CheckCambiarContrasena())
			{
				
				ResetPasswordInfo data = DependencyService.Get<IResetPassword>().GetDataFromUriCall();
                GoToCambiarContrasena(data);
			}
			else { 
				if(string.IsNullOrEmpty(r))
	            {
	                MainPage = new NavigationPage(new RegistroPage()) { BarBackgroundColor = (Color)App.Current.Resources["AzulColor"] };
					GeneralSetting.FirstTime = "ok";
	            }
	            else
	            {
	                MainPage = new InitPage();
	            }
			}



            //CurrentVersion = Context.PackageManager.GetPackageInfo(Context.PackageName, 0).VersionName;
			//MainPage = new DatosPage();
		}


		//public  void RegistrarPushToken() { 

		//	var email = "jmancilla.bild@outlook.com";

		//	 DependencyService.Get<IPushNotification>().RegisterDevice(email);


		//	Console.WriteLine("token_idd_registrado_---");

		//}


		public async void GetConfig() { 
		
				App.configuracion = await ConfiguracionService.GetConfiguracion();

		}

        public static async void LoginFacebook(string obj, string accesS_token, DateTime expires_in) {
			UserFB usr = new UserFB();
			usr.public_profile = obj;
			usr.token = accesS_token;
			usr.expireDate = expires_in;
			var dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(obj);


			if(dyn["email"] != null){
				usr.email = dyn["email"].ToString() ?? "";
			}else {
				usr.email = "";
			}

            usr.first_name = dyn["first_name"].ToString() ?? "";
			usr.last_name = dyn["last_name"].ToString() ?? "";
			usr.full_name = usr.first_name + " " + usr.last_name; 
			usr.gender = dyn["gender"].ToString() ?? "";
            usr.id_fbk = dyn["id"].ToString();
			usr.url = "https://graph.facebook.com/"+usr.id_fbk+"/picture?type=large";
			userFB = usr;

            var r = JsonConvert.SerializeObject(usr);

			//attemp login if not exists fb on db then create a new account in db idd
			await App.LoginFB(usr);

		}


		public static async Task LoginFB(UserFB usr)
		{
			Console.WriteLine("entra a registrar");

			await Task.Run(async () =>
			{

				try
				{

					var responseApi = await FbService.PostLogin(usr.id_fbk);


					 Device.BeginInvokeOnMainThread(async () =>
					{
						if (responseApi != null)
						{

							if (responseApi["codigo"].ToString() == "1")
							{
								var s = responseApi["data"].ToString();
								var userLogged = Newtonsoft.Json.JsonConvert.DeserializeObject<UserFB>(s);

								if (userLogged != null)
								{
									GeneralSetting.UserSettings = JsonConvert.SerializeObject(userLogged);
									App.NavigateToProfile();
								}//usuario nuevo registrar pagina
								else {
									var app = Current as App;
									app.MainPage = new NavigationPage(new RegistroPage(usr)) { BarBackgroundColor = (Color)App.Current.Resources["AzulColor"] };
								}
							}
							else
							{
								var app = Current as App;
								app.MainPage = new NavigationPage(new RegistroPage(usr)) { BarBackgroundColor = (Color)App.Current.Resources["AzulColor"] };

							}

						}

					});

				}
				catch (Exception e)
				{
					Console.WriteLine("ha ocurrido un error");

				}

			});

		}

		public static void NavigateToProfile()
		{
			var app = Current as App;
            app.MainPage = new InitPage();
			//var mainPage = (InitPage)app.MainPage;

			//mainPage.Detail = Navigator;
		}

        public static void BackToMain(bool isFb)
		{
			var app = Current as App;

			app.MainPage = new InitPage();
            _menu.IsLoggin(isFb);
		}

		public static void GoToLogin()
		{
			var app = Current as App;
			app.MainPage = new NavigationPage(new LoginPage()) { BarBackgroundColor = (Color)App.Current.Resources["AzulColor"] };
		}

		public static void GoToMisReclamos()
		{
			var app = Current as App;
			app.MainPage = new NavigationPage(new MisReclamosPage()) { BarBackgroundColor = (Color)App.Current.Resources["AzulColor"] };	
		}


		private static bool CheckCambiarContrasena() { 

			ResetPasswordInfo data = DependencyService.Get<IResetPassword>().GetDataFromUriCall();
			if (!String.IsNullOrEmpty(data.userId)) {
				Console.WriteLine("check cambioar contraseña_userid=" + data.userId);
				return true;
			}
			return false;
		}

		public static void GoToCambiarContrasena(ResetPasswordInfo data) { 

			DependencyService.Get<IResetPassword>().CleanDataFromUriCall();
			var app = Current as App;
			app.MainPage = new NavigationPage(new CreaContraseñaPage(data)) { BarBackgroundColor = (Color)App.Current.Resources["AzulColor"] };

		}

        public static void GoToRegistrar()
        {
            var app = Current as App;
            app.MainPage = new NavigationPage(new RegistroPage()) { BarBackgroundColor = (Color)App.Current.Resources["AzulColor"] };
        }

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
            GetConfig();

			if (CheckCambiarContrasena())
			{
				ResetPasswordInfo data = DependencyService.Get<IResetPassword>().GetDataFromUriCall();
				GoToCambiarContrasena(data);
			}

			// Handle when your app resumes
		}
	}

	public class ListDataViewCell : ViewCell
	{
		public ListDataViewCell()
		{
			var label = new Label()
			{
				Font = Font.SystemFontOfSize(NamedSize.Default),
				TextColor = Color.Blue
			};
			label.SetBinding(Label.TextProperty, new Binding("TextValue"));
			label.SetBinding(Label.ClassIdProperty, new Binding("DataValue"));
			View = new StackLayout()
			{
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(12, 8),
				Children = { label }
			};
		}
	}

	public class SimpleObject
	{
		public string TextValue
		{ get; set; }
		public string DataValue
		{ get; set; }
	}
}
