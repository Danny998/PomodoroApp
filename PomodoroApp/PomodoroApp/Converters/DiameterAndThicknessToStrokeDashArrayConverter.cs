using Avalonia.Collections;
using Avalonia.Controlz.Controls;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Converters
{
    public class DiameterAndThicknessToStrokeDashArrayConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count < 3 ||
                !double.TryParse(values[0].ToString(), out double diameter) ||
                !double.TryParse(values[1].ToString(), out double thickness) ||
                !double.TryParse(values[2].ToString(), out double progress)
                )
            {
                return 0;
            }
            double circumference = Math.PI * (diameter - thickness);
            double lineLength = circumference * progress;
            double gapLength = circumference - lineLength;
            return new AvaloniaList<double>(new[] { lineLength / thickness, gapLength / thickness });
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
