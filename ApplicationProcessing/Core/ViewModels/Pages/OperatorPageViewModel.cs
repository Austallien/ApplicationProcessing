using ApplicationProcessing.Core.Models;
using ApplicationProcessing.Services;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ApplicationProcessing.Core.ViewModels.Pages
{
    internal class OperatorPageViewModel
    {
        private NavigationService m_navigationService;

        private NavigationService m_parentNavigationService;

        // Summary:
        //      Authorized operator

        #region User

        private User m_user;

        public User User
        {
            get => m_user;
        }

        #endregion User

        public ICommand SignOutCommand => new Command(
            () =>
                {
                    bool result = new AuthorizationService().SignOut();
                    if (result)
                    {
                        m_parentNavigationService.RemoveBackEntry();
                        m_parentNavigationService.Navigate(new Core.Views.Pages.AuthorizationPage());
                    }
                }
            );

        public OperatorPageViewModel(NavigationService parentNavigationService, NavigationService navigationService)
        {
            m_parentNavigationService = parentNavigationService;
            m_navigationService = navigationService;
            m_user = AuthorizationService.User;

            m_navigationService.Navigate(new Core.Views.Pages.OperatorMainPage());
        }
    }
}