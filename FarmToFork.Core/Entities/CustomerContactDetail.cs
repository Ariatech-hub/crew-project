using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class CustomerContactDetail
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }
}
