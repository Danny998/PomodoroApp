using Avalonia;
using Avalonia.Animation;
using Avalonia.Collections;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controlz.Controls;
using Avalonia.Markup.Xaml;
using PomodoroApp.DependencyInjection;
using PomodoroApp.Extensions;
using PomodoroApp.ViewModels;
using PomodoroApp.Views;
using Splat;

namespace PomodoroApp
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            RegisterDependencies();
            Animation.RegisterAnimator<StrokeDashArrayAnimator>(prop => typeof(AvaloniaList<double>).IsAssignableFrom(prop.PropertyType));
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = GetRequiredService<MainWindowViewModel>()
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = GetRequiredService<MainViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
        private static void RegisterDependencies() => Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);
        private static T GetRequiredService<T>() => Locator.Current.GetService<T>();
    }
}