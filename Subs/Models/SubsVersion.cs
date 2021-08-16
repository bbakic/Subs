using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class SubsVersion
    {
        public SubsVersion()
        {
            SubsVersionItems = new HashSet<SubsVersionItem>();
        }

        [Key]
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public bool IsAfterExpiry { get; set; }

        [ForeignKey(nameof(SubscriptionId))]
        [InverseProperty("SubsVersions")]
        public virtual Subscription Subscription { get; set; }
        [InverseProperty(nameof(SubsVersionItem.SubsVersion))]
        public virtual ICollection<SubsVersionItem> SubsVersionItems { get; set; }
    }
}
