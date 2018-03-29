using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IDD
{
    public partial class OtrosPage : ContentPage
    {

		TapGestureRecognizer tapTerminosYCondiciones;


        public OtrosPage()
        {
            InitializeComponent();

			tapTerminosYCondiciones = new TapGestureRecognizer();
			tapTerminosYCondiciones.Tapped  += async (object sender, EventArgs e) =>
			{
				//string url = "https://www.datospersonales.gub.uy/wps/wcm/connect/urcdp/57e2264e-370f-411d-933c-63aca968c88b/Clausula_de_Consentimiento_para_Organismos_Publicos.pdf?MOD=AJPERES&CONVERT_TO=url&CACHEID=57e2264e-370f-411d-933c-63aca968c88b";
				if (!String.IsNullOrEmpty(App.configuracion.urlTerminosYCondiciones)){ 
					await Navigation.PushAsync(new PdfViewPage(App.configuracion.urlTerminosYCondiciones)); 
				}

				
			};

			stkTerminosYCondiciones.GestureRecognizers.Add(tapTerminosYCondiciones);


        }
    }
}
