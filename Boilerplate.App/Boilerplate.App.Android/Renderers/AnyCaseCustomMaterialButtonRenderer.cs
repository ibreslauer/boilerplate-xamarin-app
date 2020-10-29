using Android.Content;
using Boilerplate.App.CustomControls;
using Boilerplate.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(AnyCaseCustomMaterialButtonRenderer), new[] { typeof(CustomVisual) })]
namespace Boilerplate.App.Droid.Renderers
{
    public class AnyCaseCustomMaterialButtonRenderer : MaterialButtonRenderer
    {
        public AnyCaseCustomMaterialButtonRenderer(Context context) : base(context)
        {
        }

        public AnyCaseCustomMaterialButtonRenderer(Context context, BindableObject element) : base(context, element)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetAllCaps(false);
            }
        }
    }
}