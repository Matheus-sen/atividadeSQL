-- drop database Atividade_Conexao;
CREATE DATABASE Atividade_Conexao;

USE Atividade_Conexao;

DROP TABLE IF EXISTS categoria;
CREATE TABLE categoria(
	id_categoria INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome_categoria VARCHAR(60) NOT NULL
);

DROP TABLE IF EXISTS produto;
CREATE TABLE produto(
	id_produto INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    id_categoria INT NOT NULL,
    nome_produto VARCHAR(60) NOT NULL,

     FOREIGN KEY (id_categoria)
		 REFERENCES categoria (id_categoria)
);

DROP TABLE IF EXISTS login;
CREATE TABLE login(
	id_usuário INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	user VARCHAR(20) NOT NULL,
    senha VARCHAR(20) NOT NULL,
    tipo_usuario VARCHAR(15)
);

SELECT * FROM categoria;
SELECT * FROM produto;
SELECT * FROM login;


SELECT 
	p.id_produto, p.nome_produto,
    c.nome_categoria
FROM produto p
LEFT JOIN categoria c
	ON p.id_categoria = c.id_categoria
;







