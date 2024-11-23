namespace PersonnelTracker.Helpers
{
    public class GenderToBooleanConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            var stringValue = value as string;
            var parameterValue = parameter as string;

            // İki değeri karşılaştır
            return stringValue != null && stringValue.Equals(parameterValue, StringComparison.OrdinalIgnoreCase);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : null;  // true ise, parametreyi döndür, false ise null döndür
        }
    }
}