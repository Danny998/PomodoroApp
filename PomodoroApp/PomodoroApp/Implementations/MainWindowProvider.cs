using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls;
using PomodoroApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using PomodoroApp.ViewModels;
using PomodoroApp.Views;

namespace PomodoroApp.Implementations
{
    public class MainWindowProvider : IMainWindowProvider
    {
        public Control GetMainWindow()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var lifetime = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;

                return lifetime.MainWindow;
            }
            else if (Application.Current.ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                var lifetime = (ISingleViewApplicationLifetime)Application.Current.ApplicationLifetime;

                return lifetime.MainView;
            }
            throw new NotImplementedException();
        }
    }
}
