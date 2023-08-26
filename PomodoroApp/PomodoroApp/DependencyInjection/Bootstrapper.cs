using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.DependencyInjection
{
    public static class Bootstrapper
    {
        public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            StoresBootstrapper.RegisterStores(services, resolver);
            ServicesBootstrapper.RegisterServices(services, resolver);
            ViewModelsBootstrapper.RegisterServices(services, resolver);
        }
    }
}
