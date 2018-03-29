using System;
using System.Collections.Generic;

namespace IDD
{
	public class FestivalItem
	{
		public FestivalItem()
		{
		}

		public int id
		{
			get;
			set;
		}

		public string bg_image
		{
			get;
			set;
		}
		public string title
		{
			get;
			set;
		}

		public string region_name
		{
			get;
			set;
		}

		public string description
		{
			get;
			set;
		}

		public bool has_streaming
		{
			get;
			set;
		}

		public string url_stream
		{
			get;
			set;
		}

		public string url
		{
			get;
			set;
		}

		public DateTime date
		{
			get;
			set;
		}

		//public List<Expositor> artistas;

		public List<FestivalItem> programa;

        public static List<FestivalItem> GetLugares()
        {
            string json = "[\n\t\t{\n\t\t\t\"bg_image\": \"lugar_p_hispanidad.jpg\",\n\t\t\t\"title\": \"Parque de la Hispanidad\",\n\t\t\t\"description\": \"Fotos y videos\",\n            \"url\": \"http://ewin-appidd.com/hispanidad\"\n\t\t},\n\t\t{\n\t\t\t\"bg_image\": \"lugar_llamadas.jpg\",\n\t\t\t\"title\": \"Llamadas\",\n\t\t\t\"description\": \"Fotos y videos\",\n            \"url\": \"http://ewin-appidd.com/llamadas\"\n\t\t},\n\t\t{\n\t\t\t\"bg_image\": \"lugar_rio.jpg\",\n\t\t\t\"title\": \"Playa El Sauzal\",\n\t\t\t\"description\": \"Fotos y videos\",\n            \"url\": \"http://ewin-appidd.com/elsauzal\"\n\t\t},\n\t\t{\n\t\t\t\"bg_image\": \"lugar_camping.jpg\",\n\t\t\t\"title\": \"Camping 33 Orientales\",\n\t\t\t\"description\": \"Fotos y videos\",\n            \"url\": \"http://ewin-appidd.com/orientales\"\n\t\t},\n        {\n\t\t\t\"bg_image\": \"lugar_iglesia.jpg\",\n\t\t\t\"title\": \"Iglesia San Pedro\",\n\t\t\t\"description\": \"Fotos y videos\",\n            \"url\": \"http://ewin-appidd.com/sanpedro\"\n\t\t},\n\t\t{\n\t\t\t\"bg_image\": \"lugar_museo.jpg\",\n\t\t\t\"title\": \"Museo Casa de Rivera\",\n\t\t\t\"description\": \"Fotos y videos\",\n            \"url\": \"http://ewin-appidd.com/rivera\"\n\t\t},\n\t\t{\n\t\t\t\"bg_image\": \"lugar_bioparque.jpg\",\n\t\t\t\"title\": \"Bioparque de Durazno\",\n\t\t\t\"description\": \"Fotos y videos\",\n            \"url\": \"http://ewin-appidd.com/bioparque\"\n\t\t}\n\t]";

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<FestivalItem>>(json);
        }
	}
}
