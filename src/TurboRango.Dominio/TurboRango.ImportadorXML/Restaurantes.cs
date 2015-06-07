using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.ImportadorXML
{
    class Restaurantes
    {
        private string connectionString {get; set;}

        public Restaurantes(string connectionString) {
            this.connectionString = connectionString;
        }
    }
}
