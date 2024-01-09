using PipilikaAPI.Models.Domain;

namespace PipilikaAPI.Models.DTO
{
    public class EmployeeDTO
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public BloodGroup? BloodGroup { get; set; }
        public UserStatus? UserStatus { get; set; }
        public string Education { get; set; }

     
       


    }
}
