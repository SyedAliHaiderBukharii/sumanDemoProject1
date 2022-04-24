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
    [Display(Name = "Age")]
    public int? Age { get; set; }
    [Required(ErrorMessage = "Department  is required")]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
    [Display(Name = "Department")]
    public string Department { get; set; }
    [Required]
    public int? RoleId { get; set; }
 
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    
    public string DeletedByUserId { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool? IsDeleted { get; set; }
    public bool? IsActive { get; set; }

  }
}
