using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mcc.Revit.Toolkit.UI
{
    public static class ApplicationThemeManager
    {
        private static readonly string ThemePath = "pack://application:,,,/Mcc.Revit.UI;component/Themes/";

        public static Theme CurrentTheme { get; private set; } = Theme.Light;

        public static void ApplyTheme(Theme theme)
        {
            string themeUri = theme == Theme.Dark ? $"{ThemePath}Dark.xaml" : $"{ThemePath}Light.xaml";

            ResourceDictionary newTheme = new ResourceDictionary { Source = new Uri(themeUri, UriKind.Absolute) };

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(newTheme);

            CurrentTheme = theme;
        }
    }
}
