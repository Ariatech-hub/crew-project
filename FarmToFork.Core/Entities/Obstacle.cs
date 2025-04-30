using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Obstacle
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Code { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsSale { get; set; }

    public int OrderId { get; set; }

    public bool IsProduction { get; set; }

    public virtual ICollection<ResearchObstacle> ResearchObstacles { get; set; } = new List<ResearchObstacle>();
}
