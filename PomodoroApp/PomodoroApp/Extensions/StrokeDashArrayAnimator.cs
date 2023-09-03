using Avalonia.Animation.Animators;
using Avalonia.Collections;
using Avalonia.Controlz.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Extensions
{
    public class StrokeDashArrayAnimator : Animator<AvaloniaList<double>>
    {
        public override AvaloniaList<double> Interpolate(double progress, AvaloniaList<double> oldValue, AvaloniaList<double> newValue)
        {
            var items = oldValue.ToList();
            var prop = new AvaloniaList<double> { items[0], items[1] };
            var val = progress * (newValue[0] - oldValue[0]);
            prop[0] = oldValue[0] + val;
            return prop;
        }
    }
}
