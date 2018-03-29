//using IDD;
//using IDD.Droid;
//using System;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
using IDD.ControlRender;
using System;
using Xamarin.Forms;
using System.ComponentModel;
using Android.Views;
using IDD.Droid;
using CustomRenderer.Droid;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace CustomRenderer.Droid
{
	public class ContentPageRenderer : Xamarin.Forms.Platform.Android.PageRenderer
	{
		//protected override void OnLayout(bool changed, int l, int t, int r, int b)
      //{
      //    base.OnLayout(changed, l, t, r, b);
      //    var actionBar = ((Android.App.Activity)Context).ActionBar;
      //      actionBar.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.home_disfruta));
      //}
	}
}