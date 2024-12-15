using System.Windows;
using System.Windows.Controls;

namespace ApplicationProcessing.Core.Views.Pages
{
    /// <summary>
    /// Interaction logic for OperatorMainPage.xaml
    /// </summary>
    public partial class OperatorMainPage : Page
    {
        public OperatorMainPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Core.ViewModels.Pages.OperatorMainPageViewModel(this.NavigationService);
        }
    }
}