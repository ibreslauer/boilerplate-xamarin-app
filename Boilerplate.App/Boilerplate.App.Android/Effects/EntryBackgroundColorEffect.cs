using Android.Widget;
using Boilerplate.App.Behaviors;
using Boilerplate.App.Effects;
using Boilerplate.App.Droid.Effects;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Material.Android;

[assembly: ExportEffect(typeof(EntryBackgroundColorEffectDroid), nameof(EntryBackgroundColorEffect))]
namespace Boilerplate.App.Droid.Effects
{
    public class EntryBackgroundColorEffectDroid : PlatformEffect
    {
        EditText control;

        protected override void OnAttached()
        {
            try
            {
                if (Control is MaterialFormsTextInputLayoutBase materialControl)
                {
                    control = materialControl.EditText;
                }
                else
                {
                    control = Control as EditText;
                }
                UpdateBackgroundColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == BackgroundColorBehavior.BackgroundColorProperty.PropertyName)
            {
                UpdateBackgroundColor();
            }
        }

        private void UpdateBackgroundColor()
        {
            try
            {
                if (control != null)
                {
                    control.Background.SetColorFilter(BackgroundColorBehavior.GetBackgroundColor(Element).ToAndroid(), Android.Graphics.PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}