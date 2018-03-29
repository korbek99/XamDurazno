using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IDD
{
	public class VersionService
	{
		public static async Task<dynamic> GetVersion()
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.MaxResponseContentBufferSize = 256000;

					var uri = new Uri("http://ewin-appidd.com:3000/versions/");

					client.DefaultRequestHeaders
						  .Accept
						  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
																						//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					var response = await client.GetAsync(uri);
					if (response.IsSuccessStatusCode)
					{
						var reponseContent = await response.Content.ReadAsStringAsync();
						var rootobject = JsonConvert.DeserializeObject<List<Version>>(reponseContent);
						return rootobject[0];
					}
				}
			}
			catch (Exception e)
			{
				return null;
			}

			return null;
		}
	}
}
