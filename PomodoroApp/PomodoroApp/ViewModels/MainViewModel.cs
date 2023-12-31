﻿using PomodoroApp.Stores;
using ReactiveUI;

namespace PomodoroApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";
        private readonly ViewModelStore _viewModelStore;
        public MainViewModel(ViewModelStore viewModelStore, PomodoroMainViewModel mainViewModel)
        {
            _viewModelStore = viewModelStore;
            _viewModelStore.CurrentViewModelChanged += _viewModelStore_CurrentViewModelChanged;
            _viewModelStore.CurrentViewModel = mainViewModel;
        }

        private void _viewModelStore_CurrentViewModelChanged()
        {
            this.RaisePropertyChanged(nameof(CurrentViewModel));
        }

        public ViewModelBase CurrentViewModel => _viewModelStore.CurrentViewModel;
    }
}