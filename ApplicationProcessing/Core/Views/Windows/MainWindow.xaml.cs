﻿using System.Windows;

namespace ApplicationProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FrameWorkspace.DataContext = new Core.ViewModels.Windows.MainWindowViewModel(FrameWorkspace.NavigationService);
        }
    }
}