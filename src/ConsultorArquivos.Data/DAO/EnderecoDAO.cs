using ConsultorArquivos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsultorArquivos.Data.DAO
{
    public class EnderecoDAO
    {     
        string connectionString = @"Data Source =  DESKTOP-9D3IEDO\SQLEXPRESS02;
                                    Initial Catalog = db_ConsultorArquivos; Integrated Security=True";


        public Endereco PreencherClienteEndereco(int clienteId)
        {
            Endereco endereco = new Endereco();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand
                                 ("SELECT * FROM dbo.Endereco WHERE ClienteId =" + clienteId, connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    endereco.Id = Convert.ToInt32(reader["Id"]);
                    endereco.Logradouro = reader["Logradouro"].ToString();
                    endereco.Numero = Convert.ToInt32(reader["Numero"]);
                    endereco.Complemento = reader["Complemento"].ToString();
                    endereco.Cidade = reader["Cidade"].ToString();
                    endereco.Estado = reader["Estado"].ToString();
                    endereco.Pais = reader["Pais"].ToString();
                    endereco.Cep = Convert.ToInt32(reader["Cep"]);
                    endereco.ClienteId = clienteId;
                }
                connection.Close();
            }

            return endereco;
        }


        public void AdicionarEndereco(Endereco endereco, string clientCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Endereco(Logradouro, Numero, Complemento, Cidade, Estado,Pais,Cep, ClienteId)" +
                    "VALUES(@Logradouro, @Numero, @Complemento, @Cidade, @Estado, @Pais, @Cep, @ClienteId)", connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Logradouro", endereco.Logradouro);
                cmd.Parameters.AddWithValue("@Numero", endereco.Numero);
                cmd.Parameters.AddWithValue("@Complemento", endereco.Complemento);
                cmd.Parameters.AddWithValue("@Cidade", endereco.Cidade);
                cmd.Parameters.AddWithValue("@Estado", endereco.Estado);
                cmd.Parameters.AddWithValue("@Pais", endereco.Pais);
                cmd.Parameters.AddWithValue("@Cep", endereco.Cep);
                cmd.Parameters.AddWithValue("@ClienteId", AcharClientePorClientCode(clientCode));


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void DeletarEndereco(int clienteId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Endereco WHERE ClienteId =" + clienteId, connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        //Otimizar para não haver repetições !!
        public int AcharClientePorClientCode(string clientCode)
        {
            int idCliente = new int();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand
                    ("SELECT Id FROM Cliente WHERE ClientCode =" + clientCode, connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    idCliente = Convert.ToInt32(reader["Id"]);
                }
                connection.Close();
            }
            return idCliente;
        }

    }
}
