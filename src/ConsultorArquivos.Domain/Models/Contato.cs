using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorArquivos.Domain.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int? DDD { get; set; }
        public int? Telefone { get; set; }
        public int ClienteId { get; set; }

    }
}
