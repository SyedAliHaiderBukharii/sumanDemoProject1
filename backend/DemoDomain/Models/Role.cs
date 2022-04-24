using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Models
{
  public class Role
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
    [Display(Name = "Role Name")]
    public string Name { get; set; }
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
