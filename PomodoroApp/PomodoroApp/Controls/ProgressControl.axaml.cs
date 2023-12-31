using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace PomodoroApp.Controls
{
    public class ProgressControl : TemplatedControl
    {
        public static readonly StyledProperty<double> ProgressProperty = AvaloniaProperty.Register<ProgressControl, double>(nameof(Progress), 0.0);
        public double Progress
        {
            get { return GetValue(ProgressProperty); }
            set
            {
                SetValue(ProgressProperty, value);
            }
        }
        public static readonly StyledProperty<double> OldProgressProperty = AvaloniaProperty.Register<ProgressControl, double>(nameof(OldProgress), 0.0);
        public double OldProgress
        {
            get { return GetValue(OldProgressProperty); }
            set { SetValue(OldProgressProperty, value); }
        }
        public static readonly StyledProperty<bool> AnimateProperty = AvaloniaProperty.Register<ProgressControl, bool>(nameof(Animate), false);       
        public bool Animate
        {
            get { return GetValue(AnimateProperty); }
            set { SetValue(AnimateProperty, value); }
        }
        public static readonly StyledProperty<double> ThicknessProperty = AvaloniaProperty.Register<ProgressControl, double>(nameof(Thickness), 1.0);

        public double Thickness
        {
            get { return GetValue(ThicknessProperty); }
            set
            {
                SetValue(ThicknessProperty, value);
            }
        }
        public static readonly StyledProperty<double> DiameterProperty = AvaloniaProperty.Register<ProgressControl, double>(nameof(Diameter), 100.0);

        public double Diameter
        {
            get { return GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

    }
}
