namespace TurboRango.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TurboRango.Dominio;

    internal sealed class Configuration : DbMigrationsConfiguration<TurboRango.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TurboRango.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(TurboRango.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Restaurantes.AddOrUpdate(
                  p => p.Nome, new Restaurante
            {
                Nome = "Tiririca",
                Capacidade = 50,
                Categoria = Categoria.FastFood,
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
            },
            new Restaurante
            {
                Nome = "GARFÃO RESTAURANTE E PIZZARIA",
                Capacidade = 100,
                Categoria = Categoria.Comum,
                Contato = new Contato
                {
                    Site = "www.garfao.com",
                    Telefone = "(51) 3587-7700"
                },
                Localizacao = new Localizacao
                {
                    Bairro = "Liberdade",
                    Logradouro = "Rua Sete de Setembro, 1045 - Liberdade",
                    Latitude = -29.712571,
                    Longitude = -51.13636
                }
            },
            new Restaurante
            {
                Nome = "CHURRASCARIA PRIMAVERA",
                Capacidade = 110,
                Categoria = Categoria.Churrascaria,
                Contato = new Contato
                {
                    Site = "www.grupoprimaveranh.com.br",
                    Telefone = "3595-8081"
                },
                Localizacao = new Localizacao
                {
                    Bairro = "Primavera",
                    Logradouro = "BR 116, 2554, Km 31",
                    Latitude = -29.693741,
                    Longitude = -51.144554
                }
            },
            new Restaurante
            {
                Nome = "RESTAURANTE HABITUÉ",
                Capacidade = 90,
                Categoria = Categoria.Comum,
                Contato = new Contato
                {
                    Site = "www.habitue.com.br",
                    Telefone = "35931602"
                },
                Localizacao = new Localizacao
                {
                    Bairro = "Centro",
                    Logradouro = "Joaquim Nabuco, 1117",
                    Latitude = -29.6829086,
                    Longitude = -51.1263546
                }
            },
            new Restaurante
            {
                Nome = "PICA-PAU LANCHES",
                Capacidade = 30,
                Categoria = Categoria.FastFood,
                Contato = new Contato
                {
                    Site = "www.picapaulanches.com",
                    Telefone = "(51) 3593-8079"
                },
                Localizacao = new Localizacao
                {
                    Bairro = "Rio Branco",
                    Logradouro = "Rua: José do Patrocínio, 880",
                    Latitude = -29.68339,
                    Longitude = -51.135376
                }
            },
            new Restaurante
            {
                Nome = "ADAMS PIZZARIA",
                Capacidade = 130,
                Categoria = Categoria.Pizzaria,
                Contato = new Contato
                {
                    Site = "www.admspizzaria.com.br",
                    Telefone = "35823000"
                },
                Localizacao = new Localizacao
                {
                    Bairro = "Industrial",
                    Logradouro = "Avenida Pedro Adams Filho, 4466",
                    Latitude = -29.6933741,
                    Longitude = -51.1283531
                }
            }, new Restaurante
                {
                    Nome = "OLÉ ARMAZÉM MEXICANO",
                    Capacidade = 30,
                    Categoria = Categoria.CozinhaMexicana,
                    Contato = new Contato
                {
                    Site = "",
                    Telefone = "3279-8828"
                },
                    Localizacao = new Localizacao
                    {
                        Bairro = "Centro",
                        Logradouro = "Rua Gomes Portinho, 448",
                        Latitude = -29.682078,
                        Longitude = -51.125199
                    }
                });
        }
    }
}
