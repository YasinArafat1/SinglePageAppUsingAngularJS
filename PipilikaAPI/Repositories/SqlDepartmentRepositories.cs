using Microsoft.EntityFrameworkCore;
using PipilikaAPI.Data;
using PipilikaAPI.Models.Domain;

namespace PipilikaAPI.Repositories
{
    public class SqlDepartmentRepositories : IDepartmentRepositories
    {
        private readonly PipilikaDbContext db;

        public SqlDepartmentRepositories(PipilikaDbContext db)
        {
            this.db = db;
        }

        public async Task<Department> CreateAsync(Department department)
        {
            await db.Departments.AddAsync(department);
            await db.SaveChangesAsync();
            return department;
        }

        public async Task<Department?> DeleteAsync(Guid id)
        {
            var existingDept = await db.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
            if (existingDept == null)
            {
                return null;
            }

            db.Departments.Remove(existingDept);
            await db.SaveChangesAsync();
            return existingDept;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await db.Departments.ToListAsync();

        }

        public async Task<Department?> GetByIdAsync(Guid id)
        {
            return await db.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
        }

        public async Task<Department?> UpdateAsync(Guid id, Department department)
        {
            var existDept = await db.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
            if (existDept == null)
            {
                return null;
            }
            existDept.DepartmentName = department.DepartmentName;
            existDept.ShortName = department.ShortName;

            await db.SaveChangesAsync();
            return existDept;
        }
    }
}
