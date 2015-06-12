using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Avaliacao : Entidade
    {
        public decimal Nota { get; set; }
        public decimal Media { get; set; }
        public DateTime Data { get; set; }
        public int IdRestaurante { get; set; }
        public virtual Restaurante Restaurante { get; set; }
    }
}
