CREATE DATABASE Atividade_Conexao;

USE Atividade_Conexao;

DROP TABLE IF EXISTS categoria;
CREATE TABLE categoria(
	id_categoria INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome_categoria VARCHAR(60) NOT NULL
);

CREATE TABLE produto(
	id_produto INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nome_produto VARCHAR(60) NOT NULL,
    id_categoria INT NOT NULL,
    
    FOREIGN KEY (id_categoria)
		REFERENCES categoria (id_categoria)
);

SELECT * FROM categoria;
SELECT * FROM produto;







