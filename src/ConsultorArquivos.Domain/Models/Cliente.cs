using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorArquivos.Domain.Models
{
    public class Cliente
    {
        public Cliente(){}
        public Cliente(string cnpj, string logradouro, int? numero, string complemento,
                      string cidade, string estado, string pais, int? cep,
                      string email, int? dDD, int? telefone, string clientCode)
        {
            Cnpj = cnpj;
            Endereco.Logradouro = logradouro;
            Endereco.Numero = numero;
            Endereco.Complemento = complemento;
            Endereco.Cidade = cidade;
            Endereco.Estado = estado;
            Endereco.Pais = pais;
            Endereco.Cep = cep;
            Contato.Email = email;
            Contato.DDD = dDD;
            Contato.Telefone = telefone;
            ClientCode = clientCode;
        }


        public int Id { get; set; }
        public string ClientCode { get; set; }
        public string Cnpj { get; set; }
        public Endereco Endereco { get; set; }
        public Contato Contato { get; set; }

        public int ContatoId { get; set; }
        public int EnderecoId { get; set; }

        public override string ToString()
        {
            //Override para testar se os campos estão sendo preenchidos corretamente.

            return $" Cnpj: {Cnpj} \n Logradouro: {Endereco.Logradouro} \n Numero: {Endereco.Numero} \n Complemento:{Endereco.Complemento} \n" +
                $" Cidade: {Endereco.Cidade} \n Estado:{Endereco.Estado} \n Pais: {Endereco.Pais} \n Cep:{Endereco.Cep} \n Email:{Contato.Email} \n" +
                $" DDD: {Contato.DDD} \n Telefone: {Contato.Telefone} \n ClientCode: {ClientCode} \n \n ========================";
        }
    }
}
