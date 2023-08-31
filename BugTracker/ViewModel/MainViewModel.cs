using BugTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        ObservableCollection<Project> projects { get; } = new();
        IBugService bugService;
        
        public MainViewModel(IBugService bugService, )
        {
            this.bugService = bugService;
            this.service = service;
            Title = "Bug Tracker";
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetProjectStatus()
        {
            var projects = await bugService.GetProject();
            int count = projects.Count();
            Console.WriteLine($"There are currently {count} projects in progress");
        }
    }
}
