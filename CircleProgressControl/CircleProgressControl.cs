using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Security.Cryptography.X509Certificates;

namespace CircleProgressControl
{
    public class CircleProgressControl : Control
    {
        public static readonly StyledProperty<TimeSpan> FullTimeSpanProperty = AvaloniaProperty.Register<CircleProgressControl, TimeSpan>(nameof(FullTimeSpan));
        public TimeSpan FullTimeSpan
        {
            get => GetValue(FullTimeSpanProperty);
            set => SetValue(FullTimeSpanProperty, value);
        }
        public static readonly StyledProperty<TimeSpan> ActualTimeSpanProperty = AvaloniaProperty.Register<CircleProgressControl, TimeSpan>(nameof(ActualTimeSpan));
        public TimeSpan ActualTimeSpan
        {
            get => GetValue(ActualTimeSpanProperty);
            set => SetValue(ActualTimeSpanProperty, value);
        }
    }

}