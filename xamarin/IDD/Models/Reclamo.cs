using System;
namespace IDD
{
	public class Reclamo
	{
		public int id
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

		public string nombres
		{
			get;
			set;
		}

		public string email
		{
			get;
			set;
		}

		public string telefono
		{
			get;
			set;
		}

		public string pdf
		{
			get;
			set;
		}


		public string fechaCreacion
		{
			get;
			set;
		}

		public string estado
		{
			get;
			set;
		}



		public string imagenCompartir
		{
			get;
			set;
		}

		public string mensajeCompartir
		{
			get;
			set;
		}


		public EstadoReclamo estadoActual {
			get;
			set;
		}

		public ImagenReclamo[] imagenes {
			get;set;
		}

		public bool IsFinalizado
		{
			get { return (estadoActual.nombre.ToUpper() == "RESUELTO"); }
		}

		public Reclamo() {
			this.imagenes = new ImagenReclamo[] { };
			this.estadoActual = new EstadoReclamo();

		}
	}



	public class EstadoReclamo
	{

		public string id { get; set; }
		public string color { get; set; }
		public string nombre { get; set; }
	}

	public class ImagenReclamo {

		public bool esDespues { get; set; }
		public string url { get; set; }

	}


}
