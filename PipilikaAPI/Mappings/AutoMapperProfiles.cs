using AutoMapper;
using PipilikaAPI.Models.Domain;
using PipilikaAPI.Models.DTO;

namespace PipilikaAPI.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Department, DepartmentDTO>().ReverseMap();

            CreateMap<AddDeptRequestDto,Department>().ReverseMap();
            CreateMap<UpdateDeptRequestDto,Department>().ReverseMap();


            CreateMap<AddEmployeeRequestDTO,Employee>().ReverseMap();

            CreateMap<Employee,EmployeeDTO>().ReverseMap();
            CreateMap<UpdateEmployeeRequestDTO,Employee>().ReverseMap();


        }


    }
}
