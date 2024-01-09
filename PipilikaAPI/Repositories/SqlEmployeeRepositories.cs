using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PipilikaAPI.Data;
using PipilikaAPI.Models.Domain;

namespace PipilikaAPI.Repositories
{
    public class SqlEmployeeRepositories : IEmployeeRepositories
    {
        private readonly PipilikaDbContext db;

        public SqlEmployeeRepositories(PipilikaDbContext db)
        {
            this.db = db;
        }


        public async Task<Employee> CreateAsync(Employee employee)
        {
            await db.Employees.AddAsync(employee);

            await db.SaveChangesAsync();


            return employee;

        }

        public async Task<Employee?> DeleteAsync(Guid id)
        {
            var existingEmployee = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (existingEmployee == null)
            {
                return null;
            }
             db.Employees.Remove(existingEmployee);
            await db.SaveChangesAsync();
            return existingEmployee;


        }


        public async Task<List<Employee>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 12)
        {
            var employee = db.Employees.AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("EmployeeName", StringComparison.OrdinalIgnoreCase))
                {

                    employee = employee.Where(x => x.EmployeeName.Contains(filterQuery));
                }
            }

            //Sorting start
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("EmployeeName", StringComparison.OrdinalIgnoreCase))
                {
                    employee = isAscending ? employee.OrderBy(x => x.EmployeeName) : employee.OrderByDescending(x => x.EmployeeName);
                }

            }

            //pagination

            var totalRecords = await employee.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords /(double) pageSize);

            var skipResult = (pageNumber - 1) * pageSize;

            var paginatedEmployees = await employee.Skip(skipResult).Take(pageSize).ToListAsync();

         

            return paginatedEmployees;
            //var skipResult = (pageNumber - 1) * pageSize;
            //return await employee.Skip(skipResult).Take(pageSize).ToListAsync();

            //return await db.Employees.Include("Department").ToListAsync();

        }

       

        //public async Task<List<Employee>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        //{
        //    var employee = db.Employees.AsQueryable();

        //    //filtering
        //    if (string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
        //    {
        //        if (filterOn.Equals("EmployeeName",StringComparison.OrdinalIgnoreCase))
        //        {

        //            employee = employee.Where(x => x.EmployeeName.Contains(filterQuery));
        //        }
        //    }


        //    return await employee.ToListAsync();

        //    //return await db.Employees.Include("Department").ToListAsync();

        //}

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task<Employee?> UpdateAsync(Guid id, Employee employee)
        {
            var existingEmployee = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (existingEmployee == null)
            {
                return null;
            }

            existingEmployee.EmployeeName = employee.EmployeeName;
            existingEmployee.FatherName = employee.FatherName;
            existingEmployee.MotherName = employee.MotherName;
            existingEmployee.Username = employee.Username;
            existingEmployee.Email = employee.Email;
            existingEmployee.MobileNo = employee.MobileNo;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.BloodGroup = employee.BloodGroup;
            existingEmployee.UserStatus = employee.UserStatus;
            existingEmployee.Education = employee.Education;
            

            await db.SaveChangesAsync();
            return existingEmployee;

        }




    }
}
