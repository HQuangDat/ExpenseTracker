﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models;

public partial class Expense
{
    [Key]
    public int ExpenseId { get; set; }

    public int UserId { get; set; }

    public int WalletId { get; set; }

    public int? CategoryId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(50)]
    public string Type { get; set; } = "Add";

    [ForeignKey("CategoryId")]
    [InverseProperty("Expenses")]
    public virtual Category? Category { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Expenses")]
    [ValidateNever]
    public virtual User User { get; set; } = null!;

    [ForeignKey("WalletId")]
    [InverseProperty("Expenses")]
    [ValidateNever]
    public virtual Wallet Wallet { get; set; } = null!;
}
