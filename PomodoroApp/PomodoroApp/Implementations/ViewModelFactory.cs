using PomodoroApp.Extensions;
using PomodoroApp.Interfaces;
using PomodoroApp.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Implementations
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly IReadonlyDependencyResolver _resolver;
        public ViewModelFactory(IReadonlyDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public ViewModelBase CreateViewModel(ViewModel viewModel)
        {
            switch (viewModel)
            {
                case ViewModel.MainPomodoro:
                    return _resolver.GetRequiredService<PomodoroMainViewModel>();
                case ViewModel.PomodoroHandling:
                    return _resolver.GetRequiredService<HandlingPomodoroViewModel>();
                default:
                    return new ViewModelBase();


            }
        }
    }
}
