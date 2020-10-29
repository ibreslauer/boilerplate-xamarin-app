using Boilerplate.App.ViewModels.Base;
using Boilerplate.Common.Contracts;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Boilerplate.App.ViewModels.Samples
{
    public class SampleLookupViewModel : ViewModelBase, ILookup<string>
    {
        private readonly TaskCompletionSource<string> _dialogResult = new TaskCompletionSource<string>();
        public SampleLookupViewModel() : base()
        { }

        private string _someValue;
        public string SomeValue
        {
            get => _someValue;
            set => SetProperty(ref _someValue, value);
        }

        public ICommand SubmitCommand => new Command(OnSubmit);

        protected override Task OnBack()
        {
            _dialogResult.SetResult(null);
            return Task.CompletedTask;
        }

        private void OnSubmit()
        {
            _dialogResult.SetResult(SomeValue);
        }

        public Task<string> GetValue()
        {
            return _dialogResult.Task;
        }

        public override Task InitializeAsync(object data)
        {
            Title = $"Lookup {PageNavigationMode} Page";
            return Task.CompletedTask;
        }
    }
}
