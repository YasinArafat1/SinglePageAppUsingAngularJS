using System.ComponentModel.DataAnnotations;

namespace PipilikaAPI.Models.Domain
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        [Required]

        public string EmployeeName { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobileNo { get; set; }
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

    // Enums
    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum BloodGroup
    {
        APositive,
        ANegative,
        BPositive,
        BNegative,
        ABPositive,
        ABNegative,
        OPositive,
        ONegative
    }

    public enum UserStatus
    {
        Active,
        Inactive
    }

}
