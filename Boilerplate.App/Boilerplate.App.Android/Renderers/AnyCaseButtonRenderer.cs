using Android.Content;
using Boilerplate.App.CustomControls;
using Boilerplate.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AnyCaseButton), typeof(AnyCaseButtonRenderer))]
namespace Boilerplate.App.Droid.Renderers
{
    public class AnyCaseButtonRenderer : ButtonRenderer
    {
        public AnyCaseButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var button = this.Control;
            button.SetAllCaps(false);
        }
    }
}