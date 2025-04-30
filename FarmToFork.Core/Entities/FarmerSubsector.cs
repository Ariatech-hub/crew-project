using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class FarmerSubsector
{
    public int Id { get; set; }

    public int FarmerId { get; set; }

    public int SubsectorId { get; set; }
}
