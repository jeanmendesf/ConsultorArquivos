USE db_ConsultorArquivos

SELECT * FROM dbo.Cliente
SELECT * FROM dbo.Contato
SELECT * FROM dbo.Endereco


--Criar trigger para adicionar enderecoId/contatoId ao Cliente logo apos de adiciona-lo

INSERT INTO Contato(Email, DDD, Telefone)
	VALUES ('teste01@teste01',11,11111111);

INSERT INTO Endereco(Logradouro, Numero, Complemento, Cidade, Estado,Pais,Cep)
	VALUES('1','','','','','','');

INSERT INTO Cliente (ClientCode, Cnpj, ContatoId, EnderecoId)
	VALUES ('05102127120141', '12345678900011', 1,1 );
