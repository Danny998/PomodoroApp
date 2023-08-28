using PomodoroApp.Stores;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ViewModelStore _viewModelStore;
        public MainWindowViewModel(ViewModelStore viewModelStore, PomodoroMainViewModel mainViewModel)
        {
            _viewModelStore = viewModelStore;
            _viewModelStore.CurrentViewModelChanged += _viewModelStore_CurrentViewModelChanged;
            _viewModelStore.CurrentViewModel = mainViewModel;
        }
        public MainWindowViewModel()
        {
            
        }

        private void _viewModelStore_CurrentViewModelChanged()
        {
            this.RaisePropertyChanged(nameof(CurrentViewModel));
        }

        public ViewModelBase CurrentViewModel => _viewModelStore.CurrentViewModel;
    }
}
