using System;
namespace IDD
{
    public class Cuota
    {
        public string mes {
            get;
            set;
        }

        public int dia {
            get;
            set;
        }

        public string nombre {
            get;
            set;
        }

        public string montoString {
            get{ return "$" + monto.ToString(); }
        }

        public double monto {
            get;
            set;
        }
    }
}
