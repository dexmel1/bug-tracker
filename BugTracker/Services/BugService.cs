using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Services
{
    public class BugService
    {
        SQLiteAsyncConnection db;

        async Task Init()
        {
            //if table isn't null it wont make a new one
            if (db != null)
                return;

            //creates path to db
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Tracker.db");
            db = new SQLiteAsyncConnection(databasePath);

            //Created both tables here
            await db.CreateTableAsync<Employee>();
            await db.CreateTableAsync<Ticket>();

        }
        

        //Tasks for employee table
        public async Task AddEmployee(Employee emp)
        {
            await Init();

            var id = await db.InsertAsync(emp);
        }

        public async Task RemoveEmployee(int id)
        {
            await Init();

            await db.DeleteAsync<Employee>(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            await Init();

            var employee = await db.Table<Employee>().ToListAsync();
            return employee;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            await Init();

            var employee = await db.Table<Employee>().FirstOrDefaultAsync(e => e.Id == id);
            return employee;
        }


        //Tasks for ticket Table
        public async Task AddTicket(Ticket ticket)
        {
            await Init();

            var id = await db.InsertAsync(ticket);
        }

        public async Task RemoveTicekt(int id)
        {
            await Init();

            await db.DeleteAsync<Ticket>(id);
        }

        public async Task<IEnumerable<Ticket>> GetTicket()
        {
            await Init();

            var employee = await db.Table<Ticket>().ToListAsync();
            return employee;
        }

        public async Task<Ticket> GetTicket(int id)
        {
            await Init();

            var employee = await db.Table<Ticket>().FirstOrDefaultAsync(e => e.Id == id);
            return employee;
        }

    }
}
