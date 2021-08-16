using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class ServiceTier
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Service")]
        public int ServiceId { get; set; }
        [Range(1, 999999)]
        [Display(Name = "Min Quantity")]
        public int MinQuantity { get; set; }
        [Range(1, 999999)]
        [Display(Name = "Max Quantity")]
        public int? MaxQuantity { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(0, 999999.99)] 
        public decimal Price { get; set; }

        [ForeignKey(nameof(ServiceId))]
        [InverseProperty("ServiceTiers")]
        public virtual Service Service { get; set; }
    }
}
