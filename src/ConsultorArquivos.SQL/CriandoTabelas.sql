CREATE DATABASE db_ConsultorArquivos;

--Criando tabela Cliente
USE db_ConsultorArquivos
CREATE TABLE Cliente(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ClientCode VARCHAR(100) UNIQUE NOT NULL,
	CNPJ VARCHAR(14) UNIQUE NOT NULL,	
	EnderecoId INT,
	ContatoId INT
);



--Criando tabela Contato
USE db_ConsultorArquivos
CREATE TABLE Contato(
	Id		INT PRIMARY KEY NOT NULL ,
	Email	VARCHAR(100),
	DDD		INT,
	Telefone INT,
	ClienteId INT NOT NULL
);

--Criando tabela Endereco
USE db_ConsultorArquivos
CREATE TABLE Endereco(
	Id			INT PRIMARY KEY NOT NULL ,	
	Logradouro	VARCHAR(200),
	Numero		INT,
	Complemento VARCHAR(200),
	Cidade		VARCHAR(100),
	Estado		VARCHAR(100),
	Pais		VARCHAR(100),
	Cep			INT,
	ClienteId	INT NOT NULL

);

--Relacionando Foreign Key
--Cliente_Endereco
USE db_ConsultorArquivos
ALTER TABLE Cliente
	ADD CONSTRAINT Fk_Endereco
	FOREIGN KEY (EnderecoId)
	REFERENCES dbo.Endereco(Id);

--Cliente_Contato
USE db_ConsultorArquivos
ALTER TABLE Cliente
	ADD CONSTRAINT Fk_Contato
	FOREIGN KEY (ContatoId)
	REFERENCES dbo.Contato(Id);

--Endereco_Cliente
USE db_ConsultorArquivos
ALTER TABLE Endereco
	ADD CONSTRAINT Fk_Cliente_Endereco
	FOREIGN KEY (ClienteId)
	REFERENCES dbo.Cliente(Id);

--Contato_Cliente
USE db_ConsultorArquivos
ALTER TABLE Contato
	ADD CONSTRAINT Fk_Cliente_Contato
	FOREIGN KEY (ClienteId)
	REFERENCES dbo.Cliente(Id);