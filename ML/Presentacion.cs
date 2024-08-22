using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Presentacion
    {
        public int IdPresentacion { get; set; }
        public string NombrePresentacion { get; set; }
        public List<Presentacion> Presentaciones { get; set; }
    }
}
