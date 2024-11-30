using System;
using System.Collections.Generic;

namespace webapi.server.models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Pass { get; set; }
}
