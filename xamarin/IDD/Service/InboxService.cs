using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace IDD
{
	public class InboxService
	{
		public InboxService()
		{

		}


		public static async Task<List<Message>> GetMessages(int userId)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
					var response = await client.GetAsync("api/user/inbox?user_id=" + userId.ToString());

					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
						{
							var s = content["data"].ToString();
							var r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Message>>(s);

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

		public static async Task<JObject> DeleteMessage(string id)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{

					client.MaxResponseContentBufferSize = 5256000;




					MultipartFormDataContent form = new MultipartFormDataContent();

					form.Add(new StringContent(id), "id");

					var response = await client.PostAsync("api/user/inbox_eliminar", form);

					if (response.IsSuccessStatusCode)
					{
						Console.WriteLine("entre a success");
						var str = await response.Content.ReadAsStringAsync();
						Console.WriteLine(str);

						var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(str);


						return responseContent;

					}
					else
					{

						var errorResponse = new JObject();
						errorResponse.Add("codigo", "-1");
						errorResponse.Add("mensaje", "ha ocurrido un error");

						return errorResponse;
					}


				}
			}
			catch (Exception e)
			{
				return null;


			}

		}
	}
}
