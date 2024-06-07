using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PresentationLayer.Forms
{
    public class ShiftToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string shift = value.ToString();
            switch (shift)
            {
                case "07:00:00 - 15:00:00":
                    return Brushes.Yellow;
                case "15:00:00 - 23:00:00":
                    return Brushes.Blue;
                case "23:00:00 - 07:00:00":
                    return Brushes.Green;
                case "Off":
                    return Brushes.Gray;
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
