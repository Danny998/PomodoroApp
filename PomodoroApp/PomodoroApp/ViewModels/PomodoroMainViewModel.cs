using PomodoroApp.Interfaces;
using PomodoroApp.StaticProperties;
using PomodoroApp.Stores;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PomodoroApp.ViewModels
{
    public class PomodoroMainViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> StartPomodoroCommand { get; }
        public ReactiveCommand<string, Unit> ShowPropertyToSetCommand { get; }
        public ReactiveCommand<Unit, Unit> SetPropertyCommand { get; }
        private string _currentPropertyToSet;
        public string CurrentPropertyToSet
        {
            get
            {
                return _currentPropertyToSet;
            }
            set
            {
                _currentPropertyToSet = value;
                this.RaisePropertyChanged(nameof(CurrentPropertyToSet));
                this.RaisePropertyChanged(nameof(ShowPropertySlider));
            }
        }
        public bool ShowPropertySlider => !string.IsNullOrEmpty(_currentPropertyToSet);
        public int _sliderValue;
        public int SliderValue { get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                this.RaisePropertyChanged(nameof(SliderValue));
                SetProperty();
            }
        }
        [Reactive]
        public int SliderMaximum { get; set; }
        [Reactive]
        public int SliderMinimum { get; set; }
        private readonly INavigationService _navigationService;
        public PomodoroMainViewModel(INavigationService navigationService)
        {

            _navigationService = navigationService;
            ShowPropertyToSetCommand = ReactiveCommand.Create<string>(ShowPropertyToSet);
            SetPropertyCommand = ReactiveCommand.Create(SetProperty);
            StartPomodoroCommand = ReactiveCommand.Create(StartPomodoro);
        }
        public PomodoroMainViewModel()
        {
            
        }
        public void ShowPropertyToSet(string type)
        {
            if(CurrentPropertyToSet == type)
            {
                CurrentPropertyToSet = string.Empty;
                return;
            }
            switch (type)
            {
                case PropertyType.Cycles:
                    SliderMinimum = 2;
                    SliderMaximum = 8;
                    SliderValue = Settings.Default.Cycles;
                    break;
                case PropertyType.CoffeeBreak:
                    SliderMinimum = 5;
                    SliderMaximum = 15;
                    SliderValue = Settings.Default.CoffeeBreak;
                    break;
                case PropertyType.LongBreak:
                    SliderMinimum = 15;
                    SliderMaximum = 30;
                    SliderValue = Settings.Default.LongBreak;
                    break;
                case PropertyType.Timer:
                    SliderMinimum = 5;
                    SliderMaximum = 45;
                    SliderValue = Settings.Default.Timer;
                    break;
            }
            CurrentPropertyToSet = type;
        }
        public void SetProperty()
        {
            switch (CurrentPropertyToSet)
            {
                case PropertyType.Cycles:
                    Settings.Default.Cycles = SliderValue;
                    break;
                case PropertyType.CoffeeBreak:
                    Settings.Default.CoffeeBreak = SliderValue;
                    break;
                case PropertyType.LongBreak:
                    Settings.Default.LongBreak = SliderValue;
                    break;
                case PropertyType.Timer:
                    Settings.Default.Timer = SliderValue;
                    break;
            }
            Settings.Default.Save();
        }
        public void StartPomodoro()
        {
            _navigationService.Navigate(ViewModel.PomodoroHandling);
        }
    }
}
