using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace IDD
{
	public class ConfiguracionService
	{
		public ConfiguracionService()
		{

		}


		public static async Task<Configuracion> GetConfiguracion()
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
					var response = await client.GetAsync("api/configuracion/configuracion_app");

					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
						{
							var s = content["data"].ToString();
							var r = Newtonsoft.Json.JsonConvert.DeserializeObject<Configuracion>(s);

							return r;
						}
					}

					return null;
				}
			}
			catch (Exception e)
			{
				return null;
			}
		}



	}
}
