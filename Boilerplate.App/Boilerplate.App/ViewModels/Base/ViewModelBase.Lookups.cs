using Boilerplate.App.Bootstrap;
using Boilerplate.App.Contracts;
using Boilerplate.Common.Base;
using System;
using System.Threading.Tasks;

namespace Boilerplate.App.ViewModels.Base
{
    public partial class ViewModelBase : BindableBase, IDisposable
    {
        public Task<TR> ShowLookupPageAsync<TViewModel, TR>() where TViewModel : ViewModelBase, ILookup<TR>
        {
            var diagResult = new TaskCompletionSource<TR>();
            new Action(async () =>
            {
                IsBusy = true;
                try
                {
                    var vm = AppContainer.Resolve<TViewModel>();
                    await NavigationService.NavigateToModalAsync(vm);
                    IsBusy = false;

                    TR returnedValue = await vm.GetValue();

                    await NavigationService.NavigateBackModalAsync();

                    diagResult.SetResult(returnedValue);
                }
                catch (Exception ex)
                {
                    await DialogService.ShowDialog(ex.Message, Title);
                    IsBusy = false;
                }
            })();
            return diagResult.Task;
        }
    }
}
