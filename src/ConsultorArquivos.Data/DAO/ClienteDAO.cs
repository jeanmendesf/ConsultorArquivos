using ConsultorArquivos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorArquivos.Data.DAO
{
    public class ClienteDAO
    {
        string connectionString = @"Data Source =  DESKTOP-9D3IEDO\SQLEXPRESS01;
                                    Initial Catalog = db_ConsultorArquivos; Integrated Security=True";

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();


            return listaClientes;
        }

    }
}
