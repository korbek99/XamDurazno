using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IDD
{
    public class ReclamoService
    {
        public async Task<string> CrearReclamo(string titulo, 
                                               string descripcion, 
                                               string nombres, 
                                               string email, 
                                               string telefono, 
                                               string latitud, 
                                               string longitud, 
                                               List<byte[]> listByte,
                                               string referencia)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
					client.MaxResponseContentBufferSize = 5256000;

					MultipartFormDataContent form = new MultipartFormDataContent();
					if (listByte != null)
					{
						for (int i = 0; i < listByte.Count; i++)
						{
							form.Add(new ByteArrayContent(listByte[i], 0, listByte[i].Length), "userfile[]", i + ".jpg");
						}
					}

					form.Add(new StringContent(titulo), "titulo");
					form.Add(new StringContent(descripcion), "descripcion");
					
					
					
                    form.Add(new StringContent(latitud.Replace(',', '.')), "latitude");
					form.Add(new StringContent(longitud.Replace(',', '.')), "longitude");

                    if (telefono != null){
                        form.Add(new StringContent(telefono), "telefono");
                    }

                    if (nombres != null){
                        form.Add(new StringContent(nombres), "nombres");
                    }

                    if(email != null){
                        form.Add(new StringContent(email), "email");
                    }


                    if (referencia != null){
                        form.Add(new StringContent(referencia), "address");
                    }


					var response = await client.PostAsync("api/reclamo/reclamo", form);

					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "-1")
						{
							var s = content["mensaje"].ToString();

							return s;
						}
                        else{
                            return "En Menú: Mis Reclamos,  puedes seguir el estado de tu reclamo";
                        }
					}
                   

                    return null;
				}
			}
			catch (Exception e)
			{
				//return null;
				return "Ha ocurrido un error";
			}
		}

		public static async Task<String> GetImagenParaCompartir(string imagenAntes,string imagenDespues)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
					var response = await client.GetAsync("api/reclamo/imagen_reclamo_compartir?imagen_antes=" + imagenAntes + "&imagen_despues=" + imagenDespues);


					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
						{
							var data = content["data"].ToString();
							return data;
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

        public static async Task<String> GetImagenParaCompartirData(string imagenAntes, string imagenDespues, string datas ) {
            try {
                using (var client = ServiceBase.GetClient()) {
                    var response = await client.GetAsync("api/reclamo/shares_html?imagen_antes=" + imagenAntes + "&imagen_despues=" + imagenDespues + "&imagen_tercera=" + datas );

                    if (response.IsSuccessStatusCode) {
                        var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

                        if (content["codigo"].ToString() == "1") {
                            var data = content["data"].ToString();
                            return data;
                        }
                    }

                  return null;
                }
            } catch (Exception e) {
                return null;
            }


        }
    }
}
