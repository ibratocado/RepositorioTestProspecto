using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDatabase
{
    public class Prospecto
    {
        public string Id { get; set; }
        public string? Nombre { get ; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? Calle { get; set; }
        public int Numero { get; set; }
        public string? Colonia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Telefono { get; set; }
        public string? RFC { get; set; }
    }
}
