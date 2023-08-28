﻿using Avalonia.Controls;
using Avalonia.Threading;
using PomodoroApp.Controls;
using PomodoroApp.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
            for (int i = 1; i <= Cycles; i++)
            {
                var c = new CycleControl();
                c.Number = i;
                if (i == 1)
                {

                    c.Ellipse.Classes.Add("Animate");
                    var converter = new Avalonia.Media.BrushConverter();
                    // var brush = (Avalonia.Media.Brush)converter.ConvertFromInvariantString("#BB4949");
                    // c.Ellipse.Fill = brush;

                }
                CycleControl.Add(c);
            }
        }
        [Reactive]
        public TimeSpan TimeSpan { get; set; } = TimeSpan.FromSeconds(Settings.Default.Timer);
        public bool IsWorkTime = true;
        public bool IsCoffeeBreak;
        public bool IsLongBreak;
        int Cycles = Settings.Default.Cycles;
        int LongBreakTime = Settings.Default.LongBreak;
        int CoffeeBreakTime = Settings.Default.CoffeeBreak;
        public int CurrentCycle = 1;
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
        public ReactiveCommand<Unit, Unit> TimerCommand { get; }
        public HandlingPomodoroViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StopTimerCommand = ReactiveCommand.Create(StopTimer);
            StartTimerCommand = ReactiveCommand.CreateFromTask(async () => await StartTimer());
            TimerCommand = ReactiveCommand.Create(TimerMethod);
            RxApp.MainThreadScheduler.ScheduleAsync(async (_, __) => await StartTimer());
            for (int i = 1; i <= Cycles; i++)
            {
                var c = new CycleControl();
                c.Number = i;
                if (i == 1)
                {

                    MakeCycleControlAnimate(c);
                   
                }
                CycleControl.Add(c);
            }
        }
        public void TimerMethod()
        {
            if (timer != null)
            {
                timer?.Dispose();
                timer = null;
            }
            else
            {
                StartTimer();
            }
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
                        var control = CycleControl.Where(s => s.Number == CurrentCycle).FirstOrDefault();
                        if (control != null)
                        {
                            MakeCycleControlAnimate(control);
                        }
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
                            var control = CycleControl.Where(s => s.Number == CurrentCycle).FirstOrDefault();
                            if(control != null)
                            {
                                RemoveCycleControlAnimate(control);
                            }
                            CurrentCycle++;
                        }
                    }
                }
                else
                    TimeSpan = TimeSpan - TimeSpan.FromSeconds(1);
            }

        }
        private ObservableCollection<CycleControl> _cycleControl = new ObservableCollection<CycleControl>();
        public ObservableCollection<CycleControl> CycleControl
        {
            get { return _cycleControl; }
            set
            {
                _cycleControl = value;
                this.RaisePropertyChanged(nameof(CycleControl));
            }
        }
        void MakeCycleControlAnimate(CycleControl cycleControl)
        {
            cycleControl.Ellipse.Classes.Add("Animate");
            var converter = new Avalonia.Media.BrushConverter();
            // var brush = (Avalonia.Media.Brush)converter.ConvertFromInvariantString("#BB4949");
            // c.Ellipse.Fill = brush;
        }
        void RemoveCycleControlAnimate(CycleControl cycleControl)
        {
            cycleControl.Ellipse.Fill = Avalonia.Media.Brushes.ForestGreen;
            cycleControl.Ellipse.Classes.Remove("Animate");
        }
    }
}
