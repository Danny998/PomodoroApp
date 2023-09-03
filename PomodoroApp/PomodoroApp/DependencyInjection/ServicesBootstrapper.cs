using PomodoroApp.Extensions;
using PomodoroApp.Implementations;
using PomodoroApp.Interfaces;
using PomodoroApp.Stores;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.DependencyInjection
{
    public static class ServicesBootstrapper
    {
        public static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            RegisterCommonServices(services, resolver);
        }
        private static void RegisterCommonServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.Register(() => new ViewModelFactory(resolver), typeof(IViewModelFactory));
            services.RegisterConstant(new NavigationService(resolver.GetRequiredService<ViewModelStore>(), resolver.GetRequiredService<IViewModelFactory>()), typeof(INavigationService));
            services.RegisterConstant(new SoundEffectService(), typeof(ISoundEffectService));
        }
    }
}
