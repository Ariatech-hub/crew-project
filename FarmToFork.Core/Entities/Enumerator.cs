using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Enumerator
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Location { get; set; }

    public string? DeviceId { get; set; }

    public int? AspnetuserId { get; set; }
}
