using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace ApplicationProcessing.Services
{
    internal class LocalizationService
    {
        public enum Local
        {
            def,
            en_US,
            ru_RU
        }

        /// <summary>
        ///     List of supproted langauges
        /// </summary>
        private static List<CultureInfo> m_Languages = new List<CultureInfo>();

        /// <summary>
        ///     List of supproted langauges
        /// </summary>
        public static List<CultureInfo> Languages
        {
            get => m_Languages;
        }

        /// <summary>
        ///     Language changed event handler
        /// </summary>
        private static EventHandler LanguageChanged;

        public LocalizationService()
        {
            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("en-US"));
            m_Languages.Add(new CultureInfo("ru-RU"));
        }

        //TODO: Add language resource dictionary check for all key includement (does any dictionary contain all same keys as default dictionary)
        public static void SetLocalization(Local local)
        {
            string key = Local.def.ToString();

            if (local == Local.def)
                key = Local.en_US.ToString().Replace('_', '-');
            else
                key = local.ToString().Replace('_', '-');

            CultureInfo cultureInfo = new CultureInfo(key);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;

            ResourceDictionary newLanguage = new ResourceDictionary();
            newLanguage.Source = new Uri(string.Format("Resources/Language/lang.{0}.xaml", key), UriKind.Relative);

            ResourceDictionary? oldLanguage = Application.Current.Resources.MergedDictionaries.FirstOrDefault(item => item.Source != null && item.Source.OriginalString.Contains("lang."));

            if (oldLanguage != null)
                Application.Current.Resources.MergedDictionaries.Remove(oldLanguage);
            Application.Current.Resources.MergedDictionaries.Add(newLanguage);
        }

        public static CultureInfo GetLocalization()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        ///     Returns or sets current language
        /// </summary>
        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (value == System.Threading.Thread.CurrentThread.CurrentCulture)
                    return;

                System.Threading.Thread.CurrentThread.CurrentCulture = value;

                ResourceDictionary newLanguage = new ResourceDictionary();
                newLanguage.Source = new Uri(string.Format("Resources/Language/lang.{0}.xaml", value), UriKind.Relative);

                ResourceDictionary? oldLanguage = Application.Current.Resources.MergedDictionaries.FirstOrDefault(item => item.Source != null && item.Source.OriginalString.Contains("lang."));

                if (oldLanguage != null)
                    Application.Current.Resources.MergedDictionaries.Remove(oldLanguage);
                Application.Current.Resources.MergedDictionaries.Add(newLanguage);

                LanguageChanged(Application.Current, new EventArgs());
            }
        }
    }
}