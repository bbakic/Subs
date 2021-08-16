using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Subs.Models
{
    public partial class ExpenseItem
    {
        [Key]
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ServiceId { get; set; }
        [Column(TypeName = "decimal(8, 3)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(16, 2)")]
        public decimal Amount { get; set; }

        [ForeignKey(nameof(ExpenseId))]
        [InverseProperty("ExpenseItems")]
        public virtual Expense Expense { get; set; }
        [ForeignKey(nameof(ServiceId))]
        [InverseProperty("ExpenseItems")]
        public virtual Service Service { get; set; }
    }
}
