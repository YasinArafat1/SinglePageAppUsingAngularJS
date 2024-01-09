using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PipilikaAPI.CustomActionFilets;
using PipilikaAPI.Data;
using PipilikaAPI.Models;
using PipilikaAPI.Models.Domain;
using PipilikaAPI.Models.DTO;
using PipilikaAPI.Repositories;

namespace PipilikaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly PipilikaDbContext db;
        private readonly IDepartmentRepositories departmentRepositories;
        private readonly IMapper mapper;

        public DepartmentsController(PipilikaDbContext db,IDepartmentRepositories departmentRepositories,IMapper mapper)
        {
            this.db = db;
            this.departmentRepositories = departmentRepositories;
            this.mapper = mapper;
        }

        //All Department
        //GET:https://localhost:portnumber/api/departments
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            //Get Data from Database-Domain models
            var dept = await departmentRepositories.GetAllAsync();

            //map domain models to dto
            //var departmentsDto = new List<DepartmentDTO>();
            //foreach (var department in dept)
            //{
            //    departmentsDto.Add(new DepartmentDTO()
            //    {
            //        DepartmentId = department.DepartmentId,
            //        DepartmentName = department.DepartmentName,
            //        ShortName = department.ShortName

            //    });
            //}

            //map domain models to dtos using auto mapper

          var departmentsDto=  mapper.Map<List<DepartmentDTO>>(dept);


            //return dto to client
            return Ok(departmentsDto);
        }

        //get single dept
        //GET:https://localhost:portnumber/api/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult>  GetById([FromRoute] Guid id) 
        {
            //get dept id from database
            var department = await departmentRepositories.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            //map/convert department domain model to department dto

            //var departmentsDto = new DepartmentDTO
            //{
            //    DepartmentId = department.DepartmentId,
            //    DepartmentName = department.DepartmentName,
            //    ShortName = department.ShortName
            //};
            //return dto back client


            return Ok(mapper.Map<DepartmentDTO>(department));
        }

        //post dept to db
        //post:https://localhost:portnumber/api/departments
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult>  Create([FromBody] AddDeptRequestDto addDeptRequestDto)
        {
            
                //map or convert dto to domain model

                var deptDomainmodel = mapper.Map<Department>(addDeptRequestDto);

                //var deptDomainmodel = new Department
                //{
                //    DepartmentName = addDeptRequestDto.DepartmentName,
                //    ShortName = addDeptRequestDto.ShortName

                //};

                //use domain model to create dept
                deptDomainmodel = await departmentRepositories.CreateAsync(deptDomainmodel);
                //map domain model back to dto 

                var deptdto = mapper.Map<DepartmentDTO>(deptDomainmodel);

                //var deptdto = new DepartmentDTO
                //{
                //    DepartmentId = deptDomainmodel.DepartmentId,
                //    DepartmentName = deptDomainmodel.DepartmentName,
                //    ShortName = deptDomainmodel.ShortName

                //};

                return CreatedAtAction(nameof(GetById), new { id = deptdto.DepartmentId }, deptdto);
           


           


        }


        //update dept
        //put: https://loaclhost:portno/api/departments/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult>  Update([FromRoute] Guid id, [FromBody] UpdateDeptRequestDto updateDeptRequestDto)
        {
           
                //mapt dto to domain model
                var deptDomainModel = mapper.Map<Department>(updateDeptRequestDto);

                //var deptDomainModel = new Department
                //{
                //    DepartmentName = updateDeptRequestDto.DepartmentName,
                //    ShortName = updateDeptRequestDto.ShortName

                //};


                deptDomainModel = await departmentRepositories.UpdateAsync(id, deptDomainModel);
                if (deptDomainModel == null)
                {
                    return NotFound();
                }

                //convert domain model to dto
                var deptDto = mapper.Map<DepartmentDTO>(deptDomainModel);

                //var deptDto = new DepartmentDTO
                //{ 
                //DepartmentId= deptDomainModel.DepartmentId,
                //DepartmentName= deptDomainModel.DepartmentName,
                //ShortName= deptDomainModel.ShortName,
                //};
                return Ok(deptDto);

            

           

        }


        //delte dept
        //Delete:https://loacalhost:portno/api/departments/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult>  Delete([FromRoute] Guid id)
        {

         var deptDomainModel = await departmentRepositories.DeleteAsync(id);

            if (deptDomainModel==null)
            {
                return NotFound();
            }




            //return deleted dept back

            var deptDto = mapper.Map<DepartmentDTO>(deptDomainModel);
            //var deptDto = new DepartmentDTO
            //{
            //    DepartmentId = deptDomainModel.DepartmentId,
            //    DepartmentName = deptDomainModel.DepartmentName,
            //    ShortName = deptDomainModel.ShortName,
            //};


            return Ok(deptDto);

        }

    }
}
