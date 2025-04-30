using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ProductionEvent
{
    public int Id { get; set; }

    public int ProductionId { get; set; }

    public int StageId { get; set; }

    public string Remarks { get; set; } = null!;

    public string? PhotoName { get; set; }
}
