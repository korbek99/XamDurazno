using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IDD
{
    public class CampanaService
    {
        public CampanaService()
        {
        }

        public async Task<List<Campana>> GetCampanas(int id_usuario)
        {
            try
            {
                using (var client = ServiceBase.GetClient())
                {
					var response = await client.GetAsync("api/campana/campanas_visible?user_id="+id_usuario.ToString());

                    if (response.IsSuccessStatusCode)
                    {
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
                        {
                            var s = content["data"].ToString();
                            var r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Campana>>(s);

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

        public async Task<String> Like(int id_campana, int id_usuario)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
					
					MultipartFormDataContent form = new MultipartFormDataContent();

					form.Add(new StringContent(id_campana.ToString()), "id_campana");
					form.Add(new StringContent(id_usuario.ToString()), "id_usuario_app");



					var response = await client.PostAsync("api/campana/campana_likes", form);

					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
						{
							var s = content["mensaje"].ToString();

							return s;
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

		public async Task<String> Comentar(int id_campana, int id_usuario, string comentario)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
					//dynamic obj = new ExpandoObject();

					JObject obj = new JObject();
					obj.Add("id_campana", id_campana);
					obj.Add("id_usuario_app", id_usuario);
					obj.Add("descripcion", comentario);


					//obj.id_campana = id_campana.ToString();
					//obj.id_usuario_app = id_usuario.ToString();
     //               obj.descripcion = comentario;

					var contentPost = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
					var response = await client.PostAsync("api/campana/campana_comentario", contentPost);

					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
						{
							var s = content["mensaje"].ToString();

							return s;
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
