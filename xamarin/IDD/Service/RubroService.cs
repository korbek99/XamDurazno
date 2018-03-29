using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace IDD
{
    public class RubroService
    {
        public async Task<List<Rubro>> GetRubros()
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
					var response = await client.GetAsync("api/rubro/rubros");

					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
						{
							var s = content["data"].ToString();
                            var r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Rubro>>(s);

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
