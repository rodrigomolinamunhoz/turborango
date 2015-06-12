using System;
using System.ComponentModel.DataAnnotations;
using TurboRango.Dominio;

namespace TurboRango.Web.Models
{
    public class AvaliacaoViewModel : Entidade
    {
        public decimal Nota { get; set; }
        public decimal Media { get; set; }
        public DateTime Data { get; set; }
        [Display(Name = "Restaurante")]
        public int RestauranteId { get; set; }
    }
}