using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class Service
    {
        public Service()
        {
            ExpenseItems = new HashSet<ExpenseItem>();
            JournalItems = new HashSet<JournalItem>();
            ServiceTiers = new HashSet<ServiceTier>();
            SubsVersionItems = new HashSet<SubsVersionItem>();
            SubscriptionItems = new HashSet<SubscriptionItem>();
        }

        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public int ServiceCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey(nameof(ServiceCategoryId))]
        [InverseProperty("Services")]
        [Display(Name = "Category")]
        public virtual ServiceCategory ServiceCategory { get; set; }
        [InverseProperty(nameof(ExpenseItem.Service))]
        public virtual ICollection<ExpenseItem> ExpenseItems { get; set; }
        [InverseProperty(nameof(JournalItem.Service))]
        public virtual ICollection<JournalItem> JournalItems { get; set; }
        [InverseProperty(nameof(ServiceTier.Service))]
        public virtual ICollection<ServiceTier> ServiceTiers { get; set; }
        [InverseProperty(nameof(SubsVersionItem.Service))]
        public virtual ICollection<SubsVersionItem> SubsVersionItems { get; set; }
        [InverseProperty(nameof(SubscriptionItem.Service))]
        public virtual ICollection<SubscriptionItem> SubscriptionItems { get; set; }
    }
}
