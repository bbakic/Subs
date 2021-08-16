using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class ServiceCategory
    {
        public ServiceCategory()
        {
            Journals = new HashSet<Journal>();
            Services = new HashSet<Service>();
            Subscriptions = new HashSet<Subscription>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name="Prepaid")]
        public bool IsPrepaid { get; set; }
        [Range(1, 999999)]
        [Display(Name = "Duration (months)")]
        public int Duration { get; set; }
        [Display(Name = "Unit Quantity")]
        [Range(1, 999999)]
        public int UnitQuantity { get; set; }

        [InverseProperty(nameof(Journal.ServiceCategory))]
        public virtual ICollection<Journal> Journals { get; set; }
        [InverseProperty(nameof(Service.ServiceCategory))]
        public virtual ICollection<Service> Services { get; set; }
        [InverseProperty(nameof(Subscription.ServiceCategory))]
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
