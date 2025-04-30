using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ProductionPlanObstacle
{
    public int Id { get; set; }

    public int ProductionPlanId { get; set; }

    public int ObstacleId { get; set; }

    public bool IsSale { get; set; }

    public bool IsProduction { get; set; }
}
