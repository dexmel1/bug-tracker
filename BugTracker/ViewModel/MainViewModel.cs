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
        public ObservableCollection<Project> Projects { get; } = new();
        public ObservableCollection<Employee> Employees { get; } = new();
        public ObservableCollection<Ticket> Tickets { get; } = new();
        IBugService bugService;
        
        public MainViewModel(IBugService bugService)
        {
            IEnumerable<Ticket> openTickets = from t in Tickets where t.IsClosed = false select t;
            IEnumerable<Ticket> closedTickets = from t in Tickets where t.IsClosed = true select t;
            Title = "Bug Tracker";
            this.bugService = bugService;

        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async  Task GetProjectStatus()
        {
            var projects = await bugService.GetProject();
            if (Projects.Count() != 0)
                Projects.Clear();
            foreach(var project in projects)
                Projects.Add(project);

            var employees = await bugService.GetEmployee();
            if(Employees.Count() != 0)
                Employees.Clear();
            foreach(var employee in employees)
                Employees.Add(employee);

            var tickets = await bugService.GetTicket();
            if(Tickets.Count() != 0)
                Tickets.Clear();
            foreach(var ticket in tickets)
                Tickets.Add(ticket);

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

        [RelayCommand]
        async Task GoToSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

    }
}
