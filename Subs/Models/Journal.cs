using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class Journal
    {
        public Journal()
        {
            Expenses = new HashSet<Expense>();
            JournalItems = new HashSet<JournalItem>();
        }

        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceCategoryId { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime ExpiryDate { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal FlatAmount { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Journals")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(ServiceCategoryId))]
        [InverseProperty("Journals")]
        public virtual ServiceCategory ServiceCategory { get; set; }
        [InverseProperty(nameof(Expense.Journal))]
        public virtual ICollection<Expense> Expenses { get; set; }
        [InverseProperty(nameof(JournalItem.Journal))]
        public virtual ICollection<JournalItem> JournalItems { get; set; }
    }
}
