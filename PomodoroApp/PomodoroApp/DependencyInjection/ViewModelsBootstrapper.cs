﻿using PomodoroApp.Extensions;
using PomodoroApp.Implementations;
using PomodoroApp.Interfaces;
using PomodoroApp.Stores;
using PomodoroApp.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.DependencyInjection
{
    public static class ViewModelsBootstrapper
    {
        public static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            RegisterCommonServices(services, resolver);
        }
        private static void RegisterCommonServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton(() => new AboutViewModel());
            services.RegisterLazySingleton(() => new PomodoroMainViewModel());
            services.RegisterLazySingleton(() => new MainViewModel(resolver.GetRequiredService<ViewModelStore>(),
                resolver.GetRequiredService<PomodoroMainViewModel>()));
            services.RegisterLazySingleton(() => new MainWindowViewModel(resolver.GetRequiredService<ViewModelStore>(),
                resolver.GetRequiredService<PomodoroMainViewModel>()));
            services.RegisterLazySingleton<IMainWindowProvider>(() => new MainWindowProvider());
        }
    }
}