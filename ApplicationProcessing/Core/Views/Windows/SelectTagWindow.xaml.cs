using Database.Core.Models;
using System.Collections.Generic;
using System.Windows;

namespace ApplicationProcessing.Core.Views.Windows
{
    /// <summary>
    /// Interaction logic for SelectTagWindow.xaml
    /// </summary>
    public partial class SelectTagWindow : Window
    {
        private List<Tag> m_selectedTags;

        public List<Tag> SelectedTags
        {
            get => m_selectedTags;
            set => m_selectedTags = value;
        }

        public SelectTagWindow(List<Tag> selectedTags)
        {
            SelectedTags = selectedTags;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new ViewModels.Windows.SelectTagWindowViewModel(
                    SelectedTags,
                    () => DialogResult = false,
                    (tags) => { SelectedTags = tags; DialogResult = true; }
                );
        }
    }
}