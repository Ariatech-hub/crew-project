using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ResearchObstacle
{
    public int Id { get; set; }

    public int ResearchId { get; set; }

    public int ObstacleId { get; set; }

    public bool IsProduction { get; set; }

    public bool IsSale { get; set; }

    public virtual Obstacle Obstacle { get; set; } = null!;

    public virtual Research Research { get; set; } = null!;
}
