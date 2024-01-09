using System.ComponentModel.DataAnnotations;

namespace PipilikaAPI.Models.DTO
{
    public class AddDeptRequestDto
    {
        [Required(ErrorMessage ="Dept name is Required"),Display(Name = "Department Name"),StringLength(20,MinimumLength =3)]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Short name is Required"), Display(Name = "Short Name"), StringLength(20, MinimumLength = 2)]
        public string ShortName { get; set; }
    }
}
