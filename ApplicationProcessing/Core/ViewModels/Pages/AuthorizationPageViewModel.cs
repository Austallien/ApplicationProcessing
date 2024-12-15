using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ApplicationProcessing.Core.ViewModels.Pages
{
    internal class AuthorizationPageViewModel : INotifyPropertyChanged
    {
        private NavigationService m_navigationService;

        private string m_login = string.Empty;

        public string Login
        {
            get => m_login;
            set
            {
                m_login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string m_password = string.Empty;

        public string Password
        {
            get => m_password;
            set
            {
                m_password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool m_rememberMe = false;

        public bool RememberMe
        {
            get => m_rememberMe;
            set
            {
                m_rememberMe = value;
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        private Visibility m_isWarningTipVisible = Visibility.Hidden;

        public Visibility IsWarningTipVisible
        {
            get => m_isWarningTipVisible;
            set
            {
                m_isWarningTipVisible = value;
                OnPropertyChanged(nameof(IsWarningTipVisible));
            }
        }

        public ICommand SignInCommand => new Command(
            async () =>
                {
                    bool result = await new Services.AuthorizationService().Authorize(Login, Password, RememberMe);

                    if (result)
                    {
                        m_navigationService.RemoveBackEntry();
                        m_navigationService.Navigate(new Core.Views.Pages.OperatorPage());
                    }
                    else
                        IsWarningTipVisible = Visibility.Visible;
                }
            );

        public AuthorizationPageViewModel(NavigationService navigationService)
        {
            m_navigationService = navigationService;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}