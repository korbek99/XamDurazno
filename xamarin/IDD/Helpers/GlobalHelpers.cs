using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace IDD
{
    public class GlobalHelpers
    {
        public const string URL_BASE = "http://ec2-54-88-114-83.compute-1.amazonaws.com/idd/index.php/";
		public const string URL_BASE_LOCAL = "http://localhost:8000/index.php/";
        public const string URL_BASE_PROD = "http://190.0.158.2:8083/api/index.php/";

		public static Boolean EmailBienEscrito(String email)
		{
			String expresion;
			expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
			if (Regex.IsMatch(email, expresion))
			{
				if (Regex.Replace(email, expresion, String.Empty).Length == 0)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public static void OnEntryTextChanged(object sender, TextChangedEventArgs e)
		{
			var entry = (Entry)sender;

			//if (entry.Text.Length > 11)
			//{
			//	string entryText = entry.Text;
			//	entry.TextChanged -= OnEntryTextChanged;
			//	entry.Text = e.OldTextValue;
			//	entry.TextChanged += OnEntryTextChanged;
			//}
		}
    }
}
