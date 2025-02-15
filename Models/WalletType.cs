using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models;
public partial class WalletType
{
    [Key]
    public int WalletTypeId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("WalletType")]
    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
