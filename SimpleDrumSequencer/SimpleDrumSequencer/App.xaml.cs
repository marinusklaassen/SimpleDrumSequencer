using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleDrumSequencer.Bootstrap;
using System.Threading.Tasks;
using SimpleDrumSequencer.Constracts.Navigation;
using SimpleDrumSequencer.ViewModels;

namespace SimpleDrumSequencer
{
    public partial class App : Application
    {   
        public App()
        {
            InitializeComponent();
            InitializeApp();
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }

        protected override async void OnStart()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.NavigateToAsync<SimpleDrumSequencerViewModel>();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
