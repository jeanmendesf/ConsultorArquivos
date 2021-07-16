using ConsultorArquivos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsultorArquivos.Data.DAO
{
    public class ClienteDAO
    {
        readonly ContatoDAO _contatoDAO;
        readonly EnderecoDAO _enderecoDAO;
        public ClienteDAO()
        {
            _contatoDAO = new ContatoDAO();
            _enderecoDAO = new EnderecoDAO();
        }


        string connectionString = @"Data Source =  DESKTOP-9D3IEDO\SQLEXPRESS01;
                                    Initial Catalog = db_ConsultorArquivos; Integrated Security=True";

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Cliente", connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.Id = Convert.ToInt32(reader["Id"]);
                    cliente.ClientCode = reader["ClientCode"].ToString();
                    cliente.Cnpj = reader["Cnpj"].ToString();
                    cliente.ContatoId = Convert.ToInt32(reader["ContatoId"]);
                    cliente.EnderecoId = Convert.ToInt32(reader["EnderecoId"]);
                    cliente.Contato = _contatoDAO.PreencherClienteContato(cliente.ContatoId);                    
                    cliente.Endereco = _enderecoDAO.PreencherClienteEndereco(cliente.EnderecoId);

                    listaClientes.Add(cliente);
                }

                connection.Close();
            }
            return listaClientes;
        }


        public void AdicionarCliente(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                _contatoDAO.AdicionarContato(cliente.Contato);
                _enderecoDAO.AdicionarEndereco(cliente.Endereco);

                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Cliente(ClientCode, Cnpj, ContatoId, EnderecoId)" +
                    "VALUES(@ClienteCode, @Cnpj, @ContatoId, @EnderecoId)");
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ClienteCode", cliente.ClientCode);
                cmd.Parameters.AddWithValue("@Cnpj", cliente.Cnpj);
                cmd.Parameters.AddWithValue("@ContatoId", cliente.ContatoId);
                cmd.Parameters.AddWithValue("@EnderecoId", cliente.EnderecoId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


    }
}
