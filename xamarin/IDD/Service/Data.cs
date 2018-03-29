using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Newtonsoft.Json;

namespace IDD
{
	public class Data
	{
		public static async System.Threading.Tasks.Task<dynamic> GetHomeServer()
		{
			using (var client = new HttpClient())
			{
				client.MaxResponseContentBufferSize = 256000;

				var uri = new Uri("http://ec2-54-88-114-83.compute-1.amazonaws.com/congreso/public/index.php/congreso/getJsonv44");

				client.DefaultRequestHeaders
					  .Accept
					  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
				try
				{
					var response = await client.GetAsync(uri);
					if (response.IsSuccessStatusCode)
					{
						var reponseContent = await response.Content.ReadAsStringAsync();
                        //var rootobject = JsonConvert.DeserializeObject<FestivalJson>(reponseContent);
                        //return rootobject;
                        return null;
					}
					else return GetHome();
				}
				catch (Exception e) {
					return GetHome();
				}

			}
		}

		public static dynamic GetHome()
		{
#if __IOS__
			var resourcePrefix = "IDD.iOS.";
#endif
#if __ANDROID__
			var resourcePrefix = "IDD.Droid.";
#endif
			var assembly = typeof(Data).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream
				(resourcePrefix + "Json.festival.json");

			using (var reader = new StreamReader(stream))
			{

				var json = reader.ReadToEnd();
				//var rootobject = JsonConvert.DeserializeObject<FestivalJson>(json);

                return null;
			}
		}
	}
}
