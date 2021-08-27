using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorArquivos.Domain.Models
{
    public class Endereco
    {
        public Endereco(string logradouro, int? numero, string complemento,
                        string cidade, string estado, string pais, int? cep)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Cep = cep;
        }

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
