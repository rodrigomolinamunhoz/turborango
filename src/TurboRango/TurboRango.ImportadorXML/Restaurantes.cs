using System;
using System.Data;
using System.Data.SqlClient;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class Restaurantes
    {
        readonly static string INSERT_SQL = "INSERT INTO [dbo].[Restaurante] ([Capacidade],[Nome],[Categoria],[ContatoId],[LocalizacaoId]) VALUES (@Capacidade, @Nome, @Categoria, @ContatoId, @LocalizacaoId);";

        string ConnectionString { get; set; }
        Contatos ListaContatos { get; set; }
        Localizacoes ListaLocalizacoes { get; set; }

        public Restaurantes(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.ListaContatos = new Contatos(connectionString);
            this.ListaLocalizacoes = new Localizacoes(connectionString);
        }

        internal void Inserir(Restaurante restaurante)
        {
            int novoContato = ListaContatos.Inserir(restaurante.Contato);
            int novaLocalizacao = ListaLocalizacoes.Inserir(restaurante.Localizacao);
            ExecutarSQLInsert(restaurante, novoContato, novaLocalizacao);
        }

        void ExecutarSQLInsert(Restaurante restaurante, int fkNovoContato, int fkNovaLocalizacao)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var inserirRestaurante = new SqlCommand(INSERT_SQL, connection))
                {
                    inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                    inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria.ToString();
                    inserirRestaurante.Parameters.Add("@ContatoId", SqlDbType.Int).Value = fkNovoContato;
                    inserirRestaurante.Parameters.Add("@LocalizacaoId", SqlDbType.Int).Value = fkNovaLocalizacao;
                    connection.Open();
                    inserirRestaurante.ExecuteNonQuery();
                }
            }
        }

        // Abaixo seguem dois exemplos de "Classes aninhadas".
        // Elas foram criadas como aninhadas pois não queremos acessá-las de fora da classe Restaurante, mas queremos utilizá-las como objetos dentro da classe Restaurante.
        // Documentação: https://msdn.microsoft.com/pt-br/library/ms173120.aspx
        class Contatos
        {
            readonly static string INSERT_SQL = "INSERT INTO [dbo].[Contato] ([Site],[Telefone]) VALUES (@Site, @Telefone); SELECT @@IDENTITY";
            string ConnectionString { get; set; }

            internal Contatos(string connString)
            {
                ConnectionString = connString;
            }

            internal int Inserir(Contato contato)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var inserirContato = new SqlCommand(INSERT_SQL, connection))
                    {
                        inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site;
                        inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone;

                        connection.Open();
                        return Convert.ToInt32(inserirContato.ExecuteScalar());
                    }
                }
            }
        }

        class Localizacoes
        {
            readonly static string INSERT_SQL = "INSERT INTO [dbo].[Localizacao] ([Bairro],[Logradouro],[Latitude],[Longitude]) VALUES (@Bairro, @Logradouro, @Latitude, @Longitude); SELECT @@IDENTITY";
            string ConnectionString { get; set; }

            internal Localizacoes(string connString)
            {
                ConnectionString = connString;
            }

            internal int Inserir(Localizacao localizacao)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var inserirLocalizacao = new SqlCommand(INSERT_SQL, connection))
                    {
                        inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                        inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                        inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                        inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;
                        connection.Open();
                        return Convert.ToInt32(inserirLocalizacao.ExecuteScalar());
                    }
                }
            }
        }
    }
}
