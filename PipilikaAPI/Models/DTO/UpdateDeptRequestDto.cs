using System.ComponentModel.DataAnnotations;

namespace PipilikaAPI.Models.DTO
{
    public class UpdateDeptRequestDto
    {
        [Required(ErrorMessage = "Dept name is Required"), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Short name is Required"), Display(Name = "Short Name")]
        public string ShortName { get; set; }
    }
}
