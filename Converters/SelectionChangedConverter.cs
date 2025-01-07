using System.Globalization;

namespace ClienteMauiFrontend.Converters
{
    public class SelectionChangedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SelectionChangedEventArgs args && args.CurrentSelection.FirstOrDefault() is Models.Cliente cliente)
            {
                return cliente;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
