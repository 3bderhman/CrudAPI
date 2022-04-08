using Crud.BL.Interface;
using Crud.DAL.Database;
using Crud.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Crud.BL.Repository
{
    public class EmployeeRep : IGenaricDERep<Employee>
    {
        private readonly ApplicationContext db;

        public EmployeeRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Employee>> GetAllAsync(Expression<Func<Employee, bool>> filter = null)
        {
            if (filter == null)
                return await db.employees.Include("Department").ToListAsync();
            else
                return await db.employees.Where(filter).Include("Department").ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter = null)
        {
            return await db.employees.Where(filter).Include("Department").FirstOrDefaultAsync();
        }

        public async Task<Employee> Create(Employee obj)
        {
            await db.employees.AddAsync(obj);
            await db.SaveChangesAsync();
            return await db.employees.OrderByDescending(a => a.Id).Include("Department").FirstOrDefaultAsync();
        }
        public async Task<Employee> Update(Employee obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return await db.employees.Where(a => a.Id == obj.Id).Include("Department").FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var date = await db.employees.FindAsync(id);
            db.employees.Remove(date);
            await db.SaveChangesAsync();
        }

        
    }
}
