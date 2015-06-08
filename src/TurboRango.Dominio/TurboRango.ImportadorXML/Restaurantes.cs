using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Restaurantes
    {
        private string connectionString {get; set;}

        public Restaurantes(string connectionString) {
            this.connectionString = connectionString;
        }

        internal void Inserir(Restaurante restaurantes)
        {
            int idRestaurante;
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var inserirRestaurante = new SqlCommand("INSERT INTO [dbo].[Restaurante]"
                    + "([Capacidade],[Nome],[Categoria],[ContatoId],[LocalizacaoId])"
                    + "VALUES (@Capacidade, @Nome, @Categoria, @ContatoId, @LocalizacaoId)", connection))
                {
                    inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurantes.Capacidade;
                    inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurantes.Nome;
                    inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.Int).Value = restaurantes.Categoria;
                    inserirRestaurante.Parameters.Add("@ContatoId", SqlDbType.Int).Value = this.InserirContato(restaurantes.Contato);
                    inserirRestaurante.Parameters.Add("@LocalizacaoId", SqlDbType.Int).Value = this.InserirLocalizacao(restaurantes.Localizacao);

                    connection.Open();
                    idRestaurante = Convert.ToInt32(inserirRestaurante.ExecuteScalar());

                    Debug.WriteLine("Restaurante criado! ID no banco: {0}", idRestaurante);
                }
            }
        }

        internal int InserirContato(Contato contato)
        {
            int idContato;
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var inserirContato = new SqlCommand("INSERT INTO [dbo].[Contato] ([Site],[Telefone])"
                     + "VALUES (@Site, @Telefone); SELECT @@IDENTITY", connection))
                {
                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site == null ? DBNull.Value : (Object)contato.Site;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone == null ? DBNull.Value : (Object)contato.Telefone;

                    connection.Open();
                    idContato = Convert.ToInt32(inserirContato.ExecuteScalar());

                    Debug.WriteLine("Contato criado! ID no banco: {0}", idContato);
                }
                return idContato;
            }
        }

        internal int InserirLocalizacao(Localizacao localizacao)
        {
            int idLocalizacao;
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var inserirLocalizacao = new SqlCommand("INSERT INTO [dbo].[Localizacao]"
                     + "([Bairro],[Logradouro],[Latitude],[Longitude])"
                     + "VALUES (@Bairro, @Logradouro, @Latitude, @Longitude); SELECT @@IDENTITY", connection))
                {
                    inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;

                    connection.Open();
                    idLocalizacao = Convert.ToInt32(inserirLocalizacao.ExecuteScalar());

                    Debug.WriteLine("Localizacao criada! ID no banco: {0}", idLocalizacao);
                }
                return idLocalizacao;
            }           
        }
    }
}
