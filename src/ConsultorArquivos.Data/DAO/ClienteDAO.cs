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



    }
}
