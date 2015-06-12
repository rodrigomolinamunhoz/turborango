using System;

namespace TurboRango.Web.Models
{
    public class AvaliacaoViewModel
    {
        public decimal Nota { get; set; }
        public decimal Media { get; set; }
        public DateTime Data { get; set; }
        public int RestauranteId { get; set; }
    }
}