﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Boilerplate.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppRootPage : MasterDetailPage
    {
        public AppRootPage()
        {
            InitializeComponent();
        }
    }
}