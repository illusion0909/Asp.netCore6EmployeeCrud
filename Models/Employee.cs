using System.ComponentModel.DataAnnotations;

namespace BusinessWebSoftPvtLmtTaskApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email!")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must a positive number")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        [StringLength(18, ErrorMessage = "Designation can't be longer than 18 characters")]
        public string Designation { get; set; } = string.Empty;

        
    }

}

