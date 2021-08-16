using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class Subscription
    {
        public Subscription()
        {
            SubsVersions = new HashSet<SubsVersion>();
            SubscriptionItems = new HashSet<SubscriptionItem>();
        }

        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Display(Name = "Category")]
        public int ServiceCategoryId { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        [Range(0, 30)]
        [Display(Name = "Renewal Day Of Month")]
        public int? RenewalDayOfMonth { get; set; }
        [Display(Name = "Auto-renewal")]
        public bool IsAutomaticRenewal { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(0, 999999.99)]
        [Display(Name = "Flat Ammount")]
        public decimal FlatAmount { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Subscriptions")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(ServiceCategoryId))]
        [InverseProperty("Subscriptions")]
        public virtual ServiceCategory ServiceCategory { get; set; }
        [InverseProperty(nameof(SubsVersion.Subscription))]
        public virtual ICollection<SubsVersion> SubsVersions { get; set; }
        [InverseProperty(nameof(SubscriptionItem.Subscription))]
        public virtual ICollection<SubscriptionItem> SubscriptionItems { get; set; }
    }
}
