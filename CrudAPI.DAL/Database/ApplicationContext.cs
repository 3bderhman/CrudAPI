using Crud.DAL.Entity;
using CrudAPI.DAL.External;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.DAL.Database
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {
            
        }
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }

    }
}
