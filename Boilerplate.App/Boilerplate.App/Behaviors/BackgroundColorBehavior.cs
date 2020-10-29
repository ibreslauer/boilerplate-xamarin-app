using System.Linq;
using Boilerplate.App.Effects;
using Xamarin.Forms;

namespace Boilerplate.App.Behaviors
{
    public static class BackgroundColorBehavior
    {
        public static readonly BindableProperty ApplyBackgroundColorProperty =
            BindableProperty.CreateAttached("ApplyBackgroundColor", typeof(bool), typeof(BackgroundColorBehavior), false,
                propertyChanged: OnApplyBackgroundColorChanged);

        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(BackgroundColorBehavior), Color.Default);

        public static bool GetApplyBackgroundColor(BindableObject view)
        {
            return (bool)view.GetValue(ApplyBackgroundColorProperty);
        }

        public static void SetApplyBackgroundColor(BindableObject view, bool value)
        {
            view.SetValue(ApplyBackgroundColorProperty, value);
        }

        public static Color GetBackgroundColor(BindableObject view)
        {
            return (Color)view.GetValue(BackgroundColorProperty);
        }

        public static void SetBackgroundColor(BindableObject view, Color value)
        {
            view.SetValue(BackgroundColorProperty, value);
        }

        private static void OnApplyBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view == null)
            {
                return;
            }

            bool hasBackground = (bool)newValue;

            if (hasBackground)
            {
                view.Effects.Add(new EntryLineColorEffect());
            }
            else
            {
                var entryLineColorEffectToRemove = view.Effects.FirstOrDefault(e => e is EntryLineColorEffect);
                if (entryLineColorEffectToRemove != null)
                {
                    view.Effects.Remove(entryLineColorEffectToRemove);
                }
            }
        }
    }
}
