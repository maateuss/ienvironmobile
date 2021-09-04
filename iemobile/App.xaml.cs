using System;
using FreshMvvm;
using iemobile.Interfaces;
using iemobile.Pages;
using iemobile.Services;
using iemobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iemobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitIoC();
            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<LoginViewModel>());
        }


        protected void InitIoC() {
            FreshIOC.Container.Register<ISensorService, SensorService>();
            FreshIOC.Container.Register<IAmbienteService, AmbienteService>();
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
