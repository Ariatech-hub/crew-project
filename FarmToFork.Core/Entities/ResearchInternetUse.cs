using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ResearchInternetUse
{
    public int Id { get; set; }

    public int ResearchId { get; set; }

    public int UseOfInternetId { get; set; }

    public virtual Research Research { get; set; } = null!;

    public virtual InternetUse UseOfInternet { get; set; } = null!;
}
