using Acr.UserDialogs;
using Boilerplate.Common.Constants;
using System.Threading.Tasks;

namespace Boilerplate.App.Services.General.Contracts
{
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel = AppConstants.DIALOG_OK);

        Task<bool> ShowConfirmDialog(string message, string title,
            string confirmCaption = AppConstants.DIALOG_YES, string cancelCaption = AppConstants.DIALOG_NO);

        void ShowToast(string message);
    }
}
