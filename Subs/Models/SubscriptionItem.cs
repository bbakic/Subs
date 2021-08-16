using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class SubscriptionItem
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Subscription")]
        public int SubscriptionId { get; set; }
        [Display(Name = "Service")]
        public int ServiceId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(0, 999999.99)]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        [Range(0, 100.00)]
        [Display(Name = "Discount %")]
        public decimal DiscountPercent { get; set; }

        [ForeignKey(nameof(ServiceId))]
        [InverseProperty("SubscriptionItems")]
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(SubscriptionId))]
        [InverseProperty("SubscriptionItems")]
        public virtual Subscription Subscription { get; set; }
    }
}
