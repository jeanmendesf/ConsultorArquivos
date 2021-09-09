using ConsultorArquivos.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorArquivos.LeitorArquivos
{
    public class ManipuladorTxt
    {

        //Vai ler um arquivo de texto, organizar e separar por clientes as informações contidas.
        public static async Task<List<Cliente>> LeitorTexto(string txtEndereco)
        {
            List<Cliente> clientes = new List<Cliente>();

            

            using (StreamReader streamReader = new StreamReader(txtEndereco))
            {
                var linha = streamReader.ReadLine();

                string[] item = linha.Split(';');
                if (item[0] != "CNPJ")
                {
                    Cliente novoCliente = new Cliente(item[0],
                                                      item[1],
                                                      string.IsNullOrEmpty(item[2]) ? 0 : Convert.ToInt32(item[2]),
                                                      item[3],
                                                      item[4],
                                                      item[5],
                                                      item[6],
                                                      string.IsNullOrEmpty(item[7]) ? 0 : Convert.ToInt32(item[7]),
                                                      item[8],
                                                      string.IsNullOrEmpty(item[9]) ? 0 : Convert.ToInt32(item[9]),
                                                      string.IsNullOrEmpty(item[10]) ? 0 : Convert.ToInt32(item[10]),
                                                      item[11]);

                    if (!ClienteExiste(novoCliente, clientes))
                        clientes.Add(novoCliente);
                }
            }       

            
            return clientes;
        }


        //Vai retornar True se o cliente existe, false caso não exista
        //Procurar e excluir o cliente antigo que tenha o mesmo ClientCode e adicionar o cliente novo.
        public static bool ClienteExiste(Cliente novoCliente, List<Cliente> listaClientes)
        {
            bool existe = new bool(); 

            foreach (Cliente cliente in listaClientes.ToList())
            {
                if (cliente.ClientCode == novoCliente.ClientCode)
                {
                    listaClientes.Remove(cliente);
                    listaClientes.Add(novoCliente);
                    existe = true;
                }
                else
                    existe = false;
            }
            return existe;
        }


    }
}
