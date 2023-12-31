﻿using BugTracker.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using DependencyAttribute = Microsoft.Maui.Controls.DependencyAttribute;

[assembly:Dependency(typeof(BugService))]
namespace BugTracker.Services
{
    public class BugService : IBugService
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

            //Created all tables here
            await db.CreateTableAsync<Employee>();
            await db.CreateTableAsync<Ticket>();
            await db.CreateTableAsync<Project>();

        }


        //Tasks for employee table
        public async Task<int> AddEmployee(Employee employee)
        {
            await Init();
            if (employee.Id != 0)
                return await db.UpdateAsync(employee);
            else
                return await db.InsertAsync(employee);
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
        public async Task<int> AddTicket(Ticket ticket)
        {
            await Init();
            if (ticket.Id != 0)
                return await db.UpdateAsync(ticket);
            else
                return await db.InsertAsync(ticket);
        }

        public async Task RemoveTicekt(int id)
        {
            await Init();

            await db.DeleteAsync<Ticket>(id);
        }

        public async Task<IEnumerable<Ticket>> GetTicket()
        {
            await Init();

            var ticket = await db.Table<Ticket>().ToListAsync();
            return ticket;
        }

        public async Task<Ticket> GetTicket(int id)
        {
            await Init();

            var ticket = await db.Table<Ticket>().FirstOrDefaultAsync(t => t.Id == id);
            return ticket;
        }

        //Task for Project Table
        public async Task<int> AddProject(Project project)
        {
            await Init();
            if (project.Id != 0)
                return await db.UpdateAsync(project);
            else
                return await db.InsertAsync(project);
        }

        public async Task RemoveProject(int id)
        {
            await Init();

            await db.DeleteAsync<Project>(id);
        }

        public async Task<IEnumerable<Project>> GetProject()
        {
            await Init();

            var project = await db.Table<Project>().ToListAsync();
            return project;
        }

        public async Task<Project> GetProject(int id)
        {
            await Init();

            var project = await db.Table<Project>().FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }
    }
}
