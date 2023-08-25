using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Models
{
    public class Pomodoro
    {
        public int WorkTime { get; set; }
        public int BreakTime { get; set; }
        public int LongBreakTime { get; set; }
        public int WorkCycles { get; set; }
        public int CurrentCycle { get; set; }
    }
}
