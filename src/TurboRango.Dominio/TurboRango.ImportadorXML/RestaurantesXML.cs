using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class RestaurantesXML
    {
        public string NomeArquivo {get; private set;}

        IEnumerable<XElement> restaurantes;

        
        /// <summary>
        /// Constrói restaurantes a partir de um numero de arquivo.
        /// </summary>
        /// <param name="nomeArquivo"></param>
        public RestaurantesXML(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            restaurantes = XDocument.Load(NomeArquivo).Descendants("restaurante");
        }

        public IList<string> ObterNomes()
        {
            //var resultado = new List<string>();

            //var nodos = restaurantes;

            //foreach (var item in nodos)
            //{
            //    resultado.Add(item.Attribute("nome").Value);
            //}

            //return resultado;

            /*
            return XDocument
                            .Load(NomeArquivo)
                            .Descendants("restaurante")
                            .Select(n => n.Attribute("nome").Value)
                            .ToList();
            */

            return (
                from n in restaurantes
                orderby n.Attribute("nome").Value descending
                where Convert.ToInt32(n.Attribute("capacidade").Value) < 100
                select n.Attribute("nome").Value
            ).ToList();
        }

        public double CapacidadeMedia()
        {
            return (
                from n in restaurantes
                select Convert.ToInt32(n.Attribute("capacidade").Value)
                ).Average();
        }

        public double CapacidadeMax()
        {
            var mad = (
                from n in restaurantes
                select Convert.ToInt32(n.Attribute("capacidade").Value)
                );
            return mad.Max();
        }

        public object AgruparPorCategoria()
        {
            return (
                from n in restaurantes
                group n by n.Attribute("categoria").Value into g
                select new { Categoria = g.Key, Restaurantes = g.ToList() }
            ).ToList();
        }
    }
}
