using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ExcelDatum
{
    public string? BOG { get; set; }

    public double? GrainCycle { get; set; }

    public double? Kg { get; set; }

    public double? Rate { get; set; }

    public int? FarmerId { get; set; }
}
