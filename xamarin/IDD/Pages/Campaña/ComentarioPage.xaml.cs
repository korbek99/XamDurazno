﻿﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDD
{
    public partial class ComentarioPage : ContentPage
    {
        TapGestureRecognizer tapComentar;
        
        CampanaService _camapanaService;
        FbService _fbService;


        Campana campana;
        public ComentarioPage()
        {
            InitializeComponent();
        }

		public ComentarioPage(Campana campana)
		{
			InitializeComponent();

			_camapanaService = new CampanaService();
			this.campana = campana;


            editorDescription.TextChanged += EditorDescription_TextChanged;

            tapComentar = new TapGestureRecognizer();
            tapComentar.Tapped += TapComentar_Tapped;

            stkComentar.GestureRecognizers.Add(tapComentar);
            Loading(false);
		}



        void EditorDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
			string _text = editorDescription.Text;      //Get Current Text
			if (_text != null)
			{
				if (_text.Length > 524)       //If it is more than your character restriction
				{
					_text = _text.Remove(_text.Length - 1);  // Remove Last character
					editorDescription.Text = _text;        //Set the Old value
				}
			}
        }

        void TapComentar_Tapped(object sender, EventArgs e)
        {
            try
            {
				if (editorDescription.Text.Length > 0 && editorDescription.Text != "Agrega un comentario a esta campaña")
				{
                    Loading(true);
                    Comentar();
				}
            }catch(Exception ex)
            {}

        }

		public async Task Comentar()
		{
			await Task.Run(async () =>
			{
				

                var idUsuario = App.userFB.id;

                var msnj = await _camapanaService.Comentar(campana.id, idUsuario, editorDescription.Text);
				Device.BeginInvokeOnMainThread(async () =>
				{
					await DisplayAlert("Comentario", msnj, "Aceptar");
					if (msnj == "Comentario creado exitosamente."){
						campana.cantidad_comentarios++;
						await Navigation.PopAsync();
						//campana.cantidad_comentarios++;
						//await Navigation.PushAsync(new DetalleCampanaPage(campana));

					}
						
					Loading(false);
				});
			});
		}

		void Loading(bool isVisible)
		{
			boxIndicator.IsVisible = isVisible;
			actIndicator.IsRunning = isVisible;
			actIndicator.IsVisible = isVisible;
		}
    }
}
