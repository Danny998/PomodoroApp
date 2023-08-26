using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Interfaces
{
    public interface INavigationService
    {
        public void Navigate(ViewModel url);
    }
    public enum ViewModel
    {
        MainPomodoro,
        PomodoroHandling
    }
}
