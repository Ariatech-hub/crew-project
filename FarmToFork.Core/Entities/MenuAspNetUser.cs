using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class MenuAspNetUser
{
    public int Id { get; set; }

    public string AspNetUserId { get; set; } = null!;

    public int MenuId { get; set; }
}
