using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Models.Entities
{
    public abstract class AuditableEntity
    {
      [Key]
      public Guid Id { get; set; } = Guid.NewGuid();
        
      [Required]
      public DateTime CreatedAt { get; set; }

      [Required]
      public DateTime UpdatedAt { get; set; }

      public Guid? CreatedBy { get; set; }
      public Guid? UpdatedBy { get; set; }

      [Required]
      public bool IsActive { get; set; } = true;
    }
}
