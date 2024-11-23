namespace PersonnelTracker.Helpers
{
    public class DateOnlyToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly dateOnly)
            {
                // DateOnly türünü DateTime'a dönüştürür.
                return dateOnly.ToDateTime(TimeOnly.MinValue);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // DateTime'ı DateOnly türüne dönüştürür.
                return DateOnly.FromDateTime(dateTime);
            }
            return null;
        }
    }
}