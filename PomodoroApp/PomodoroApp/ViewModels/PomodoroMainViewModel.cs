using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.ViewModels
{
    public class PomodoroMainViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> StartPomodoroCommand { get; }
        public PomodoroMainViewModel()
        {
            
        }
    }
}
