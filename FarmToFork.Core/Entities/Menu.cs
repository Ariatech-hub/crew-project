using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Menu
{
    public int Id { get; set; }

    public int ParentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Url { get; set; }

    public bool IsActive { get; set; }

    public string? Icon { get; set; }

    public int? OrderId { get; set; }

    public bool? IsLink { get; set; }
}
