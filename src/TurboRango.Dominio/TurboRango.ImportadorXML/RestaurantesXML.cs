using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TurboRango.ImportadorXML
{
    public class RestaurantesXML
    {
        public string NomeArquivo {get; private set;}

        
        /// <summary>
        /// Constrói restaurantes a partir de um numero de arquivo.
        /// </summary>
        /// <param name="nomeArquivo"></param>
        public RestaurantesXML(string nomeArquivo)
        {
            this.NomeArquivo = NomeArquivo;
        }

        public IList<string> ObterNomes()
        {
            //var resultado = new List<string>();

            //var nodos = XDocument.Load(NomeArquivo).Descendants("restaurante");

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
                from n in XDocument.Load(NomeArquivo).Descendants("restaurante")
                orderby n.Attribute("nome").Value
                select n.Attribute("nome").Value
                ).ToList();
             
        }

        public double CapacidadeMedia()
        {
            return (
                from n in XDocument.Load(NomeArquivo).Descendants("restaurante")
                select Convert.ToInt32(n.Attribute("capacidade").Value)
                ).Average();
        }

        public double CapacidadeMax()
        {
            var mad = (
                from n in XDocument.Load(NomeArquivo).Descendants("restaurante")
                select Convert.ToInt32(n.Attribute("capacidade").Value)
                );
            return mad.Max();
        }
    }
}
