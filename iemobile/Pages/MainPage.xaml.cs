using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace iemobile.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
