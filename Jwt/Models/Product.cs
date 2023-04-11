using System;
using System.Collections.Generic;

namespace Jwt.Models;

public partial class Product
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime ExDate { get; set; }

    public int Status { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
