using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class Expense
    {
        public Expense()
        {
            ExpenseItems = new HashSet<ExpenseItem>();
        }

        [Key]
        public int Id { get; set; }
        public int JournalId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        [ForeignKey(nameof(JournalId))]
        [InverseProperty("Expenses")]
        public virtual Journal Journal { get; set; }
        [InverseProperty(nameof(ExpenseItem.Expense))]
        public virtual ICollection<ExpenseItem> ExpenseItems { get; set; }
    }
}
