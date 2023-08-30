namespace BugTracker.Services
{
    public interface IBugService
    {
        Task AddEmployee(Employee emp);
        Task AddProject(Project proj);
        Task AddTicket(Ticket ticket);
        Task<IEnumerable<Employee>> GetEmployee();
        Task<Employee> GetEmployee(int id);
        Task<IEnumerable<Project>> GetProject();
        Task<Project> GetProject(int id);
        Task<IEnumerable<Ticket>> GetTicket();
        Task<Ticket> GetTicket(int id);
        Task RemoveEmployee(int id);
        Task RemoveProject(int id);
        Task RemoveTicekt(int id);
    }
}