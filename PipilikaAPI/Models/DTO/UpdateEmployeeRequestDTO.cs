using PipilikaAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace PipilikaAPI.Models.DTO
{
    public class UpdateEmployeeRequestDTO
    {

        [Required(ErrorMessage = "Employee Name is Required"), Display(Name = "Employee Name")]
        public string EmployeeName { get; set; } = default!;
        [Required(ErrorMessage = "Father Name is Required"), Display(Name = "Employee Name")]
        public string FatherName { get; set; } = default!;
        [Required(ErrorMessage = "Mother Name is Required"), Display(Name = "Employee Name")]
        public string MotherName { get; set; } = default!;
        [Required(ErrorMessage = "Username is Required"), Display(Name = "Employee Name")]
        public string Username { get; set; } = default!;
        [Required(ErrorMessage = "Email  is Required"), Display(Name = "Email ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Phone NO is Required"), Display(Name = "Mobile NO")]
        public string MobileNo { get; set; } = default!;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        public Gender? Gender { get; set; }
        public BloodGroup? BloodGroup { get; set; }
        public UserStatus? UserStatus { get; set; }
        public string Education { get; set; }

        //public Guid? DepartmentId { get; set; }
    }
}
