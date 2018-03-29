using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class CalendarioPagoPage : ContentPage
    {
        RubroService _ruborService;
        public CalendarioPagoPage()
        {
            InitializeComponent();
            _ruborService = new RubroService();

            //MainOne.DataSource = GetSampleData();
            //MainOne.DataBind();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Loading(true);
            LoadMain();
        }

		async Task LoadMain()
		{
			await Task.Run(async () =>
			{
                var noticias = await _ruborService.GetRubros();
				Console.WriteLine("OK listas");
				Device.BeginInvokeOnMainThread(() =>
				{
					if (noticias != null)
                    {
                        var vResult = new List<AccordionSource>();

                        foreach(var i in noticias)
                        {
							var vFirstAccord = new AccordionSource()
							{
                                ContentItems = new ListCuotas(i._cuotas),
								rubro = i
							};
							vResult.Add(vFirstAccord);
                        }

                        MainOne.DataSource = vResult;
						MainOne.DataBind();
					}

					Loading(false);
				});
			});
		}

		void Loading(bool isVisible)
		{
			boxIndicator.IsVisible = isVisible;
			actIndicator.IsRunning = isVisible;
			actIndicator.IsVisible = isVisible;
		}

        public List<AccordionSource> GetSampleData()
        {
            var vResult = new List<AccordionSource>();

            #region Second List
            var listTestCuota = new List<Cuota>();
            listTestCuota.Add(
                new Cuota()
                {
                    dia = 10,
                    mes = "sept",
                    nombre = "1a.Cuota",
                    monto = 10.78
                });
			listTestCuota.Add(
				new Cuota()
				{
					dia = 10,
					mes = "sept",
					nombre = "1a.Cuota",
					monto = 10.78
				});
			listTestCuota.Add(
				new Cuota()
				{
					dia = 10,
					mes = "sept",
					nombre = "1a.Cuota"
				});
			#endregion

			#region StackLayout
			var listTestCuota2 = new List<Cuota>();
			listTestCuota2.Add(
				new Cuota()
				{
					dia = 10,
					mes = "sept",
					nombre = "1a.Cuota",
					monto = 10.78
				});
			listTestCuota2.Add(
				new Cuota()
				{
					dia = 10,
					mes = "sept",
					nombre = "1a.Cuota",
					monto = 10.78
				});
			listTestCuota2.Add(
				new Cuota()
				{
					dia = 10,
					mes = "sept",
					nombre = "1a.Cuota"
				});
			listTestCuota2.Add(
				new Cuota()
				{
					dia = 10,
					mes = "sept",
					nombre = "1a.Cuota",
					monto = 10.78
				});
			listTestCuota2.Add(
				new Cuota()
				{
					dia = 10,
					mes = "sept",
					nombre = "1a.Cuota",
					monto = 10.78
				});
			listTestCuota2.Add(
				new Cuota()
				{
					dia = 10,
					mes = "sept",
					nombre = "1a.Cuota"
				});
            #endregion

            var vFirstAccord = new AccordionSource()
            {
                ContentItems = new ListCuotas(listTestCuota),
                rubro = new Rubro { id = 1, icono = "calendario_inmb", year = "2017", nombre = "test1" }
            };
            vResult.Add(vFirstAccord);

            var vSecond = new AccordionSource()
            {
                ContentItems = new ListCuotas(listTestCuota2),
                rubro = new Rubro { id = 2, icono = "calendario_rodados", year = "2017", nombre = "test2" }
            };
            vResult.Add(vSecond);
            var vThird = new AccordionSource()
            {
                ContentItems = new ListCuotas(listTestCuota),
                rubro = new Rubro { id = 3, icono = "calendario_rural", year = "2017", nombre = "test3" }
            };
            vResult.Add(vThird);
            return vResult;
        }
    }
}
