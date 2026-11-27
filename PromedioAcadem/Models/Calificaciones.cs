using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromedioAcadem.Models
{
    internal class Calificaciones
    {
        public decimal Unidad1 { get; set; }
        public decimal Unidad2 { get; set; }

        public decimal Unidad3 { get; set; }

        public decimal CalcularPromedio() => (Unidad1 + Unidad2 + Unidad3) / 3;
    }
}
