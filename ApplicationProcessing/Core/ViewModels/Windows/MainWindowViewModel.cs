using ApplicationProcessing.Services;
using System.Windows.Navigation;

namespace ApplicationProcessing.Core.ViewModels.Windows
{
    internal class MainWindowViewModel
    {
        public MainWindowViewModel(NavigationService navigationService)
        {
            AuthorizationService authorizationService = new AuthorizationService();

            if (authorizationService.RestoreUser())
                navigationService.Navigate(new Core.Views.Pages.OperatorPage());
            else
                navigationService.Navigate(new Core.Views.Pages.AuthorizationPage());
        }
    }
}