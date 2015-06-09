using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;
using TurboRango.Dominio.Utils;

namespace TurboRango.ImportadorXML
{
    class Restaurantes
    {
        private string connectionString { get; set; }

        public Restaurantes(string connectionString)
        {
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

        public void Remover(int id)
        {
            {
                int idLocalizacao = ObterIdLocalizacao(id);
                int idContato = ObterIdContato(id);
                using (var connection = new SqlConnection(this.connectionString))
                {
                    using (var deletarRestaurante = new SqlCommand("DELETE FROM [dbo].[Restaurante] WHERE [Id]=@Id", connection))
                    {
                        deletarRestaurante.Parameters.AddWithValue("@Id", id);
                        connection.Open();
                        id = Convert.ToInt32(deletarRestaurante.ExecuteScalar());
                    }

                    using (var deletarLocalizacao = new SqlCommand("DELETE FROM [dbo].[Localizacao] WHERE [Id]=@Id", connection))
                    {
                        deletarLocalizacao.Parameters.AddWithValue("@Id", idLocalizacao);
                        idLocalizacao = Convert.ToInt32(deletarLocalizacao.ExecuteScalar());
                    }

                    using (var deletarContato = new SqlCommand("DELETE FROM [dbo].[Contato] WHERE [Id]=@Id", connection))
                    {
                        deletarContato.Parameters.AddWithValue("@Id", idContato);
                        idLocalizacao = Convert.ToInt32(deletarContato.ExecuteScalar());
                    }
                }
            }
        }


        private int ObterIdContato(int idRestaurante)
        {
            int resultado;
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var obterContato = new SqlCommand("SELECT [ContatoId] FROM [dbo].[Restaurante] WHERE [Id]=@Id; SELECT @@IDENTITY", connection))
                {
                    obterContato.Parameters.AddWithValue("@Id", idRestaurante);
                    connection.Open();
                    resultado = Convert.ToInt32(obterContato.ExecuteScalar());
                }
                return resultado;
            }
        }

        private int ObterIdLocalizacao(int idRestaurante)
        {
            int resultado;
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var obterLocalizacao = new SqlCommand("SELECT [LocalizacaoId] FROM [dbo].[Restaurante] WHERE [Id]=@Id; SELECT @@IDENTITY", connection))
                {
                    obterLocalizacao.Parameters.AddWithValue("@Id", idRestaurante);
                    connection.Open();
                    resultado = Convert.ToInt32(obterLocalizacao.ExecuteScalar());
                }
                return resultado;
            }
        }

        public void Atualizar(int id, Restaurante restaurante)
        {
            AtualizarLocalizacao(ObterIdLocalizacao(id), restaurante.Localizacao);
            AtualizarContato(ObterIdContato(id), restaurante.Contato);
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var atualizarRestaurante = new SqlCommand("UPDATE [dbo].[Restaurante] SET [Capacidade] = @Capacidade"
                     + ",[Nome] = @Nome, [Categoria] = @Categoria"
                     + " WHERE [Id]=@Id", connection))
                {
                    atualizarRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                    atualizarRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                    atualizarRestaurante.Parameters.Add("@Categoria", SqlDbType.Int).Value = restaurante.Categoria;
                    atualizarRestaurante.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    atualizarRestaurante.ExecuteNonQuery();
                }
            }
        }

        private void AtualizarContato(int id, Contato contato)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var atualizarLocalizacao= new SqlCommand("UPDATE [dbo].[Contato] SET [Site] = @Site, [Telefone] = @Telefone WHERE [Id]=@Id", connection))
                {
                    atualizarLocalizacao.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site;
                    atualizarLocalizacao.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone;
                    atualizarLocalizacao.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    atualizarLocalizacao.ExecuteNonQuery();
                }
            }
        }

        private void AtualizarLocalizacao(int id, Localizacao localizacao)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                using (var atualizarLocalizacao = new SqlCommand("UPDATE [dbo].[Localizacao] SET [Bairro] = @Bairro"
                     + ", [Logradouro] = @Logradouro, [Latitude] = @Latitude, [Longitude] = @Longitude"
                     + " WHERE [Id]=@Id", connection))
                {
                    atualizarLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    atualizarLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    atualizarLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    atualizarLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;               
                    atualizarLocalizacao.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    atualizarLocalizacao.ExecuteNonQuery();
                }
            }

        }

        public IEnumerable<Restaurante> Todos()
        {
            var restaurantes = new List<Restaurante>();
            using (var connection = new SqlConnection(this.connectionString))
            { 
                using (var selecionaRestaurantes = new SqlCommand("SELECT r.[Id],[Capacidade],[Nome],[Categoria],c.[Site]"
                    + ",c.[Telefone],l.[Bairro],l.[Latitude],l.[Logradouro],l.[Longitude] FROM [dbo].[Restaurante] AS r"
                    + " INNER JOIN [dbo].[Contato] AS c ON c.Id = r.ContatoId INNER JOIN [dbo].[Localizacao] AS l ON"
                    + " l.Id = r.LocalizacaoId", connection))
                {
                    connection.Open();

                    var reader = selecionaRestaurantes.ExecuteReader();

                    while (reader.Read())
                    {
                        restaurantes.Add(new Restaurante
                        {
                            Capacidade = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Contato = new Contato
                            {
                                Site = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Telefone = reader.IsDBNull(3) ? null : reader.GetString(3)
                            },
                            Localizacao = new Localizacao
                            {
                                Bairro = reader.GetString(4),
                                Latitude = reader.GetDouble(5),
                                Logradouro = reader.GetString(6),
                                Longitude = reader.GetDouble(7),
                            },
                            Categoria = reader.GetString(8).ToEnum<Categoria>(),
                        });
                    }
                }
            }
            return restaurantes;
        }
    }
}
