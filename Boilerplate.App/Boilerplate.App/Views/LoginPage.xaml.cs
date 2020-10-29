using Boilerplate.App.Views.Base;
using Xamarin.Forms.Xaml;

namespace Boilerplate.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BaseContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}