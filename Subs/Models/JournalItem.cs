using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class JournalItem
    {
        [Key]
        public int Id { get; set; }
        public int JournalId { get; set; }
        public int ServiceId { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(8, 3)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        public decimal DiscountPercent { get; set; }

        [ForeignKey(nameof(JournalId))]
        [InverseProperty("JournalItems")]
        public virtual Journal Journal { get; set; }
        [ForeignKey(nameof(ServiceId))]
        [InverseProperty("JournalItems")]
        public virtual Service Service { get; set; }
    }
}
