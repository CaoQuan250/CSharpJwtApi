using System;
using System.Collections.Generic;

namespace Jwt.Models;

public partial class Order
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long CustomerId { get; set; }

    public int Amount { get; set; }

    public decimal Price { get; set; }
    
    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
