using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Models
{
  public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [StringLength(2, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Age")]
        public string age  { get; set; }
        
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        [Display(Name = "Employee Role")]
        public string RoleName { get; set; }


    }
}
