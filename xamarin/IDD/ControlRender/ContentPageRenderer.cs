using IDD;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform;



[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace IDD.ControlRender
{
	public class ContentPageRenderer : PageRenderer
	{
		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			base.OnLayout(changed, l, t, r, b);
			var actionBar = ((Activity)Context).ActionBar;
			actionBar.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.home_disfruta));
		}
	}
}
