using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Journals = new HashSet<Journal>();
            Subscriptions = new HashSet<Subscription>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [StringLength(10)]
        public string Postcode { get; set; }
        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(11)]
        public string Oib { get; set; }

        [InverseProperty(nameof(Journal.Customer))]
        public virtual ICollection<Journal> Journals { get; set; }
        [InverseProperty(nameof(Subscription.Customer))]
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
