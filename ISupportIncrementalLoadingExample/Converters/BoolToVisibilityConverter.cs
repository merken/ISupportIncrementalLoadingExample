using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ISupportIncrementalLoadingExample.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool v = (bool)value;
            bool isInverted = false;
            if (parameter != null && !String.IsNullOrWhiteSpace(parameter.ToString()))
            {
                if (String.Compare("inverted", parameter.ToString()) == 0)
                {
                    isInverted = true;
                }
            }

            if (!isInverted)
            {
                if (v)
                    return Visibility.Visible;
            }
            else
                if (!v)
                    return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
