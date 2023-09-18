using BugTracker.Services;
using BugTracker.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.ViewModel
{
    public partial class ProjectViewModel : BaseViewModel
    {
        public ObservableCollection<Project> Projects { get; } = new();
        public ObservableCollection<Employee> Employees { get; } = new();
        public ObservableCollection<Ticket> Tickets { get; } = new();
        IBugService bugService;

        public ProjectViewModel(IBugService bugService)
        {
            Title = "Projects";
            this.bugService = bugService;

        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        int id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        Employee leadEmp;
        [ObservableProperty]
        DateTime completionDate;

        [RelayCommand]
        async Task GetStatus()
        {
            if(IsBusy) return;

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
            IsBusy= false;
        }

        [RelayCommand]
        async Task GetProjectAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var projects = await bugService.GetProject();
                if (Projects.Count != 0)
                    Projects.Clear();

                foreach (var project in projects)
                    Projects.Add(project);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Projects: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task AddProjectAsync()
        {
            if (IsBusy)
                return;

            try
            {
                Project proj = new Project();
                proj.Id = Id;
                proj.Name = Name;
                proj.Description = Description;
                proj.Lead = LeadEmp;
                proj.EstCompletion = CompletionDate;
                if(Id != 0)
                {
                    proj.Updated = DateTime.Now;
                }

                IsBusy = true;
                await bugService.AddProject(proj);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to add Project!: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                Id = 0;
                Name = string.Empty;
                Description = string.Empty;
                CompletionDate = DateTime.Now;
                await GetProjectAsync();
            }
        }

        [RelayCommand]
        void GetSingleProject(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            LeadEmp = project.Lead;
            CompletionDate = project.EstCompletion;
        }

        [RelayCommand]
        async Task DeleteProject(Project project)
        {
            if (IsBusy) return;

            try
            {
                if (Id == 0)
                    return;

                IsBusy = true;
                await bugService.RemoveProject(Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to remove Project!: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                Id = 0;
                Name = string.Empty;
                Description = string.Empty;
                CompletionDate = DateTime.Now;
                await GetProjectAsync();
            }
        }
    }
}
