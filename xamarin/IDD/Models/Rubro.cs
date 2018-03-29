using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IDD
{
    public class Rubro
    {
        public int id
        {
            get;
            set;
        }
        public string nombre 
        {
            get;
            set;
        }

        public string icono 
        {
            get;
            set;
        }

		string _year;
		public string year
		{
			get { return "Cuotas " + _year; }
			set { _year = value; }
		}

        public int cuotas {
            get;
            set;
        }

		public List<Cuota> _cuotas
		{
			get;
			set;
		}
    }
}
