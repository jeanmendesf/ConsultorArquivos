using ConsultorArquivos.Domain.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsultorArquivos.Data.DAO
{
    public class ContatoDAO
    {
        string connectionString = @"Data Source =  DESKTOP-9D3IEDO\SQLEXPRESS02;
                                    Initial Catalog = db_ConsultorArquivos; Integrated Security=True";


        public Contato PreencherClienteContato(int clienteId)
        {
            Contato contato = new Contato();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand
                                 ("SELECT * FROM dbo.Contato WHERE ClienteId=" + clienteId, connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contato.Id = Convert.ToInt32(reader["Id"]);
                    contato.Email = reader["Email"].ToString();
                    contato.DDD = Convert.ToInt32(reader["DDD"]);
                    contato.Telefone = Convert.ToInt32(reader["Telefone"]);
                    contato.ClienteId = clienteId;
                }
                connection.Close();
            }

            return contato;
        }


        public void AdicionarContato(Contato contato, string clientCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Contato(Email, DDD, Telefone, ClienteId)" +
                    "VALUES(@Email, @DDD, @Telefone, @ClienteId)", connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Email", contato.Email);
                cmd.Parameters.AddWithValue("@DDD", contato.DDD);
                cmd.Parameters.AddWithValue("@Telefone", contato.Telefone);
                cmd.Parameters.AddWithValue("@ClienteId", AcharClientePorClientCode(clientCode));


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void AtualizarContato(Contato contato, int clienteId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Contato SET " +
                    "Email=@Email, DDD=@DDD, Telefone=@Telefone WHERE ClienteId = " + clienteId, connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Email", contato.Email);
                cmd.Parameters.AddWithValue("@DDD", contato.DDD);
                cmd.Parameters.AddWithValue("@Telefone", contato.Telefone);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void DeletarContato(int clienteId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Contato WHERE ClienteId =" + clienteId, connection);
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
