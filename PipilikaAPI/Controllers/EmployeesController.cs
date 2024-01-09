using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PipilikaAPI.CustomActionFilets;
using PipilikaAPI.Models.Domain;
using PipilikaAPI.Models.DTO;
using PipilikaAPI.Repositories;
using PipilikaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PipilikaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEmployeeRepositories employeeRepositories;

        public EmployeesController(IMapper mapper, IEmployeeRepositories employeeRepositories)
        {
            this.mapper = mapper;
            this.employeeRepositories = employeeRepositories;
        }



        //Create employee
        //post:api/employees
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create(AddEmployeeRequestDTO addEmployeeRequestDTO)
        {

           
                //Map DTO to Domain Model
                var employeeDomainModel = mapper.Map<Employee>(addEmployeeRequestDTO);
      
            await employeeRepositories.CreateAsync(employeeDomainModel);

                //Map domain to Dto


                return Ok(mapper.Map<EmployeeDTO>(employeeDomainModel));
           
        }


        //GET employees
        //GET: /api/employees?filterOn=EmployeeName&filterQuery=Track

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 12)
        {
            

            var employeeDomainModel = await employeeRepositories.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //map domain model to dto 

            return Ok(mapper.Map<List<EmployeeDTO>>(employeeDomainModel));

        }

       
        //public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery )
        //{
        //    var employeeDomainModel = await employeeRepositories.GetAllAsync();

        //    //map domain model to dto 

        //    return Ok(mapper.Map<List<EmployeeDTO>>(employeeDomainModel));

        //}
        









        //Get Employee by Id
        //Get:/api/Employee/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var EmployeeDomainModel = await employeeRepositories.GetByIdAsync(id);
            if (EmployeeDomainModel == null)
            {
                return NotFound();
            }

            //map domain model to dto 
            return Ok(mapper.Map<EmployeeDTO>(EmployeeDomainModel));

        }

        //this is update by Id
        //PUT Verb:/api/Employee/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateEmployeeRequestDTO updateEmployeeRequestDTO)
        {
           
                //Map dto to domain model

                var employeeDomainModel = mapper.Map<Employee>(updateEmployeeRequestDTO);

                employeeDomainModel = await employeeRepositories.UpdateAsync(id, employeeDomainModel);

                if (employeeDomainModel == null)
                {
                    return NotFound();
                }

                //map domain model to dto

                return Ok(mapper.Map<EmployeeDTO>(employeeDomainModel));
           

        }



        //delete a employee by id
        //Delete:/api/employees/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        { 
        
        var deletedEmpDomainModel =  await employeeRepositories.DeleteAsync(id);

            if (deletedEmpDomainModel==null)
            {
                return NotFound();
            }
            //map domain model to dto 
            return Ok(mapper.Map<EmployeeDTO>(deletedEmpDomainModel));
        
        }





    }

}
