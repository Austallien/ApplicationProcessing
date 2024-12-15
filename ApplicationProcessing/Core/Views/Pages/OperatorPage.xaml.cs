using System.Windows;
using System.Windows.Controls;

namespace ApplicationProcessing.Core.Views.Pages
{
    /// <summary>
    /// Interaction logic for ApplicationPage.xaml
    /// </summary>
    public partial class OperatorPage : Page
    {
        public OperatorPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new Core.ViewModels.Pages.OperatorPageViewModel(NavigationService, FrameWorkspace.NavigationService);
        }
    }
}