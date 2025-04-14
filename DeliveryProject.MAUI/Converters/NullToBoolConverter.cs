using System.Globalization;

namespace DeliveryProject.MAUI.Converters;


public class NullToBoolConverter : IValueConverter
{
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value != null;
    }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return ConvertBack(value, targetType, parameter, culture);
    }
}