using Boilerplate.App.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Boilerplate.App.Views.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class BaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as ViewModelBase;
            vm?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as ViewModelBase;
            vm?.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            var vm = BindingContext as ViewModelBase;
            var result = vm?.OnBackButtonPressed() ?? base.OnBackButtonPressed();
            return result;
        }
    }
}
