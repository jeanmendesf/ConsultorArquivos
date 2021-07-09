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
        string connectionString = @"Data Source =  DESKTOP-9D3IEDO\SQLEXPRESS01;
                                    Initial Catalog = db_ConsultorArquivos; Integrated Security=True";

        public Endereco PreencherClienteEndereco(int ClienteId)
        {
            Endereco endereco = new Endereco();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand
                                 ("SELECT * FROM dbo.Endereco WHERE ClienteId =" + ClienteId, connection);
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
                }
            }

            return endereco;
        }

    }
}
