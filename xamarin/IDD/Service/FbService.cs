using System;
using System.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IDD
{
	public class FbService
	{
		public static async Task<JObject> PostUserFB(UserFB user)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
                    var r = JsonConvert.SerializeObject(user);
                    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");


					client.MaxResponseContentBufferSize = 5256000;

					MultipartFormDataContent form = new MultipartFormDataContent();

					form.Add(new StringContent(user.token), "token");
					form.Add(new StringContent(user.url??String.Empty), "url");
					form.Add(new StringContent(user.expireDate.ToShortDateString()), "expireDate");
					form.Add(new StringContent(user.id_fbk), "id_fbk");
					form.Add(new StringContent(user.email), "email");
					form.Add(new StringContent(user.first_name), "first_name");
					form.Add(new StringContent(user.last_name), "last_name");
					form.Add(new StringContent(user.phone), "phone");
					form.Add(new StringContent(user.first_name +" "+ user.last_name), "full_name");
					form.Add(new StringContent(user.public_profile??String.Empty), "public_profile");
					form.Add(new StringContent(user.user_hometown??String.Empty), "user_hometown");
					form.Add(new StringContent(user.user_location??String.Empty), "user_location");
					form.Add(new StringContent(user.gender??string.Empty), "gender");
				

                    var response = await client.PostAsync("api/user/user_fb", form);

					if (response.IsSuccessStatusCode)
					{

						var str = await response.Content.ReadAsStringAsync();
						var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(str);

						return responseContent;
						//return user;
					}
					else { 
						
						var errorResponse = new JObject();
						errorResponse.Add("codigo", "-1");
						errorResponse.Add("mensaje", "ha ocurrido un error");
						return errorResponse;
					}

					return null;
				}
			}
			catch (Exception e)
			{
				return null;
			}
		}


       


		public static async Task<JObject> PostLogin(string id_fbk)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{

					client.MaxResponseContentBufferSize = 5256000;

					MultipartFormDataContent form = new MultipartFormDataContent();

					form.Add(new StringContent(id_fbk), "id_fbk");
				


					var response = await client.PostAsync("api/user/login_user_facebook", form);

					if (response.IsSuccessStatusCode)
					{

						var str = await response.Content.ReadAsStringAsync();
						var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(str);

						return responseContent;

					}
					else {

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

		//public static async Task<dynamic> GetFacebookProfileAsync(string accessToken)
		//{
		//	var requestUrl =
		//		"https://graph.facebook.com/v2.7/me/?fields=email,first_name,last_name,gender,picture&access_token="
		//		+ accessToken;

		//	using (var httpClient = new HttpClient())
		//	{
		//		var userJson = await httpClient.GetStringAsync(requestUrl);

  //              var obj = Newtonsoft.Json.JsonConvert.SerializeObject(userJson);

		//		return obj;
		//	}


		//}
	}
}
