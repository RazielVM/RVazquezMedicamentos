using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medicamento
    {
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public  Presentacion Presentacion { get; set; }
        public string Dosis { get; set; }
        public string NombreLaboratio { get; set; }
        public string FechaCaducidad { get; set; }
        public List<Medicamento> Medicamentos { get; set; }
    }
}
