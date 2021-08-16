using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class SubsVersionItem
    {
        [Key]
        public int Id { get; set; }
        public int SubsVersionId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        public decimal DiscountPercent { get; set; }

        [ForeignKey(nameof(ServiceId))]
        [InverseProperty("SubsVersionItems")]
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(SubsVersionId))]
        [InverseProperty("SubsVersionItems")]
        public virtual SubsVersion SubsVersion { get; set; }
    }
}
