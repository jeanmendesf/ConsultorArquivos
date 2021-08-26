USE db_ConsultorArquivos

SELECT * FROM dbo.Cliente
SELECT * FROM dbo.Contato
SELECT * FROM dbo.Endereco


INSERT INTO Cliente (ClientCode, Cnpj)
	VALUES ('05102127120141', '12345678900011');

INSERT INTO Contato(Email, DDD, Telefone, ClienteId)
	VALUES ('teste01@teste01',11,11111111, 1);

INSERT INTO Endereco(Logradouro, Numero, Complemento, Cidade, Estado,Pais,Cep, ClienteId)
	VALUES('1','','','','','','', 1);



DELETE FROM dbo.Cliente WHERE Id = 3

SELECT Id FROM Cliente WHERE ClientCode = 05102127120141