using BugTracker.Services;
using BugTracker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        ObservableCollection<Project> Projects { get; } = new();
        ObservableCollection<Employee> Employees { get; } = new();
        ObservableCollection<Ticket> Tickets { get; } = new();
        IBugService bugService;
        
        public MainViewModel(IBugService bugService)
        {
            IEnumerable<Ticket> openTickets = from t in Tickets where t.IsClosed = false select t;
            IEnumerable<Ticket> closedTickets = from t in Tickets where t.IsClosed = true select t;

            Projectm = $"There are currently {Projects.Count} projects in progress";
            Employeem = $"There are currently {Employees.Count} employees working on projects at this time";
            Ticketm = $"There are currently {openTickets.Count()} tickets open at this time, {closedTickets.Count()} tickets closed at this time, and {Tickets.Count} on record";
            Title = "Bug Tracker";
            this.bugService = bugService;

        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async  Task GetProjectStatus()
        {
            var projects = await bugService.GetProject();
            foreach(var project in projects)
            {
                Projects.Add(project);
            }

            var employees = await bugService.GetEmployee();
            foreach(var employee in employees)
            {
                Employees.Add(employee);
            }

            var tickets = await bugService.GetTicket();
            foreach(var ticket in tickets)
            {
                Tickets.Add(ticket);
            }

        }

        [RelayCommand]
        async Task GoToProjects()
        {
            await Shell.Current.GoToAsync(nameof(ProjectPage));
        }
        [RelayCommand]
        async Task GoToTickets()
        {
            await Shell.Current.GoToAsync(nameof(TicketPage));
        }
        [RelayCommand]
        async Task GoToEmployees()
        {
            await Shell.Current.GoToAsync(nameof(EmployeePage));
        }


    }
}
