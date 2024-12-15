using ApplicationProcessing.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ApplicationProcessing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            new LocalizationService();

            // Decrease delay before ToolTip appearing
            ToolTipService.InitialShowDelayProperty
                .OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(100));

            new Database.Core.Utils.TestDataProvider();

            StartupUri = new Uri("Core/Views/Windows/MainWindow.xaml", UriKind.Relative);
        }
    }
}