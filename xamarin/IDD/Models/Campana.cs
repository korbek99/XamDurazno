using System;
namespace IDD
{
    public class Campana
    {
        public int id {
            get;
            set;
        }

        public string titulo {
            get;
            set;
        }

        public string descripcion {
            get;
            set;
        }

        public string foto_url {
            get;
            set;
        }

        public string pdf_url
		{
			get;
			set;
		}

		public string video_url
		{
			get;
			set;
		}

        public DateTime fecha_inicio {
            get;
            set;
        }

        public DateTime fecha_fin {
            get;
            set;
        }

        public int likes {
            get;
            set;
        }
		public bool me_gusta {
			get;
			set;
		}

        public int cantidad_comentarios {
            get;
            set;
        }
    }
}
