using BugTracker.View;

namespace BugTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ProjectPage), typeof(ProjectPage));
            Routing.RegisterRoute(nameof(TicketPage), typeof(TicketPage));
            Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));
        }
    }
}