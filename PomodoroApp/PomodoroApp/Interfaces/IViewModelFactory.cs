using PomodoroApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroApp.Interfaces
{
    public interface IViewModelFactory
    {
        public ViewModelBase CreateViewModel(ViewModel viewModel);
    }
}
