using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace IDD
{
	public class UserService
	{
		public UserService()
		{
		}

		public async Task<JObject> PostUser(UserFB user)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{

					client.MaxResponseContentBufferSize = 5256000;

					MultipartFormDataContent form = new MultipartFormDataContent();
					//if (listByte != null)
					//{
					//	for (int i = 0; i < listByte.Count; i++)
					//	{
					//		form.Add(new ByteArrayContent(listByte[i], 0, listByte[i].Length), "userfile[]", i + ".jpg");
					//	}
					//}
					form.Add(new StringContent(user.email), "email");
					form.Add(new StringContent(user.full_name), "full_name");
					form.Add(new StringContent(user.phone), "phone");
					form.Add(new StringContent(user.password), "password");


					var response = await client.PostAsync("api/user/user", form);

					if (response.IsSuccessStatusCode)
					{

						var str = await response.Content.ReadAsStringAsync();
						var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(str);

						return responseContent;

						//if (responseContent["codigo"].ToString() == "1")
						//{
						//	var s = responseContent["data"].ToString();
						//	var res = Newtonsoft.Json.JsonConvert.DeserializeObject<UserFB>(s);

						//	var json = new JObject();
						//	json.Add("data", s);
						//	json.Add("codigo"



						//	return res;
						//}
						//else {


						//}
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


		public static async Task<JObject> PostLogin(string email, string password)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{

					client.MaxResponseContentBufferSize = 5256000;



					Console.WriteLine("email,password" + email + "_" + password);


					MultipartFormDataContent form = new MultipartFormDataContent();

					form.Add(new StringContent(email), "email");
					form.Add(new StringContent(password), "password");


					var response = await client.PostAsync("api/user/login_user_app", form);

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



		public static async Task<List<Reclamo>> GetReclamos(string email)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{
					var response = await client.GetAsync("api/user/reclamos?email=" + email);

					if (response.IsSuccessStatusCode)
					{
						var content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

						if (content["codigo"].ToString() == "1")
						{
							var s = content["data"].ToString();
							var r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Reclamo>>(s);

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


		//registrar dispositivo para recibir notificaciones push
		public static async Task<JObject> PostRegisterSNSDevice(int userId, string endPointArn,string applicationArn)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{

					client.MaxResponseContentBufferSize = 5256000;




					MultipartFormDataContent form = new MultipartFormDataContent();


					form.Add(new StringContent(userId.ToString()), "user_id");
					form.Add(new StringContent(endPointArn), "endpoint_arn");
					form.Add(new StringContent(applicationArn), "application_arn");


					var response = await client.PostAsync("api/user/register_sns_device", form);

					if (response.IsSuccessStatusCode)
					{
						
						var str = await response.Content.ReadAsStringAsync();
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


		public static async Task<JObject> PostResetPasswordRequest(string email)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{

					client.MaxResponseContentBufferSize = 5256000;

					MultipartFormDataContent form = new MultipartFormDataContent();

					form.Add(new StringContent(email), "email");


					var response = await client.PostAsync("api/user/reset_password_request", form);

					if (response.IsSuccessStatusCode)
					{
						var str = await response.Content.ReadAsStringAsync();
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

		public static async Task<JObject> PostResetPassword(string userId,string password,string token)
		{
			try
			{
				using (var client = ServiceBase.GetClient())
				{

					client.MaxResponseContentBufferSize = 5256000;

					MultipartFormDataContent form = new MultipartFormDataContent();

					form.Add(new StringContent(userId), "user_id");
					form.Add(new StringContent(password), "password");
					form.Add(new StringContent(token), "token");


					var response = await client.PostAsync("api/user/reset_password", form);

					if (response.IsSuccessStatusCode)
					{
						var str = await response.Content.ReadAsStringAsync();
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
