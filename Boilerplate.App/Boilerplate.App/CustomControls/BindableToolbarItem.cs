using System.Linq;
using Xamarin.Forms;

namespace Boilerplate.App.CustomControls
{
    public class BindableToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create("IsVisible", typeof(bool), typeof(BindableToolbarItem),
                defaultValue: true, BindingMode.OneWay, propertyChanged: OnIsVisibleChanged);

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        private static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var item = bindable as BindableToolbarItem;
            Device.BeginInvokeOnMainThread(() => { item.SetVisibility(newValue); });
        }

        public BindableToolbarItem()
        { }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            SetVisibility(IsVisible);
        }

        void SetVisibility(object newValue)
        {
            if (Parent != null)
            {
                var items = ((ContentPage)Parent).ToolbarItems;

                if ((bool)newValue && !items.Contains(this))
                {
                    // Find the insertion point (based on Priority of other toolbar items)
                    var nextItem = items.FirstOrDefault(i => i.Priority > Priority);
                    var idx = (nextItem != null) ? items.IndexOf(nextItem) : items.Count;
                    // Insert this toolbar item
                    items.Insert(idx, this);
                }
                else if (!(bool)newValue && items.Contains(this))
                {
                    items.Remove(this);
                }
            }
        }
    }
}
