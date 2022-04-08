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
    public class DepartmentRep : IGenaricDERep<Department>
    {
        private readonly ApplicationContext db;

        public DepartmentRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Department>> GetAllAsync(Expression<Func<Department, bool>> filter = null)
        {
            if (filter == null)
                return await db.departments.ToListAsync();
            else
                return await db.departments.Where(filter).ToListAsync();
        }

        public async Task<Department> GetByIdAsync(Expression<Func<Department, bool>> filter = null)
        {
            return await db.departments.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<Department> Create(Department obj)
        {
            await db.departments.AddAsync(obj);
            await db.SaveChangesAsync();
            return await db.departments.OrderByDescending(a => a.Id).FirstOrDefaultAsync();
        }

        public async Task<Department> Update(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return await db.departments.Where(a => a.Id == obj.Id).FirstOrDefaultAsync();
        }

        public async Task Delete(int id)
        {
            var date = await db.departments.FindAsync(id);
            db.departments.Remove(date);
            await db.SaveChangesAsync();
        }
    }
}
