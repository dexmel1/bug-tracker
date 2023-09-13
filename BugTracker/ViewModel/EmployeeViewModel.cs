using BugTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.ViewModel
{
    public partial class EmployeeViewModel : BaseViewModel
    {
        ObservableCollection<Project> Projects { get; } = new();
        ObservableCollection<Employee> Employees { get; } = new();
        ObservableCollection<Ticket> Tickets { get; } = new();
        IBugService bugService;

        public EmployeeViewModel (IBugService bugService)
        {
            Title = "Employees";
            this.bugService = bugService;
        }
    }
}
