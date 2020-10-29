using System.ComponentModel;

using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using View = Android.Views.View;

using Boilerplate.App.CustomControls;
using Boilerplate.App.Droid.Renderers;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace Boilerplate.App.Droid.Renderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        private View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            _selected = false;
            _unselectedBackground = _cellCore.Background;
            return _cellCore;
        }
        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);
            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;
                if (_selected)
                {
                    var extendedViewCell = sender as CustomViewCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.SelectedItemBackgroundColor.ToAndroid());
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }
    }
}