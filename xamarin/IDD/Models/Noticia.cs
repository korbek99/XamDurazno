using System;
using System.Collections.Generic;

namespace IDD
{
    public class Noticia
    {
        public int id
        {
            get;
            set;
        }

        public string imagen
        {
            get;
            set;
        }

        public string titulo
        {
            get;
            set;
        }

		public string descripcion
		{
			get;
			set;
		}

		public string imgYoutube
		{
			get;
			set;
		}

		public string urlYoutube
		{
			get;
			set;
		}

        public List<Foto> imagenes
        {
            get;
            set;
        }
    }
}
