using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PomodoroApp.ViewModels
{
    public class HandlingPomodoroViewModel : ViewModelBase
    {
        [Reactive]
        public TimeSpan TimeSpan { get; set; } = TimeSpan.FromMinutes(Settings.Default.Timer);
        public bool IsWorkTime = true;
        public bool IsCoffeeBreak;
        public bool IsLongBreak;
        int Cycles = Settings.Default.Cycles;
        int LongBreakTime = Settings.Default.LongBreak;
        int CoffeeBreakTime = Settings.Default.CoffeeBreak;
        PeriodicTimer timer;
        public HandlingPomodoroViewModel()
        {
            //TODO: START Timer
            RxApp.MainThreadScheduler.ScheduleAsync(async (_, __) => await StartTimer(__));
        }
        async Task StartTimer(CancellationToken token)
        {
            timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await timer.WaitForNextTickAsync(token))
            {
                if (TimeSpan <= TimeSpan.Zero)
                {
                    if (IsLongBreak)
                    {
                        timer.Dispose();
                        //Navigate to PomodoroMain
                    }
                    if (IsWorkTime)
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
                    if (IsCoffeeBreak)
                    {
                        IsCoffeeBreak = false;
                        IsWorkTime = true;
                        TimeSpan = TimeSpan.FromMinutes(Settings.Default.Timer);
                    }
                }
                else
                    TimeSpan = TimeSpan - TimeSpan.FromSeconds(1);
            }

        }
    }
}
