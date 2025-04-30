using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class MenuAccessLevel
{
    public int Id { get; set; }

    public int AccessLevelId { get; set; }

    public int MenuId { get; set; }

    public string AdminName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
