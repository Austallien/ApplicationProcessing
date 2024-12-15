using System.Windows;
using System.Windows.Controls;

namespace ApplicationProcessing.Core.Views.Pages
{
    /// <summary>
    /// Interaction logic for OperatorApplicationPage.xaml
    /// </summary>
    public partial class OperatorApplicationPage : Page
    {
        private Database.Core.Models.Application m_application;

        public OperatorApplicationPage(Database.Core.Models.Application application)
        {
            InitializeComponent();

            m_application = application;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Core.ViewModels.Pages.OperatorApplicationPageViewModel(this.NavigationService, m_application);
        }

        private void TextBoxContent_SelectionChanged(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (TextBoxContent.SelectionLength > 0)
                TextBoxContent.SelectionLength = 0;
        }
    }
}