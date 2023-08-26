using Avalonia.Threading;
using PomodoroApp.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PomodoroApp.ViewModels
{
    public class HandlingPomodoroViewModel : ViewModelBase
    {
        public HandlingPomodoroViewModel()
        {
                
        }
        [Reactive]
        public TimeSpan TimeSpan { get; set; } = TimeSpan.FromSeconds(Settings.Default.Timer);
        public bool IsWorkTime = true;
        public bool IsCoffeeBreak;
        public bool IsLongBreak;
        int Cycles = Settings.Default.Cycles;
        int LongBreakTime = Settings.Default.LongBreak;
        int CoffeeBreakTime = Settings.Default.CoffeeBreak;
        [Reactive]
        public bool TimerIsRunning { get; set; }

        PeriodicTimer? _timer;
        PeriodicTimer? timer
        {
            get
            {
                return _timer;
            }
            set
            {
                _timer = value;
                if (_timer == null) TimerIsRunning = false;
                else TimerIsRunning = true;
            }
        }
        private CancellationToken cancellationToken = new CancellationToken();
        private readonly INavigationService _navigationService;
        public ReactiveCommand<Unit, Unit> StopTimerCommand { get; }
        public ReactiveCommand<Unit, Unit> StartTimerCommand { get; }
        public HandlingPomodoroViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StopTimerCommand = ReactiveCommand.Create(StopTimer);
            StartTimerCommand = ReactiveCommand.CreateFromTask(async () => await StartTimer());
            RxApp.MainThreadScheduler.ScheduleAsync(async (_, __) => await StartTimer());
        }
        public void StopTimer()
        {
            timer?.Dispose();
            timer = null;
        }
        async Task StartTimer()
        {
            timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                if (TimeSpan <= TimeSpan.Zero)
                {
                    if (IsLongBreak)
                    {
                        timer.Dispose();
                        _navigationService.Navigate(ViewModel.MainPomodoro);
                    }
                    else if (IsCoffeeBreak)
                    {
                        IsCoffeeBreak = false;
                        IsWorkTime = true;
                        TimeSpan = TimeSpan.FromMinutes(Settings.Default.Timer);
                    }
                    else if (IsWorkTime)
                    {
                        IsWorkTime = false;
                        if (Cycles == 0)
                        {
                            TimeSpan = TimeSpan.FromMinutes(LongBreakTime);
                            IsLongBreak = true;
                        }
                        else
                        {
                            TimeSpan = TimeSpan.FromMinutes(CoffeeBreakTime);
                            IsCoffeeBreak = true;
                            Cycles--;
                        }
                    }
                }
                else
                    TimeSpan = TimeSpan - TimeSpan.FromSeconds(1);
            }

        }
    }
}
