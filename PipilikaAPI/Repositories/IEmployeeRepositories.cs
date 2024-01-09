using PipilikaAPI.Models.Domain;

namespace PipilikaAPI.Repositories
{
    public interface IEmployeeRepositories
    {
        Task<Employee>CreateAsync(Employee employee);

        //Task<List<Employee>> GetAllAsync(string? filterOn=null,string? filterQuery=null);

        Task<List<Employee>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 12);

        Task<Employee?> GetByIdAsync(Guid id);

       Task<Employee?> UpdateAsync(Guid id, Employee employee);


        Task<Employee?> DeleteAsync(Guid id);


    }
}
