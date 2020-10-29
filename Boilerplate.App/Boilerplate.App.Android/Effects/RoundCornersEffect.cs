using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Essentials;

using Boilerplate.App.Effects;
using Boilerplate.App.Droid.Effects;

using Android.Graphics;
using Android.Views;
using View = Android.Views.View;

[assembly: ResolutionGroupName("BarScan")]
[assembly: ExportEffect(typeof(RoundCornersEffectDroid), nameof(RoundCornersEffect))]
namespace Boilerplate.App.Droid.Effects
{
    public class RoundCornersEffectDroid : PlatformEffect
    {
        ViewOutlineProvider _originalProvider;
        View _effectTarget;

        protected override void OnAttached()
        {
            try
            {
                PrepareContainer();
                SetCornerRadius();
            }
            catch { }
        }

        protected override void OnDetached()
        {
            if (_effectTarget != null)
            {
                _effectTarget.OutlineProvider = _originalProvider;
                _effectTarget.ClipToOutline = false;
            }
        }

        private void PrepareContainer()
        {
            _effectTarget = Container ?? Control;
            _originalProvider = _effectTarget.OutlineProvider;
            _effectTarget.ClipToOutline = true;
        }

        private void SetCornerRadius()
        {
            var cornerRadius = RoundCornersEffect.GetCornerRadius(Element) * GetDensity();
            _effectTarget.OutlineProvider = new RoundedOutlineProvider(cornerRadius);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == RoundCornersEffect.CornerRadiusProperty.PropertyName)
                SetCornerRadius();
        }

        private static float GetDensity() =>
            (float)DeviceDisplay.MainDisplayInfo.Density;

        private class RoundedOutlineProvider : ViewOutlineProvider
        {
            private readonly float _radius;

            public RoundedOutlineProvider(float radius)
            {
                _radius = radius;
            }

            public override void GetOutline(Android.Views.View view, Outline outline)
            {
                outline?.SetRoundRect(0, 0, view.Width, view.Height, _radius);
            }
        }
    }
}