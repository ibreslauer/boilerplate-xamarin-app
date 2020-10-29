using Boilerplate.App.Views.Base;
using Xamarin.Forms.Xaml;

namespace Boilerplate.App.Views.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SamplePage : BaseContentPage
    {
        public SamplePage()
        {
            InitializeComponent();
        }
    }
}