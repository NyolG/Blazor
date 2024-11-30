using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Compartir
{
    public class Responses<T>
    {
        public bool Correcto { get; set; }

        public T? Valores { get; set; }

        public string? Mensaje { get; set; }
    }
}
