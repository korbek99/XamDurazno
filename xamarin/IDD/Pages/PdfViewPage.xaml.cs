using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IDD
{
    public partial class PdfViewPage : ContentPage
    {
        public PdfViewPage()
        {
            InitializeComponent();

            pdfView.Source = "http://drive.google.com/viewerng/viewer?embedded=true&url=http://www.alberguemontfalco.com/fotosbd/120520140951280826.pdf";

		}

        public PdfViewPage(string url)
		{
			InitializeComponent();

			if (!String.IsNullOrEmpty(url))
			{
				if (url.ToUpper().StartsWith("HTTP"))
				{
					pdfView.Source = "http://drive.google.com/viewerng/viewer?embedded=true&url=" + url;
				}//mostrar no preview available cuando no sea un url valida
				else
				{
					pdfView.Source = "http://drive.google.com/viewerng/viewer?embedded=true&url=" + "http://";
				}
			}
			else { 
				//mostrar no preview available cuando venga vacio
				pdfView.Source = "http://drive.google.com/viewerng/viewer?embedded=true&url=" + "http://";
			}

		}
    }
}
