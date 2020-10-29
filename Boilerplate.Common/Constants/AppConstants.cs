using System;

namespace Boilerplate.Common.Constants
{
    public static class AppConstants
    {
        public const string DIALOG_OK = "OK";
        public const string DIALOG_YES = "YES";
        public const string DIALOG_NO = "NO";
        public const string CANCEL_BUTTON_CAPTION = "Cancel Item";


        public const string HEADER_DEVICE_ID = "X-Device-Id";

        public static readonly TimeSpan CACHE_SLIDING_EXPIRATION = TimeSpan.FromDays(7);

        public const string MSG_SETTINGS_SAVED = "New settings saved";

        public const int MIN_PIN_LENGTH = 3;
        public const int MAX_PAGE_SIZE = 100;
        public const int MAX_DECIMAL_PLACES = 5;
        public const string USER_ID_CLAIM = "boilerplate.uid";
    }

    public enum MenuItemType
    {
        Home,
        Page,
        Settings,
        Logout
    }

    public enum PageNavigationMode
    {
        Modal,
        Modeless,
        Popup
    }
}
