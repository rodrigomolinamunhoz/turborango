﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Exemplos
                Restaurante restaurante = new Restaurante();
                //restaurante
                Console.WriteLine(restaurante.Capacidade.HasValue ? 
                                  restaurante.Capacidade.Value.ToString() : "oi");
            
                restaurante.Nome = String.Empty + " ";

                Console.WriteLine(restaurante.Nome ?? "nulo!!");

                Console.WriteLine(!string.IsNullOrEmpty(restaurante.Nome.Trim()) ? "Tem valor" : "Não tem valor");

                var oQueEuGosto = "bacon";
                var texto = String.Format("Eu gosto de {0}", oQueEuGosto);
                //var texto = String.Format("Eu gosto de \{oQueEuGosto}");

                StringBuilder pedreiro = new StringBuilder();
                pedreiro.Append("!!!!!!!!");
                pedreiro.AppendFormat("Eu gosto de {0}", oQueEuGosto);

                //object obj = "oi";
                //int? a = obj as int?;
                //int res = 12 + a.Value;

                object obj = "1";
                int a = Convert.ToInt32(obj);
                int res = 12 + a;

                Console.WriteLine(pedreiro);
            #endregion

            #region Exercícios
                const string nomeArquivo = "restaurantes.xml";

                var restaurantesXML = new RestaurantesXML(nomeArquivo);
                var nomes  = restaurantesXML.ObterNomes();
                var capacidadeMedia = restaurantesXML.CapacidadeMedia();
                var capacidadeMaxima = restaurantesXML.CapacidadeMax();
                var porCategoria = restaurantesXML.AgruparPorCategoria();
                #endregion

            #region ADD.NET
                var connString = @"Data Source=.;Initial Catalog=TurboRango_dev; Integrated Security=True;";
                // Usuário feevale = var connString = @"Data Source=.;Initial Catalog=TurboRango_dev; Integrated Segurity=True;UID=sa;PWD=feevale";

                var acessoAoBanco = new CarinhaQueManipulaOBanco(connString);

                acessoAoBanco.Inserir(new Contato {
                    Site = "www.camigoal.com.br",
                    Telefone = "55991096010"
                });

                IEnumerable<Contato> contatos = acessoAoBanco.GetContatos();
            #endregion

        }
    }
}
