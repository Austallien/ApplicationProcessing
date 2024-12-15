using System.Windows;
using System.Windows.Controls;

namespace ApplicationProcessing.Core.Views.Pages
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Core.ViewModels.Pages.AuthorizationPageViewModel(this.NavigationService);
        }
    }
}