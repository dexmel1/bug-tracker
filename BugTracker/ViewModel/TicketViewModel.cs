using BugTracker.Services;
using Microsoft.Maui.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BugTracker.ViewModel
{
    public partial class TicketViewModel : BaseViewModel
    {
        public ObservableCollection<Project> Projects { get; } = new();
        public ObservableCollection<Employee> Employees { get; } = new();
        public ObservableCollection<Ticket> Tickets { get; } = new();
        IBugService bugService;

        public TicketViewModel(IBugService bugService)
        {
            Title = "Tickets";
            this.bugService = bugService;
            statusCode = new string[] { "Submitted", "In Progress", "In Review", "Closed" };
            priorityCode = new int[] { 1, 2, 3 };
            GetStatus();
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        int[] priorityCode;
        [ObservableProperty]
        string[] statusCode;
        [ObservableProperty]
        int id;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        int priority;
        [ObservableProperty]
        string status;
        [ObservableProperty]
        Employee empAssign;
        [ObservableProperty]
        Project projAssign;


        [RelayCommand]
        async Task GetStatus()
        {
            if (IsBusy) return;

            var projects = await bugService.GetProject();
            if (Projects.Count != 0)
                Projects.Clear();
            foreach (var project in projects)
                Projects.Add(project);

            var employees = await bugService.GetEmployee();
            if (Employees.Count != 0)
                Employees.Clear();
            foreach (var employee in employees)
                Employees.Add(employee);

            var tickets = await bugService.GetTicket();
            if (Tickets.Count != 0)
                Tickets.Clear();
            foreach (var ticket in tickets)
                Tickets.Add(ticket);
            

            IsRefreshing = false;
            IsBusy = false;
        }

        [RelayCommand]
        async Task GetTicketAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var tickets = await bugService.GetTicket();
                if (Tickets.Count != 0)
                    Tickets.Clear();
                foreach (var ticket in tickets)
                    Tickets.Add(ticket);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Tickets: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task AddTicketAsync()
        {
            if (IsBusy)
                return;

            try
            {
                Ticket ticket = new Ticket();
                ticket.Id = Id;
                ticket.Description = Description;
                ticket.Priority = Priority;
                ticket.Status = Status;
                ticket.AssignedTo = EmpAssign.Id;
                ticket.ProjectAssign = ProjAssign.Id;
                if (Id != 0)
                {
                    ticket.Updated = DateTime.Now;
                }
                else
                {
                    ticket.Created = DateTime.Now;
                }

                IsBusy = true;
                await bugService.AddTicket(ticket);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to add Ticket!: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                Id = 0;
                Description = string.Empty;
                Priority = 0;
                Status = string.Empty;
                await GetTicketAsync();
            }
        }

        [RelayCommand]
        async Task DeleteTicketAsync()
        {
            if (IsBusy) return;

            try
            {
                if (Id == 0)
                    return;

                IsBusy = true;
                await bugService.RemoveTicekt(Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to remove Ticket!: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                Id = 0;
                Description = string.Empty;
                Priority = 0;
                Status = string.Empty;
                await GetTicketAsync();
            }
        }

        [RelayCommand]
        void GetSingleTicket(Ticket ticket)
        {
            Id = ticket.Id;
            Description = ticket.Description;
            Priority = ticket.Priority;
            Status = ticket.Status;
            EmpAssign = Employees.FirstOrDefault(e => e.Id == ticket.Id);
            ProjAssign = Projects.FirstOrDefault(p => p.Id == ticket.Id);
        }

    }
}
