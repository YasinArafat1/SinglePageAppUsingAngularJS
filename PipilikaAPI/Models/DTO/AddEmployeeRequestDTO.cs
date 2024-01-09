using PipilikaAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace PipilikaAPI.Models.DTO
{
    public class AddEmployeeRequestDTO
    {
      
        [Required]
        public string EmployeeName { get; set; } = default!;
        [Required]
        public string FatherName { get; set; } = default!;
        [Required]
        public string MotherName { get; set; } = default!;
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string Email { get; set; } = default!;
        [Required]
        public string MobileNo { get; set; } = default!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender? Gender { get; set; }
        [Required]
        public BloodGroup? BloodGroup { get; set; }
        [Required]
        public UserStatus? UserStatus { get; set; }
        [Required]
        public string Education { get; set; }

        

    }
}
