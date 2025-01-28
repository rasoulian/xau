using Gold.Services;
using System.Globalization;

namespace Gold.Controls;

internal class NumberToCurrencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }
        if (parameter is null)
        {
            throw new ArgumentException("Culture parameter is required");
        }
        var reqCulture = new CultureInfo(parameter.ToString());

        decimal.TryParse(value.ToString(), out decimal amount);
        return amount.NumberToText(Language.Persian);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return default;
    }
}



internal class PersianDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }
        if (parameter is null)
        {
            throw new ArgumentException("Culture parameter is required");
        }
        var reqCulture = new CultureInfo(parameter.ToString());

        DateTime.TryParse(value.ToString(), out DateTime amount);
        return ServiceHelper.GetService<PersianService>().ToPersian(amount);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return null;
        }
        if (parameter is null)
        {
            throw new ArgumentException("Culture parameter is required");
        }
        string currencyString = value.ToString();
        var reqCulture = new CultureInfo(parameter.ToString());

        DateTime.TryParse(currencyString, reqCulture, out DateTime amount);
        return ServiceHelper.GetService<PersianService>().ToPersian(amount);
    }
}