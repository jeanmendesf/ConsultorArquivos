USE db_ConsultorArquivos

SELECT * FROM dbo.Cliente
SELECT * FROM dbo.Contato
SELECT * FROM dbo.Endereco

INSERT INTO Cliente (ClienteCode, Cnpj)
	VALUES (05102127000140, '12345678900000')

INSERT INTO Contato(Id, Email, DDD, Telefone, ClienteId)
	VALUES (1, 'teste@teste',11,12345678,1)

INSERT INTO Endereco(Id,Logradouro, Numero, Complemento, Cidade, Estado,Pais,Cep,ClienteId)
	VALUES(1,'','','','','','','',1)