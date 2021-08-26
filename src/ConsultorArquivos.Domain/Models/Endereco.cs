using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorArquivos.Domain.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public int? Cep { get; set; }
        public int ClienteId { get; set; }

    }
}
