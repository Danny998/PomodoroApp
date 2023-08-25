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
    public static class StoresBootstrapper
    {
        public static void RegisterStores(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            RegisterCommonStores(services, resolver);
        }
        private static void RegisterCommonStores(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton(() => new ViewModelStore());
        }
    }
}
