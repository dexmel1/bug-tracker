using BugTracker.Services;
using BugTracker.View;
using System;
using System.Collections.Generic;
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
        async Task GetEmployeeAsync()
        {
            if(IsBusy) 
                return;

            try
            {
                IsBusy = true;
                var employees = await bugService.GetEmployee();
                if(Employees.Count != 0)
                    Employees.Clear();
                foreach (var employee in employees)
                    Employees.Add(employee);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Employees!: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
