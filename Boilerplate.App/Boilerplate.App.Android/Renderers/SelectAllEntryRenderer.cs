using Android.Content;
using Boilerplate.App.CustomControls;
using Boilerplate.App.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SelectAllEntry), typeof(SelectAllEntryRenderer))]
namespace Boilerplate.App.Droid.Renderers
{
    public class SelectAllEntryRenderer : MaterialEntryRenderer
    {
        public SelectAllEntryRenderer(Context context) : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var nativeEditText = Control.EditText;
                nativeEditText.SetSelectAllOnFocus(true);
            }
        }
    }
}