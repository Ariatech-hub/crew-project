using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ProductionPlanInternetUse
{
    public int Id { get; set; }

    public int ProductionPlanId { get; set; }

    public int InternetUseId { get; set; }
}
