using System;
using ASHWorkersAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ASHWorkersAPI.Data
{
	public class ApiDbContext : DbContext
	{
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public void AddObject(Object ts)
        {
            throw new NotImplementedException();
        }
    }
}

