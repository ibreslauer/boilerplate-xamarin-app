using Boilerplate.Common.Base;
using Boilerplate.Common.Constants;
using System;

namespace Boilerplate.Common.Models
{
    public class AppRootMenuItem : BindableBase
    {
        private string _menuText;
        private MenuItemType _menuItemType;
        private Type _viewModelToLoad;
        private bool _hasSeparator;

        public MenuItemType MenuItemType
        {
            get => _menuItemType;
            set
            {
                _menuItemType = value;
                OnPropertyChanged();
            }
        }

        public string MenuText
        {
            get => _menuText;
            set
            {
                _menuText = value;
                OnPropertyChanged();
            }
        }

        public Type ViewModelToLoad
        {
            get => _viewModelToLoad;
            set
            {
                _viewModelToLoad = value;
                OnPropertyChanged();
            }
        }

        public bool HasSeparator {
            get => _hasSeparator;
            set
            {
                _hasSeparator = value;
                OnPropertyChanged();
            }
        }
    }
}
