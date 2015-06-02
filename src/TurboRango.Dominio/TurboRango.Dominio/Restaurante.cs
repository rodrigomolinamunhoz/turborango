﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    internal class Restaurante
    {
        internal int Capacidade { get; set; }
        internal string Nome { get; set; }
        internal Categoria Categoria { get; set; }
        internal Contato Contato { get; set; }
        internal Localizacao Localizacao { get; set; }
    }
}
