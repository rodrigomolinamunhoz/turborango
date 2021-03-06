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
                var nomesAscendente = restaurantesXML.OrdenarPorNomeAsc();

                var todos = restaurantesXML.TodosRestaurantes();  
                
                #endregion

            #region ADD.NET
                //var connString = @"Data Source=.;Initial Catalog=TurboRango_dev; Integrated Security=True;";
                // Usuário feevale = var connString = @"Data Source=.;Initial Catalog=TurboRango_dev; Integrated Segurity=True;UID=sa;PWD=feevale";

                //var acessoAoBanco = new CarinhaQueManipulaOBanco(connString);

                //acessoAoBanco.Inserir(new Contato {
                //    Site = "www.camigoal.com.br",
                //    Telefone = "55991096010"
                //});

                //IEnumerable<Contato> contatos = acessoAoBanco.GetContatos();
            #endregion

                #region Exercícios ADO.NET

                var stringDeConexao = @"Data Source=.;Initial Catalog=TurboRango_dev; Integrated Security=True;";
           

                var restaurantes = new Restaurantes(stringDeConexao);

                restaurantes.Inserir(new Restaurante
                {
                    Nome = "Tiririca",
                    Capacidade = 50,
                    Categoria = Categoria.Fastfood,
                    Contato = new Contato
                    {
                        Site = "http://github.com/tiririca",
                        Telefone = "5555 5555"
                    },
                    Localizacao = new Localizacao
                    {
                        Bairro = "Vila Nova",
                        Logradouro = "ERS 239, 2755",
                        Latitude = -29.6646122,
                        Longitude = -51.1188255
                    }
                });


                const string nomeArquivoXML = "restaurantes.xml";

                var restaurantesEmXML = new RestaurantesXML(nomeArquivoXML);

                foreach (var atual in restaurantesEmXML.TodosRestaurantes()) {
                    restaurantes.Inserir(atual);

                }
                restaurantes.Remover(17);

                restaurantes.Atualizar(91, new Restaurante
                {
                    Nome = "TiriricaDois",
                    Capacidade = 60,
                    Categoria = Categoria.Fastfood,
                    Contato = new Contato
                    {
                        Site = "http://github.com/tiririca2",
                        Telefone = "5555 5555"
                    },
                    Localizacao = new Localizacao
                    {
                        Bairro = "Vila Nova2",
                        Logradouro = "ERS 239, 2755",
                        Latitude = -29.6646122,
                        Longitude = -51.1188254
                    }
                });

                restaurantes.Todos();

              #endregion

        }
    }
}
