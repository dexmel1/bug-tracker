using BugTracker.Services;
using BugTracker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.ViewModel
{
    public partial class EmployeeViewModel : BaseViewModel
    {

        string fName, lName, role;

        public string FName { get => fName; set => SetProperty(ref fName, value); }
        public string LName { get => lName; set => SetProperty(ref lName, value); }
        public string Role { get => role; set => SetProperty(ref role, value); }
        
        public ObservableCollection<Project> Projects { get; } = new();
        public ObservableCollection<Employee> Employees { get; } = new();
        public ObservableCollection<Ticket> Tickets { get; } = new();
        IBugService bugService;
        public EmployeeViewModel(IBugService bugService)
        {
            Title = "Employees";
            this.bugService = bugService;
            
    
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetEmployeeAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var employees = await bugService.GetEmployee();
                if (Employees.Count != 0)
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

        [RelayCommand]
        async Task AddEmployeeAsync()
        {
            if (IsBusy)
                return;

            try
            {
                Employee emp = new Employee();
                emp.FirstName = FName;
                emp.LastName = LName;
                emp.Role = Role;

                IsBusy = true;
                await bugService.AddEmployee(emp);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to add Employee!: {ex.Message}");
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
