using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class Logo
{
    public int LogoId { get; set; }

    public string LogoName { get; set; } = null!;

    public int? Order { get; set; }

    public string? Meta { get; set; }

    public string? Image { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
