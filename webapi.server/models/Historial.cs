using System;
using System.Collections.Generic;

namespace webapi.server.models;

public partial class Historial
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? Number { get; set; }

    public int? NumResp { get; set; }

    public DateTime? Fecha { get; set; }
}
