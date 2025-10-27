CREATE DATABASE IF NOT EXISTS FolhaEsigDB;
USE FolhaEsigDB;
-- Entidades
CREATE TABLE IF NOT EXISTS cargo (
    cargo_id INT AUTO_INCREMENT PRIMARY KEY,
    cargo_nome VARCHAR(100) NOT NULL,
    salario_base DECIMAL(10,2) NOT NULL
);

CREATE TABLE IF NOT EXISTS pessoa (
    pessoa_id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cidade VARCHAR(50),
    email VARCHAR(100),
    cep VARCHAR(20),
    endereco VARCHAR(150),
    pais VARCHAR(50),
    usuario VARCHAR(50),
    telefone VARCHAR(20),
    data_nascimento VARCHAR(20),
    cargo_id INT NOT NULL,
    FOREIGN KEY (cargo_id) REFERENCES cargo(cargo_id)
);

CREATE TABLE IF NOT EXISTS pessoa_salario (
    pessoa_id INT NOT NULL PRIMARY KEY,
    pessoa_nome VARCHAR(100) NOT NULL,
    cargo_nome VARCHAR(100) NOT NULL,
    salario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (pessoa_id) REFERENCES pessoa(pessoa_id)
);
-- Views
CREATE OR REPLACE VIEW vw_listarpessoas AS
SELECT 
    p.pessoa_id,
    p.nome,
    p.cidade,
    p.email,
    p.cep,
    p.endereco,
    p.pais,
    p.usuario,
    p.telefone,
    p.data_nascimento,
    c.cargo_nome,
    c.salario_base
FROM pessoa p
JOIN cargo c ON p.cargo_id = c.cargo_id;

CREATE OR REPLACE VIEW vw_listarcargos AS
SELECT cargo_id, cargo_nome, salario_base
FROM cargo;


DELIMITER $$

CREATE PROCEDURE AtualizarPessoaSalario()
BEGIN
-- Atualiza registros existentes quando houver mudança
    UPDATE pessoa_salario ps
    JOIN pessoa p ON ps.pessoa_id = p.pessoa_id
    JOIN cargo c ON p.cargo_id = c.cargo_id
    SET ps.pessoa_nome = p.nome,
        ps.cargo_nome = c.cargo_nome,
        ps.salario = c.salario_base
    WHERE ps.pessoa_nome <> p.nome
       OR ps.cargo_nome <> c.cargo_nome
       OR ps.salario <> c.salario_base;

-- Insere registros novos que não existem na tabela pessoa_salario
    INSERT INTO pessoa_salario (pessoa_id, pessoa_nome, cargo_nome, salario)
    SELECT p.pessoa_id, p.nome, c.cargo_nome, c.salario_base
    FROM pessoa p
    JOIN cargo c ON p.cargo_id = c.cargo_id
    LEFT JOIN pessoa_salario ps ON ps.pessoa_id = p.pessoa_id
    WHERE ps.pessoa_id IS NULL;

-- Remove registros que não existem mais na tabela pessoa
    DELETE ps
    FROM pessoa_salario ps
    LEFT JOIN pessoa p ON ps.pessoa_id = p.pessoa_id
    WHERE p.pessoa_id IS NULL;
END$$

DELIMITER ;


DELIMITER //

CREATE PROCEDURE InserirCargo(
    IN p_cargo_nome VARCHAR(100),
    IN p_salario_base DECIMAL(10,2)
)
BEGIN
    INSERT INTO cargo (cargo_nome, salario_base)
    VALUES (p_cargo_nome, p_salario_base);
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE InserirPessoa(
    IN p_nome VARCHAR(100),
    IN p_cidade VARCHAR(50),
    IN p_email VARCHAR(100),
    IN p_cep VARCHAR(20),
    IN p_endereco VARCHAR(150),
    IN p_pais VARCHAR(50),
    IN p_usuario VARCHAR(50),
    IN p_telefone VARCHAR(20),
    IN p_data_nascimento VARCHAR(20),
    IN p_cargo_id INT
)
BEGIN
    INSERT INTO pessoa (nome, cidade, email, cep, endereco, pais, usuario, telefone, data_nascimento, cargo_id)
    VALUES (p_nome, p_cidade, p_email, p_cep, p_endereco, p_pais, p_usuario, p_telefone, p_data_nascimento, p_cargo_id);
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE ListarPessoaSalario(IN Ordem VARCHAR(50))
BEGIN
    IF Ordem = 'salario' THEN
        SELECT pessoa_id, pessoa_nome, cargo_nome, salario
        FROM pessoa_salario
        ORDER BY salario DESC;
    ELSE
        SELECT pessoa_id, pessoa_nome, cargo_nome, salario
        FROM pessoa_salario
        ORDER BY pessoa_nome;
    END IF;
END //
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE RemoverPessoa(IN p_pessoa_id INT)
BEGIN
    -- Primeiro remove salário vinculado
    DELETE FROM pessoa_salario WHERE pessoa_id = p_pessoa_id;
    
    -- Depois remove pessoa
    DELETE FROM pessoa WHERE pessoa_id = p_pessoa_id;
END$$

DELIMITER ;