using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json.Linq;

namespace IDD
{
    public class NoticiaService
    {
        public async Task<List<Noticia>> GetNoticias(bool destacadas = false)
		{
			try
			{
                using (var client = ServiceBase.GetClient())
                {
                    var response = await client.GetAsync("api/noticia/noticias?destacada="+destacadas);

                    if(response.IsSuccessStatusCode)
                    {
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if(content["codigo"].ToString() == "1")
                        {
                            var s = content["data"].ToString();
                            var r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Noticia>>(s);

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

		public async Task<Noticia> GetNoticia(int id)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
                    var response = await client.GetAsync("api/noticia/noticia?id=" + id);

					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
						{
							var s = content["data"].ToString();
							var r = Newtonsoft.Json.JsonConvert.DeserializeObject<Noticia>(s);

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
