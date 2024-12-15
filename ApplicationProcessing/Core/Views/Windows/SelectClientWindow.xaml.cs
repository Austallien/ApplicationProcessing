using Database.Core.Models;
using System.Windows;

namespace ApplicationProcessing.Core.Views.Windows
{
    /// <summary>
    /// Interaction logic for SelectClientWindow.xaml
    /// </summary>
    public partial class SelectClientWindow : Window
    {
        private Person m_client;

        public Person Client
        {
            get => m_client;
        }

        public SelectClientWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new Core.ViewModels.Windows.SelectClientWindowViewModel(
                    (client) => { m_client = client; DialogResult = true; },
                    () => DialogResult = false
                );
        }
    }
}