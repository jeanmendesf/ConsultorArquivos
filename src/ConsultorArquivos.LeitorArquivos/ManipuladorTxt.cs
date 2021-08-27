using ConsultorArquivos.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsultorArquivos.LeitorArquivos
{
    public class ManipuladorTxt
    {

        //Vai ler um arquivo de texto, organizar e separar por clientes as informações contidas.
        public static void LeitorTexto(string txtEndereco)
        {
            List<Cliente> clientes = new List<Cliente>();

            var linhas = File.ReadAllLines(txtEndereco).ToList();

            foreach (var linha in linhas)
            {
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
                    clientes.Add(novoCliente);
                }
            }

            foreach(Cliente c in clientes)
                Console.WriteLine(c);

            Console.ReadLine();

            //Organizar arquivo para retornar a lista de clientes, criada acima.
            //return clientes;
        }
    }
}
