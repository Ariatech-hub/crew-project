using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? DeviceId { get; set; }
}
