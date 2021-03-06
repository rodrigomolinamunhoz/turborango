﻿using System;

namespace TurboRango.Dominio
{
    public class Avaliacao : Entidade
    {
        public decimal Nota { get; set; }
        public decimal Media { get; set; }
        public DateTime Data { get; set; }
        public virtual Restaurante Restaurante { get; set; }
    }
}
