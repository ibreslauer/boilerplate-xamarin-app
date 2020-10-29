using Acr.UserDialogs;
using Boilerplate.Common.Constants;
using Boilerplate.App.Services.General.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boilerplate.App.Services.General
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel = AppConstants.DIALOG_OK)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public Task<bool> ShowConfirmDialog(string message, string title, 
            string confirmCaption = AppConstants.DIALOG_YES, string cancelCaption = AppConstants.DIALOG_NO)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, confirmCaption, cancelCaption);
        }

        public void ShowToast(string message)
        {
            UserDialogs.Instance.Toast(message);
        }
    }
}
