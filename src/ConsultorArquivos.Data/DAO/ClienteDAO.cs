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


        readonly string connectionString = @"Data Source =  DESKTOP-9D3IEDO\SQLEXPRESS02;
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
                    cliente.Contato = _contatoDAO.PreencherClienteContato(cliente.Id);                    
                    cliente.Endereco = _enderecoDAO.PreencherClienteEndereco(cliente.Id);

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
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Cliente(ClientCode, Cnpj)" +
                    "VALUES(@ClienteCode, @Cnpj)", connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ClienteCode", cliente.ClientCode);
                cmd.Parameters.AddWithValue("@Cnpj", cliente.Cnpj);                

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            _contatoDAO.AdicionarContato(cliente.Contato, cliente.ClientCode);
            _enderecoDAO.AdicionarEndereco(cliente.Endereco, cliente.ClientCode);
        }


        public void DeletarCliente(int id)
        {
            _contatoDAO.DeletarContato(id);
            _enderecoDAO.DeletarEndereco(id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Cliente WHERE Id =" + id, connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        //Se o registro existir, retornará True, se não, retornará False
        public bool Existe(string clientCode)
        {
            Cliente cliente = new Cliente();
            
            //Verificar se há algum cliente com um clienteCode especifico.
            
            return true;
        }

    }
}
