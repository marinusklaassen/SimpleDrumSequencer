using SimpleDrumSequencer.Constracts.Navigation;
using SimpleDrumSequencer.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SimpleDrumSequencer.Services.Navigation
{
	public class NavigationService : INavigationService
	{  
		public async Task NavigateToAsync<TViewModel>()
		{
			var viewModelType = typeof(TViewModel);

			var viewType = Type.GetType($"{viewModelType.FullName.Replace("Model", string.Empty)}, {viewModelType.GetTypeInfo().Assembly.FullName}");

      		var page      = Activator.CreateInstance(viewType) as Page;
			var viewModel = Activator.CreateInstance(viewModelType) as ViewModelBase;

			page.BindingContext = viewModel;

			if (Application.Current.MainPage is null)
				Application.Current.MainPage = new NavigationPage(page);
			else
				await Application.Current.MainPage.Navigation.PushAsync(page, true);
		}
	}
}

