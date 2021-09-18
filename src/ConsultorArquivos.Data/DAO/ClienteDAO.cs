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

                while (reader.Read())
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


        public void AdicionarVariosClientes(List<Cliente> listaCliente)
        {
            foreach (Cliente cliente in listaCliente)
            {
                AdicionarCliente(cliente);
            }
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


        public void AtualizarCliente(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE dbo.Cliente SET " +
                    "ClientCode=@ClientCode, Cnpj=@Cnpj WHERE Id =" + cliente.Id, connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ClientCode", cliente.ClientCode);
                cmd.Parameters.AddWithValue("@Cnpj", cliente.Cnpj);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }

            _contatoDAO.AtualizarContato(cliente.Contato, cliente.Id);
            _enderecoDAO.AtualizarEndereco(cliente.Endereco, cliente.Id);
        }


        //Se o registro existir, retornará True, se não, retornará False
        //Verificar se há algum cliente com um clienteCode especifico.
        public bool Existe(string clientCode)
        {
            bool existe;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Id FROM dbo.Cliente WHERE ClientCode =" + clientCode, connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                object obj = cmd.ExecuteScalar();

                if (obj != null)
                    existe = true;

                else
                    existe = false;

                connection.Close();
            }

            return existe;
        }


        //Irá verificar uma lista, e retornar duas lista de dados: "Novo Cliente" ou "Cliente existente"
        public (List<Cliente>, List<Cliente>) VerificarClientes(List<Cliente> clientes)
        {
            var clientesNovos = new List<Cliente>();
            var clientesExistentes = new List<Cliente>();

            //Testar todos clientes utilizando a função "Existe"
            //Se o cliente ja existir, colocar na lista clientesExistentes, senão na clientesNovos.
            foreach (Cliente cliente in clientes)
            {
                if (Existe(cliente.ClientCode))
                {
                    //Adiciona o cliente existente na lista clientesExistentes
                    clientesExistentes.Add(cliente);
                }
                else
                {
                    //Adiciona o cliente novo na lista clientesNovos
                    clientesNovos.Add(cliente);
                }
            }


            return (clientesNovos, clientesExistentes);
        }
    }
}
