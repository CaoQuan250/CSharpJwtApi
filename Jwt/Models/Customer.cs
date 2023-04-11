using System;
using System.Collections.Generic;

namespace Jwt.Models;

public partial class Customer
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string Salt { get; set; }
    public string? Address { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
