using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Compartir
{
    public class HistorialClass
    {
        public int Id { get; set; }
        
        public int? IdUser { get; set; }

        public int? Number { get; set; }

        public int? NumResp { get; set; }

        public DateTime? Fecha { get; set; }
    }
}
