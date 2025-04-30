using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class AuditLogin
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime LogInTime { get; set; }
}
