using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Interfaces
{
    public interface IMainWindowProvider
    {
        Control GetMainWindow();
    }
}
