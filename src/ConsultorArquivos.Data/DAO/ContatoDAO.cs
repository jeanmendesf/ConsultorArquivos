using ConsultorArquivos.Domain.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsultorArquivos.Data.DAO
{
    public class ContatoDAO
    {
        string connectionString = @"Data Source =  DESKTOP-9D3IEDO\SQLEXPRESS01;
                                    Initial Catalog = db_ConsultorArquivos; Integrated Security=True";

        public Contato PreencherClienteContato(int ClienteId)
        {
            Contato contato = new Contato();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand
                                 ("SELECT * FROM dbo.Contato WHERE ClienteId =" + ClienteId, connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contato.Id = Convert.ToInt32(reader["Id"]);
                    contato.Email = reader["Email"].ToString();
                    contato.DDD = Convert.ToInt32(reader["DDD"]);
                    contato.Telefone = Convert.ToInt32(reader["Telefone"]);                    
                }
            }

            return contato;
        }

    }
}
