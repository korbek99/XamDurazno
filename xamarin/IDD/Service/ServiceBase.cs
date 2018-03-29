using System;
using System.Net.Http;
using ModernHttpClient;

namespace IDD
{
    public class ServiceBase
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient();

			client.MaxResponseContentBufferSize = 256000;

			client.DefaultRequestHeaders
				  .Accept
				  .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.BaseAddress = new Uri(GlobalHelpers.URL_BASE_PROD);

            //client.BaseAddress = new Uri(GlobalHelpers.URL_BASE);  //desarrollo


			//client.BaseAddress = new Uri(GlobalHelpers.URL_BASE_LOCAL);

            return client;
        }
    }
}
