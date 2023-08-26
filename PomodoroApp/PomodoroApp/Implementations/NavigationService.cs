using PomodoroApp.Interfaces;
using PomodoroApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Implementations
{
    public class NavigationService : INavigationService
    {
        private readonly ViewModelStore _viewModelStore;
        private readonly IViewModelFactory _viewModelFactory;
        public NavigationService(ViewModelStore viewModelStore, IViewModelFactory viewModelFactory)
        {
            _viewModelStore = viewModelStore;
            _viewModelFactory = viewModelFactory;
        }
        public void Navigate(ViewModel url)
        {
            _viewModelStore.CurrentViewModel = _viewModelFactory.CreateViewModel(url);
        }
    }
}
