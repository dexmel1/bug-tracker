﻿using BugTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.ViewModel
{
    public partial class ProjectViewModel : BaseViewModel
    {
        ObservableCollection<Project> Projects { get; } = new();
        ObservableCollection<Employee> Employees { get; } = new();
        ObservableCollection<Ticket> Tickets { get; } = new();
        IBugService bugService;

        public ProjectViewModel(IBugService bugService)
        {
            Title = "Projects";
            this.bugService = bugService;
        }
    }
}
