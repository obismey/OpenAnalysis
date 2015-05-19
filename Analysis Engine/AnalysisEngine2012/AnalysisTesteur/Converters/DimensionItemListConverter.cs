using System;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;

namespace AnalysisTesteur.Converters
{
    public class DimensionItemListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                var items = value as IEnumerable<Analysis.DimensionItem>;

                return items== null  ? "" : string.Join(Environment.NewLine, from v in items select v.Value);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
