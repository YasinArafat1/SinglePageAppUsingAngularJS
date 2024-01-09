using PipilikaAPI.Models.Domain;
using System.Runtime.InteropServices;

namespace PipilikaAPI.Repositories
{
    public interface IDepartmentRepositories
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(Guid id);

        Task<Department> CreateAsync(Department department);

        Task<Department?> UpdateAsync(Guid id, Department department);

        Task<Department?> DeleteAsync(Guid id);
    }
}
