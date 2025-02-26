using Mcc.Revit.Entity;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mcc.Revit.Master.Converters
{
    public class CommandParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2)
            {
                return new CommandParameters
                {
                    UrlIdentify = values[0],
                    WebView2Control = values[1] as WebView2
                };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
