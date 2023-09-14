using BugTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.ViewModel
{
    public partial class SettingsViewModel : BaseViewModel
    {
        ObservableCollection<Project> Projects { get; } = new();
        ObservableCollection<Employee> Employees { get; } = new();
        ObservableCollection<Ticket> Tickets { get; } = new();
        IBugService bugService;
        public SettingsViewModel(IBugService bugService)
        {
            Title = "Settings";
            this.bugService = bugService;

        }
    }
}
