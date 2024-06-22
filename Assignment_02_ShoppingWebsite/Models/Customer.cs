﻿using System;
using System.Collections.Generic;

namespace Assignment_02_ShoppingWebsite.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Password { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
