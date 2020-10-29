using Boilerplate.App.ViewModels.Base;
using Boilerplate.Common.Constants;
using System.Threading.Tasks;

namespace Boilerplate.App.ViewModels.Samples
{
    public class SampleViewModel : ViewModelBase
    {
        public SampleViewModel() : base()
        { }

        public bool IsModeless => PageNavigationMode == PageNavigationMode.Modeless;

        private void RefreshPage()
        {
            Title = $"{PageNavigationMode} Page";
            BackNavigationEnabled = PageNavigationMode != PageNavigationMode.Modal;
            OnPropertyChanged(nameof(IsModeless));
        }

        public override Task InitializeAsync(object data)
        {
            RefreshPage();
            return Task.CompletedTask;
        }
    }
}
