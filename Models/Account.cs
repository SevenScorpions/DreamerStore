using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Username { get; set; }

    public byte[]? Password { get; set; }
}
