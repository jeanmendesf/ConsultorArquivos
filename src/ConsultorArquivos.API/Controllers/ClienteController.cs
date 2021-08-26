using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultorArquivos.Data.DAO;
using ConsultorArquivos.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsultorArquivos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        readonly ClienteDAO _clienteDAO;
        public ClienteController()
        {
            _clienteDAO = new ClienteDAO();
        }


        [HttpGet]
        [Route("")]
        public ActionResult ObterTodosClientes()
        {
            IEnumerable<Cliente> listaClientes;
            listaClientes = _clienteDAO.ObterTodosClientes();

            return Ok(listaClientes);
        }


        [HttpPost]
        [Route("adicionar")]
        public ActionResult AdicionarCliente(Cliente cliente)
        {
            _clienteDAO.AdicionarCliente(cliente);
            return Ok();
        }


        [HttpPut]
        [Route("atualizar/{id:int}")]
        public ActionResult AtualizarCliente(Cliente cliente, int id)
        {
            cliente.Id = id;
            _clienteDAO.AtualizarCliente(cliente);
            return Ok();
        }


        [HttpDelete]
        [Route("deletar/{id:int}")]
        public ActionResult DeletarCliente(int id)
        {
            _clienteDAO.DeletarCliente(id);
            return Ok();
        }

    }
}
