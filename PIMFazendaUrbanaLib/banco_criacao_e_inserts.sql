### CRIAÇÃO DO BANCO:
CREATE DATABASE pimfazendaurbana;
USE pimfazendaurbana;

begin;
### PESSOAS
## Cliente:
CREATE TABLE `cliente` (
	`id_cliente` int NOT NULL AUTO_INCREMENT,
	`nome_cliente` varchar(100) NOT NULL,
	`email_cliente` varchar(50) NOT NULL UNIQUE,
	`cnpj_cliente` varchar(25) NOT NULL UNIQUE,
	`ativo_cliente` boolean DEFAULT true,
	PRIMARY KEY (`id_cliente`)
);
CREATE TABLE `enderecocliente` (
	`logradouro_endcliente` varchar(100) NOT NULL,
	`numero_endcliente` varchar(10) NOT NULL,
	`complemento_endcliente` varchar(50) DEFAULT NULL,
	`bairro_endcliente` varchar(100) NOT NULL,
	`cidade_endcliente` varchar(100) NOT NULL,
	`uf_endcliente` varchar(10) NOT NULL,
	`cep_endcliente` varchar(25) NOT NULL,
	`ativo_endcliente` boolean DEFAULT true,
	`id_cliente` int NOT NULL,
	PRIMARY KEY (`id_cliente`),
	KEY `id_cliente` (`id_cliente`),
	CONSTRAINT `enderecocliente_ibfk_1` FOREIGN KEY (`id_cliente`) REFERENCES `cliente` (`id_cliente`)
);
CREATE TABLE `telefonecliente` (
	`ddd_telcliente` varchar(10) NOT NULL,
	`numero_telcliente` varchar(25) NOT NULL,
	`ativo_telcliente` boolean DEFAULT true,
	`id_cliente` int NOT NULL,
	PRIMARY KEY (`id_cliente`),
	KEY `id_cliente` (`id_cliente`),
	CONSTRAINT `telefonecliente_ibfk_1` FOREIGN KEY (`id_cliente`) REFERENCES `cliente` (`id_cliente`)
);

## Fornecedor:
CREATE TABLE `fornecedor` (
	`id_fornecedor` int NOT NULL AUTO_INCREMENT,
	`nome_fornecedor` varchar(100) NOT NULL,
	`email_fornecedor` varchar(50) NOT NULL UNIQUE,
	`cnpj_fornecedor` varchar(25) NOT NULL UNIQUE,
	`ativo_fornecedor` boolean DEFAULT true,
	PRIMARY KEY (`id_fornecedor`)
);
CREATE TABLE `enderecofornecedor` (
	`logradouro_endfornecedor` varchar(100) NOT NULL,
	`numero_endfornecedor` varchar(10) NOT NULL,
	`complemento_endfornecedor` varchar(50) DEFAULT NULL,
	`bairro_endfornecedor` varchar(100) NOT NULL,
	`cidade_endfornecedor` varchar(100) NOT NULL,
	`uf_endfornecedor` varchar(10) NOT NULL,
	`cep_endfornecedor` varchar(25) NOT NULL,
	`ativo_endfornecedor` boolean DEFAULT true,
	`id_fornecedor` int NOT NULL,
	PRIMARY KEY (`id_fornecedor`),
	KEY `id_fornecedor` (`id_fornecedor`),
	CONSTRAINT `enderecofornecedor_ibfk_1` FOREIGN KEY (`id_fornecedor`) REFERENCES `fornecedor` (`id_fornecedor`)
);
CREATE TABLE `telefonefornecedor` (
  `ddd_telfornecedor` varchar(10) NOT NULL,
  `numero_telfornecedor` varchar(25) NOT NULL,
  `ativo_telfornecedor` boolean DEFAULT true,
  `id_fornecedor` int NOT NULL,
  PRIMARY KEY (`id_fornecedor`),
  KEY `id_fornecedor` (`id_fornecedor`),
  CONSTRAINT `telefonefornecedor_ibfk_1` FOREIGN KEY (`id_fornecedor`) REFERENCES `fornecedor` (`id_fornecedor`)
);

## Funcionario:
CREATE TABLE `funcionario` (
	`id_funcionario` int NOT NULL AUTO_INCREMENT,
	`nome_funcionario` varchar(100) NOT NULL,
    `cpf_funcionario` varchar(25) NOT NULL UNIQUE,
	`sexo_funcionario` varchar(10) DEFAULT NULL,
	`email_funcionario` varchar(100) NOT NULL UNIQUE,
	`cargo_funcionario` varchar(50) NOT NULL,
	`usuario_funcionario` varchar(50) NOT NULL UNIQUE,
	`senha_funcionario` varchar(50) NOT NULL,
	`ativo_funcionario` boolean DEFAULT true,
	PRIMARY KEY (`id_funcionario`)
);
CREATE TABLE `enderecofuncionario` (
	`logradouro_endfuncionario` varchar(100) NOT NULL,
	`numero_endfuncionario` varchar(10) NOT NULL,
	`complemento_endfuncionario` varchar(50) DEFAULT NULL,
	`bairro_endfuncionario` varchar(100) NOT NULL,
	`cidade_endfuncionario` varchar(100) NOT NULL,
	`uf_endfuncionario` varchar(10) NOT NULL,
	`cep_endfuncionario` varchar(25) NOT NULL,
	`ativo_endfuncionario` boolean DEFAULT true,
	`id_funcionario` int NOT NULL,
	PRIMARY KEY (`id_funcionario`),
	KEY `id_funcionario` (`id_funcionario`),
	CONSTRAINT `enderecofuncionario_ibfk_1` FOREIGN KEY (`id_funcionario`) REFERENCES `funcionario` (`id_funcionario`)
);
CREATE TABLE `telefonefuncionario` (
	`ddd_telfuncionario` varchar(10) NOT NULL,
	`numero_telfuncionario` varchar(25) NOT NULL,
	`ativo_telfuncionario` boolean DEFAULT true,
	`id_funcionario` int NOT NULL,
	PRIMARY KEY (`id_funcionario`),
	KEY `id_funcionario` (`id_funcionario`),
	CONSTRAINT `telefonefuncionario_ibfk_1` FOREIGN KEY (`id_funcionario`) REFERENCES `funcionario` (`id_funcionario`)
);
commit;

### VENDAS E PRODUÇÃO
begin;
-- Criar a tabela cultivo
CREATE TABLE `cultivo` (
    `id_cultivo` int NOT NULL AUTO_INCREMENT,
    `nome_cultivo` varchar(100) NOT NULL,
    `variedade_cultivo` varchar(100) NOT NULL UNIQUE,
    `tempoprodtrad_cultivo` int DEFAULT NULL,
    `tempoprodctrl_cultivo` int DEFAULT NULL,
	`categoria_cultivo` varchar(50) NOT NULL,
	`ativo_cultivo` boolean DEFAULT true,
	PRIMARY KEY (`id_cultivo`)
);

-- Inserir os dados na tabela cultivo
INSERT INTO `cultivo` (
	`id_cultivo`, 
	`nome_cultivo`, 
	`variedade_cultivo`, 
	`tempoprodtrad_cultivo`, 
	`tempoprodctrl_cultivo`, 
	`categoria_cultivo`
) VALUES
(1, 'Abacaxi', 'Abacaxi Pérola', 600, 365, 'Fruta'),
(2, 'Abóbora', 'Abóbora Japonesa', 100, 60, 'Legume'),
(3, 'Abobrinha', 'Abobrinha Menina Brasileira', 50, 30, 'Legume'),
(4, 'Acelga', 'Acelga Verde de Verão', 60, 35, 'Verdura'),
(5, 'Agrião', 'Agrião de Água', 45, 25, 'Verdura'),
(6, 'Alface', 'Alface Crespa', 66, 41, 'Verdura'),
(7, 'Alface', 'Alface Americana', 70, 44, 'Verdura'),
(8, 'Algodão', 'Algodão BRS 368', 180, 120, 'Outro'),
(9, 'Alho', 'Alho Roxo', 270, 180, 'Legume'),
(10, 'Alho-poró', 'Alho-poró Porto Rico', 150, 90, 'Legume'),
(11, 'Banana', 'Banana Prata', 210, 150, 'Fruta'),
(12, 'Batata-doce', 'Batata-doce Beauregard', 105, 60, 'Legume'),
(13, 'Beterraba', 'Beterraba Detroit Dark Red', 60, 35, 'Legume'),
(14, 'Beterraba', 'Beterraba Early Wonder', 60, 35, 'Legume'),
(15, 'Berinjela', 'Berinjela Roxa', 80, 50, 'Legume'),
(16, 'Brócolis', 'Brócolis Calabrês', 70, 40, 'Legume'),
(17, 'Caju', 'Caju Anão Precoce', 210, 150, 'Fruta'),
(18, 'Cebola', 'Cebola Baia Periforme', 110, 70, 'Legume'),
(19, 'Cebolinha', 'Cebolinha Verde Todo o Ano', 50, 30, 'Verdura'),
(20, 'Cenoura', 'Cenoura Brasília', 80, 50, 'Legume'),
(21, 'Cenoura', 'Cenoura Nantes', 70, 45, 'Legume'),
(22, 'Chicória', 'Chicória Catalonha', 55, 30, 'Verdura'),
(23, 'Coentro', 'Coentro Português', 55, 30, 'Verdura'),
(24, 'Couve', 'Couve Manteiga', 58, 34, 'Verdura'),
(25, 'Couve-de-bruxelas', 'Couve-de-bruxelas Menina', 90, 55, 'Legume'),
(26, 'Couve-flor', 'Couve-flor de Inverno', 55, 30, 'Legume'),
(27, 'Cupuaçu', 'Cupuaçuzeiro', 210, 150, 'Fruta'),
(28, 'Erva-doce', 'Erva-doce de Mesa', 110, 70, 'Verdura'),
(29, 'Ervilha', 'Ervilha Douce Provence', 65, 40, 'Legume'),
(30, 'Ervilha', 'Ervilha Early Frosty', 65, 40, 'Legume'),
(31, 'Espinafre', 'Espinafre Gigante de Inverno', 50, 30, 'Verdura'),
(32, 'Fava', 'Fava Superaguadulce', 90, 55, 'Legume'),
(33, 'Feijão', 'Feijão BRS Estilo', 80, 50, 'Legume'),
(34, 'Feijão', 'Feijão Elettra', 90, 55, 'Legume'),
(35, 'Feijão', 'Feijão Carioca', 80, 50, 'Legume'),
(36, 'Feijão-vagem', 'Feijão-vagem Macarrão', 65, 40, 'Legume'),
(37, 'Girassol', 'Girassol Catissol 01', 100, 60, 'Outro'),
(38, 'Hortelã', 'Hortelã Comum', 95, 60, 'Verdura'),
(39, 'Manjericão', 'Manjericão Genovês', 65, 40, 'Verdura'),
(40, 'Mandioca', 'Mandioca Branca', 273, 180, 'Legume'),
(41, 'Maracujá', 'Maracujá Azedo', 165, 100, 'Legume'),
(42, 'Maxixe', 'Maxixe Paulista', 60, 35, 'Legume'),
(43, 'Melancia', 'Melancia Crimson Sweet', 90, 55, 'Fruta'),
(44, 'Melão', 'Melão Orange Sherbet', 90, 55, 'Fruta'),
(45, 'Milho', 'Milho Doce', 80, 50, 'Legume'),
(46, 'Morango', 'Morango Camarosa', 68, 40, 'Fruta'),
(47, 'Orégano', 'Orégano Grego', 75, 45, 'Verdura'),
(48, 'Pepino', 'Pepino Caipira', 60, 35, 'Legume'),
(49, 'Pimentão', 'Pimentão Amarelo', 80, 50, 'Legume'),
(50, 'Quiabo', 'Quiabo Cristal', 55, 30, 'Legume'),
(51, 'Rabanete', 'Rabanete Crimson Giant', 25, 20, 'Legume'),
(52, 'Repolho', 'Repolho Roxo de Inverno', 100, 60, 'Legume'),
(53, 'Rúcula', 'Rúcula Cultivada', 45, 25, 'Verdura'),
(54, 'Salsa', 'Salsa Gigante de Itália', 80, 50, 'Verdura'),
(55, 'Salsinha', 'Salsinha Crespa', 80, 50, 'Verdura'),
(56, 'Soja', 'Soja BRS 369', 150, 100, 'Legume'),
(57, 'Tomate', 'Tomate Cereja', 75, 46, 'Fruta'),
(58, 'Tomate', 'Tomate Italiano', 79, 48, 'Fruta'),
(59, 'Tomate', 'Tomate Santa Cruz Kada', 80, 48, 'Fruta'),
(60, 'Tomate', 'Tomateiro Cereja', 75, 46, 'Fruta'),
(61, 'Tomilho', 'Tomilho Limão', 85, 50, 'Verdura')
;
commit;

begin;
-- Criar a tabela recomendacaocultivo
CREATE TABLE `recomendacaocultivo` (
    `id_recomendacaocultivo` int NOT NULL AUTO_INCREMENT,
    `regiao` VARCHAR(25) NOT NULL,
    `estacao` VARCHAR(25) NOT NULL,
    `id_cultivo` INT NOT NULL,
    PRIMARY KEY (`id_recomendacaocultivo`, `id_cultivo`),
	KEY `id_cultivo` (`id_cultivo`),
    CONSTRAINT `recomendacaocultivo_ibfk_1` FOREIGN KEY (`id_cultivo`) REFERENCES `cultivo`(`id_cultivo`)
);

-- Inserir os dados na tabela recomendacaocultivo
-- Sul
INSERT INTO `recomendacaocultivo` (
	`regiao`, 
	`estacao`, 
	`id_cultivo`
) VALUES
('Sul', 'Inverno', 31), ('Sul', 'Inverno', 24), ('Sul', 'Inverno', 6), ('Sul', 'Inverno', 53), ('Sul', 'Inverno', 16),
('Sul', 'Inverno', 26), ('Sul', 'Inverno', 52), ('Sul', 'Inverno', 25), ('Sul', 'Inverno', 19), ('Sul', 'Inverno', 10),
('Sul', 'Inverno', 20), ('Sul', 'Inverno', 13), ('Sul', 'Inverno', 30), ('Sul', 'Inverno', 34), ('Sul', 'Inverno', 54),
('Sul', 'Inverno', 39), ('Sul', 'Inverno', 61),
('Sul', 'Verão', 60), ('Sul', 'Verão', 49), ('Sul', 'Verão', 3), ('Sul', 'Verão', 48), ('Sul', 'Verão', 15),
('Sul', 'Verão', 45), ('Sul', 'Verão', 36), ('Sul', 'Verão', 50), ('Sul', 'Verão', 21), ('Sul', 'Verão', 14),
('Sul', 'Verão', 51), ('Sul', 'Verão', 7), ('Sul', 'Verão', 31), ('Sul', 'Verão', 53), ('Sul', 'Verão', 5),
('Sul', 'Verão', 39), ('Sul', 'Verão', 23), ('Sul', 'Verão', 38), ('Sul', 'Verão', 55), ('Sul', 'Verão', 19),
('Sul', 'Outono', 2), ('Sul', 'Outono', 12), ('Sul', 'Outono', 21), ('Sul', 'Outono', 13), ('Sul', 'Outono', 51),
('Sul', 'Outono', 6), ('Sul', 'Outono', 31), ('Sul', 'Outono', 16), ('Sul', 'Outono', 26), ('Sul', 'Outono', 24),
('Sul', 'Outono', 4), ('Sul', 'Outono', 22), ('Sul', 'Outono', 5), ('Sul', 'Outono', 29), ('Sul', 'Outono', 32),
('Sul', 'Primavera', 59), ('Sul', 'Primavera', 49), ('Sul', 'Primavera', 48), ('Sul', 'Primavera', 15),
('Sul', 'Primavera', 46), ('Sul', 'Primavera', 43), ('Sul', 'Primavera', 44), ('Sul', 'Primavera', 3),
('Sul', 'Primavera', 50), ('Sul', 'Primavera', 36), ('Sul', 'Primavera', 45), ('Sul', 'Primavera', 7),
('Sul', 'Primavera', 53), ('Sul', 'Primavera', 31), ('Sul', 'Primavera', 39);

-- Sudeste
INSERT INTO `recomendacaocultivo` (
	`regiao`, 
	`estacao`, 
	`id_cultivo`
) VALUES
('Sudeste', 'Inverno', 6), ('Sudeste', 'Inverno', 31), ('Sudeste', 'Inverno', 24), ('Sudeste', 'Inverno', 53),
('Sudeste', 'Inverno', 16), ('Sudeste', 'Inverno', 26), ('Sudeste', 'Inverno', 52), ('Sudeste', 'Inverno', 19),
('Sudeste', 'Inverno', 10), ('Sudeste', 'Inverno', 20), ('Sudeste', 'Inverno', 14), ('Sudeste', 'Inverno', 30),
('Sudeste', 'Inverno', 34), ('Sudeste', 'Inverno', 54), ('Sudeste', 'Inverno', 39),
('Sudeste', 'Verão', 58), ('Sudeste', 'Verão', 49), ('Sudeste', 'Verão', 48), ('Sudeste', 'Verão', 15),
('Sudeste', 'Verão', 3), ('Sudeste', 'Verão', 50), ('Sudeste', 'Verão', 45), ('Sudeste', 'Verão', 36),
('Sudeste', 'Verão', 43), ('Sudeste', 'Verão', 44), ('Sudeste', 'Verão', 21), ('Sudeste', 'Verão', 14),
('Sudeste', 'Verão', 51), ('Sudeste', 'Verão', 7), ('Sudeste', 'Verão', 53),
('Sudeste', 'Outono', 6), ('Sudeste', 'Outono', 31), ('Sudeste', 'Outono', 24), ('Sudeste', 'Outono', 53),
('Sudeste', 'Outono', 16), ('Sudeste', 'Outono', 26), ('Sudeste', 'Outono', 52), ('Sudeste', 'Outono', 19),
('Sudeste', 'Outono', 10), ('Sudeste', 'Outono', 20), ('Sudeste', 'Outono', 14), ('Sudeste', 'Outono', 30),
('Sudeste', 'Outono', 34), ('Sudeste', 'Outono', 54), ('Sudeste', 'Outono', 39),
('Sudeste', 'Primavera', 58), ('Sudeste', 'Primavera', 49), ('Sudeste', 'Primavera', 48), ('Sudeste', 'Primavera', 15),
('Sudeste', 'Primavera', 46), ('Sudeste', 'Primavera', 3), ('Sudeste', 'Primavera', 50), ('Sudeste', 'Primavera', 36),
('Sudeste', 'Primavera', 45), ('Sudeste', 'Primavera', 20), ('Sudeste', 'Primavera', 14), ('Sudeste', 'Primavera', 51),
('Sudeste', 'Primavera', 7), ('Sudeste', 'Primavera', 53), ('Sudeste', 'Primavera', 31), ('Sudeste', 'Primavera', 39);

-- Nordeste
INSERT INTO `recomendacaocultivo` (
	`regiao`, 
	`estacao`, 
	`id_cultivo`
) VALUES
('Nordeste', 'Verão', 2), ('Nordeste', 'Verão', 43), ('Nordeste', 'Verão', 44), ('Nordeste', 'Verão', 50),
('Nordeste', 'Verão', 33), ('Nordeste', 'Verão', 42), ('Nordeste', 'Verão', 45), ('Nordeste', 'Verão', 58),
('Nordeste', 'Verão', 49), ('Nordeste', 'Verão', 48), ('Nordeste', 'Verão', 15), ('Nordeste', 'Verão', 20),
('Nordeste', 'Verão', 14), ('Nordeste', 'Verão', 6), ('Nordeste', 'Verão', 53),
('Nordeste', 'Outono', 2), ('Nordeste', 'Outono', 35), ('Nordeste', 'Outono', 45), ('Nordeste', 'Outono', 40),
('Nordeste', 'Outono', 12), ('Nordeste', 'Outono', 20), ('Nordeste', 'Outono', 13), ('Nordeste', 'Outono', 42),
('Nordeste', 'Outono', 48), ('Nordeste', 'Outono', 49), ('Nordeste', 'Outono', 59), ('Nordeste', 'Outono', 6),
('Nordeste', 'Outono', 31), ('Nordeste', 'Outono', 24), ('Nordeste', 'Outono', 53),
('Nordeste', 'Inverno', 6), ('Nordeste', 'Inverno', 24), ('Nordeste', 'Inverno', 31), ('Nordeste', 'Inverno', 51),
('Nordeste', 'Inverno', 13), ('Nordeste', 'Inverno', 20), ('Nordeste', 'Inverno', 9), ('Nordeste', 'Inverno', 18),
('Nordeste', 'Inverno', 28), ('Nordeste', 'Inverno', 23), ('Nordeste', 'Inverno', 55), ('Nordeste', 'Inverno', 39),
('Nordeste', 'Inverno', 61), ('Nordeste', 'Inverno', 38), ('Nordeste', 'Inverno', 47),
('Nordeste', 'Primavera', 45), ('Nordeste', 'Primavera', 35), ('Nordeste', 'Primavera', 40), ('Nordeste', 'Primavera', 12),
('Nordeste', 'Primavera', 20), ('Nordeste', 'Primavera', 13), ('Nordeste', 'Primavera', 48), ('Nordeste', 'Primavera', 49),
('Nordeste', 'Primavera', 59), ('Nordeste', 'Primavera', 50), ('Nordeste', 'Primavera', 2), ('Nordeste', 'Primavera', 43),
('Nordeste', 'Primavera', 44), ('Nordeste', 'Primavera', 42), ('Nordeste', 'Primavera', 6);

-- Norte
INSERT INTO `recomendacaocultivo` (
	`regiao`, 
	`estacao`, 
	`id_cultivo`
) VALUES
('Norte', 'Verão', 40), ('Norte', 'Verão', 45), ('Norte', 'Verão', 1), ('Norte', 'Verão', 11), ('Norte', 'Verão', 41),
('Norte', 'Verão', 43), ('Norte', 'Verão', 44), ('Norte', 'Verão', 17), ('Norte', 'Verão', 27), ('Norte', 'Verão', 29),
('Norte', 'Outono', 1), ('Norte', 'Outono', 40), ('Norte', 'Outono', 45), ('Norte', 'Outono', 11), ('Norte', 'Outono', 41),
('Norte', 'Outono', 17), ('Norte', 'Outono', 27), ('Norte', 'Outono', 29),
('Norte', 'Inverno', 1), ('Norte', 'Inverno', 17), ('Norte', 'Inverno', 27), ('Norte', 'Inverno', 29),
('Norte', 'Primavera', 40), ('Norte', 'Primavera', 45), ('Norte', 'Primavera', 1), ('Norte', 'Primavera', 11),
('Norte', 'Primavera', 41), ('Norte', 'Primavera', 43), ('Norte', 'Primavera', 44), ('Norte', 'Primavera', 17),
('Norte', 'Primavera', 27), ('Norte', 'Primavera', 29);

-- Centro-Oeste
INSERT INTO `recomendacaocultivo` (
	`regiao`, 
	`estacao`, 
	`id_cultivo`
) VALUES
('Centro-Oeste', 'Verão', 45), ('Centro-Oeste', 'Verão', 56), ('Centro-Oeste', 'Verão', 37), ('Centro-Oeste', 'Verão', 8),
('Centro-Oeste', 'Verão', 34), ('Centro-Oeste', 'Verão', 59), ('Centro-Oeste', 'Verão', 49), ('Centro-Oeste', 'Verão', 2),
('Centro-Oeste', 'Verão', 43), ('Centro-Oeste', 'Verão', 48), ('Centro-Oeste', 'Verão', 15), ('Centro-Oeste', 'Verão', 50),
('Centro-Oeste', 'Verão', 20), ('Centro-Oeste', 'Verão', 14), ('Centro-Oeste', 'Verão', 3),
('Centro-Oeste', 'Outono', 45), ('Centro-Oeste', 'Outono', 56), ('Centro-Oeste', 'Outono', 34), ('Centro-Oeste', 'Outono', 37),
('Centro-Oeste', 'Outono', 8), ('Centro-Oeste', 'Outono', 59), ('Centro-Oeste', 'Outono', 49), ('Centro-Oeste', 'Outono', 2),
('Centro-Oeste', 'Outono', 48), ('Centro-Oeste', 'Outono', 15), ('Centro-Oeste', 'Outono', 50), ('Centro-Oeste', 'Outono', 20),
('Centro-Oeste', 'Outono', 14), ('Centro-Oeste', 'Outono', 3), ('Centro-Oeste', 'Outono', 43),
('Centro-Oeste', 'Inverno', 6), ('Centro-Oeste', 'Inverno', 31), ('Centro-Oeste', 'Inverno', 24), ('Centro-Oeste', 'Inverno', 53),
('Centro-Oeste', 'Inverno', 16), ('Centro-Oeste', 'Inverno', 26), ('Centro-Oeste', 'Inverno', 52), ('Centro-Oeste', 'Inverno', 19),
('Centro-Oeste', 'Inverno', 10), ('Centro-Oeste', 'Inverno', 20), ('Centro-Oeste', 'Inverno', 14), ('Centro-Oeste', 'Inverno', 30),
('Centro-Oeste', 'Inverno', 34), ('Centro-Oeste', 'Inverno', 54), ('Centro-Oeste', 'Inverno', 39),
('Centro-Oeste', 'Primavera', 45), ('Centro-Oeste', 'Primavera', 56), ('Centro-Oeste', 'Primavera', 37),
('Centro-Oeste', 'Primavera', 8), ('Centro-Oeste', 'Primavera', 34), ('Centro-Oeste', 'Primavera', 59),
('Centro-Oeste', 'Primavera', 49), ('Centro-Oeste', 'Primavera', 2), ('Centro-Oeste', 'Primavera', 48),
('Centro-Oeste', 'Primavera', 15), ('Centro-Oeste', 'Primavera', 50), ('Centro-Oeste', 'Primavera', 20),
('Centro-Oeste', 'Primavera', 14), ('Centro-Oeste', 'Primavera', 3), ('Centro-Oeste', 'Primavera', 43);
commit;

begin;
## EstoqueInsumo
CREATE TABLE `estoqueinsumo` (
	`id_insumo` int NOT NULL AUTO_INCREMENT,
	`nome_insumo` varchar(100) NOT NULL,
	`categoria_insumo` varchar(50) NOT NULL,
    `qtd_insumo` int DEFAULT 0,
    `unidqtd_insumo` varchar(10) DEFAULT 'kg',
	`ativo_insumo` boolean DEFAULT true,
	PRIMARY KEY (`id_insumo`)
);

## PedidoCompra
CREATE TABLE `pedidocompra` (
	`id_pedidocompra` int NOT NULL AUTO_INCREMENT,
	`data_pedidocompra` DATE,
	`id_fornecedor` int NOT NULL,
	PRIMARY KEY (`id_pedidocompra`),
	KEY `id_fornecedor` (`id_fornecedor`),
	CONSTRAINT `pedidocompra_ibfk_1` FOREIGN KEY (`id_fornecedor`) REFERENCES `fornecedor` (`id_fornecedor`)
);

## CompraItem
CREATE TABLE `compraitem` (
	`id_compraitem` int NOT NULL AUTO_INCREMENT,
	`qtd_compraitem` int DEFAULT 0,
	`unidqtd_compraitem` varchar(10) DEFAULT 'kg',
    `valor_compraitem` decimal(9,3) NOT NULL,
	`id_pedidocompra` int NOT NULL,
	`id_insumo` int NOT NULL,
	PRIMARY KEY (`id_compraitem`, `id_pedidocompra`),
	KEY `id_pedidocompra` (`id_pedidocompra`),
	CONSTRAINT `compraitem_ibfk_1` FOREIGN KEY (`id_pedidocompra`) REFERENCES `pedidocompra` (`id_pedidocompra`),
	KEY `id_insumo` (`id_insumo`),
	CONSTRAINT `compraitem_ibfk_2` FOREIGN KEY (`id_insumo`) REFERENCES `estoqueinsumo` (`id_insumo`)
);

-- Criar Trigger para atualizar qtd_insumo após inserir em compraitem
DELIMITER //
CREATE TRIGGER after_insert_compraitem
AFTER INSERT ON compraitem
FOR EACH ROW
BEGIN
    UPDATE estoqueinsumo
    SET qtd_insumo = qtd_insumo + NEW.qtd_compraitem
    WHERE id_insumo = NEW.id_insumo;
END //
DELIMITER ;

## SaidaInsumo
CREATE TABLE `saidainsumo` (
	`id_saidainsumo` int NOT NULL AUTO_INCREMENT,
	`qtd_saidainsumo` int DEFAULT 0,
	`unidqtd_saidainsumo` varchar(10) DEFAULT 'kg',
	`data_saidainsumo` DATE,
	`id_insumo` int NOT NULL,
	PRIMARY KEY (`id_saidainsumo`, `id_insumo`),
	KEY `id_insumo` (`id_insumo`),
	CONSTRAINT `saidainsumo_ibfk_1` FOREIGN KEY (`id_insumo`) REFERENCES `estoqueinsumo` (`id_insumo`)
);

-- Criar Trigger para atualizar qtd_insumo após inserir em saidainsumo
DELIMITER $$
CREATE TRIGGER `verifica_estoque_suficiente`
BEFORE INSERT ON `saidainsumo`
FOR EACH ROW
BEGIN
    DECLARE estoque_atual INT;

    SELECT `qtd_insumo` INTO estoque_atual
    FROM `estoqueinsumo`
    WHERE `id_insumo` = NEW.`id_insumo`;

    IF estoque_atual < NEW.`qtd_saidainsumo` THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Quantidade de saída maior que a do estoque.';
    END IF;
END$$
DELIMITER ;

-- Criar Trigger para atualizar qtd_insumo após inserir em saidainsumo
DELIMITER $$
CREATE TRIGGER `atualiza_estoque_saidainsumo`
AFTER INSERT ON `saidainsumo`
FOR EACH ROW
BEGIN
    UPDATE `estoqueinsumo`
    SET `qtd_insumo` = `qtd_insumo` - NEW.`qtd_saidainsumo`
    WHERE `id_insumo` = NEW.`id_insumo`;
END$$
DELIMITER ;

commit;


begin;

## Producao
CREATE TABLE `producao` (
	`id_producao` int NOT NULL AUTO_INCREMENT,
	`qtd_producao` int NOT NULL,
	`unidqtd_producao` varchar(10) DEFAULT 'kg',
	`data_producao` DATE,
    `datacolheita_producao` DATE,
    `ambientectrl_producao` boolean DEFAULT true,
    `finalizado_producao` boolean DEFAULT false,
	`id_cultivo` int NOT NULL,
	PRIMARY KEY (`id_producao`),
	KEY `id_cultivo` (`id_cultivo`),
	CONSTRAINT `producao_ibfk_1` FOREIGN KEY (`id_cultivo`) REFERENCES `cultivo` (`id_cultivo`)
);

## EstoqueProduto
CREATE TABLE `estoqueproduto` (
	`id_estoqueproduto` int NOT NULL AUTO_INCREMENT,
	`qtd_estoqueproduto` int NOT NULL,
	`unidqtd_estoqueproduto` varchar(10) DEFAULT 'kg',
	`dataentrada_estoqueproduto` DATE,
	`ativo_estoqueproduto` boolean DEFAULT true,
	`id_producao` int NOT NULL,
	PRIMARY KEY (`id_estoqueproduto`),
	KEY `id_producao` (`id_producao`),
	CONSTRAINT `estoqueproduto_ibfk_1` FOREIGN KEY (`id_producao`) REFERENCES `producao` (`id_producao`)
);

-- Criar Trigger para atualizar estoqueproduto após finalizar produção
DELIMITER //
CREATE TRIGGER after_update_producao
AFTER UPDATE ON producao
FOR EACH ROW
BEGIN
    IF NEW.finalizado_producao = true AND OLD.finalizado_producao = false THEN
        INSERT INTO estoqueproduto (qtd_estoqueproduto, unidqtd_estoqueproduto, dataentrada_estoqueproduto, id_producao)
        VALUES (NEW.qtd_producao, NEW.unidqtd_producao, NEW.datacolheita_producao, NEW.id_producao);
    END IF;
END //
DELIMITER ;

## PedidoVenda
CREATE TABLE `pedidovenda` (
	`id_pedidovenda` int NOT NULL AUTO_INCREMENT,
	`data_pedidovenda` DATE,
	`id_cliente` int NOT NULL,
	PRIMARY KEY (`id_pedidovenda`),
	KEY `id_cliente` (`id_cliente`),
	CONSTRAINT `pedidovenda_ibfk_1` FOREIGN KEY (`id_cliente`) REFERENCES `cliente` (`id_cliente`)
);

## VendaItem
CREATE TABLE `vendaitem` (
	`id_vendaitem` int NOT NULL AUTO_INCREMENT,
	`qtd_vendaitem` int NOT NULL,
	`unidqtd_vendaitem` varchar(10) DEFAULT 'kg',
    `valor_vendaitem` decimal(9,3) NOT NULL,
    `desconto_vendaitem` decimal(9,3),
	`id_pedidovenda` int NOT NULL,
	`id_estoqueproduto` int NOT NULL,
	PRIMARY KEY (`id_vendaitem`, `id_pedidovenda`),
	KEY `id_pedidovenda` (`id_pedidovenda`),
	CONSTRAINT `vendaitem_ibfk_1` FOREIGN KEY (`id_pedidovenda`) REFERENCES `pedidovenda` (`id_pedidovenda`),
	KEY `id_estoqueproduto` (`id_estoqueproduto`),
	CONSTRAINT `vendaitem_ibfk_2` FOREIGN KEY (`id_estoqueproduto`) REFERENCES `estoqueproduto` (`id_estoqueproduto`)
);

-- Criar Trigger para definir estoqueproduto como inativo se sua qtd for 0
DELIMITER $$
CREATE TRIGGER tr_update_ativo_estoqueproduto
BEFORE UPDATE ON estoqueproduto
FOR EACH ROW
BEGIN
    -- Verifica se a quantidade está sendo atualizada para 0
    IF NEW.qtd_estoqueproduto = 0 THEN
        SET NEW.ativo_estoqueproduto = FALSE;
    ELSE
        SET NEW.ativo_estoqueproduto = TRUE;
    END IF;
END$$
DELIMITER ;
commit;

use pimfazendaurbana;

## FUNCIONARIO, CLIENTE, FORNECEDOR
## Funcionario:
begin;
INSERT INTO `funcionario` (`nome_funcionario`, `cpf_funcionario`, `sexo_funcionario`, `email_funcionario`, `cargo_funcionario`, `usuario_funcionario`, `senha_funcionario`, `ativo_funcionario`) VALUES
('Alice Silva', '12345678900', 'F', 'alice.silva@unip.br', 'Funcionário', 'alice.s', 'A1!eB3@cD', true),
('Bruno Souza', '23456789011', 'M', 'bruno.souza@unip.br', 'Funcionário', 'bruno.s', 'B2#rU4%uZ', true),
('Carla Dias', '34567890122', 'F', 'carla.dias@unip.br', 'Funcionário', 'carla.d', 'C3$rL5&dI', true),
('Diego Pereira', '45678901233', 'M', 'diego.pereira@unip.br', 'Gerente', 'diego.p', 'D4#eP6&pR', true),
('Eva Martins', '56789012344', 'F', 'eva.martins@unip.br', 'Gerente', 'eva.m', 'E5@mA7&nS', true),
('Felipe Ramos', '67890123455', 'M', 'felipe.ramos@unip.br', 'Funcionário', 'felipe.r', 'F6#lR8*pM', true),
('Giselle Rocha', '78901234566', 'F', 'giselle.rocha@unip.br', 'Funcionário', 'giselle.r', 'G7&lR9@cA', true),
('Hugo Almeida', '89012345677', 'M', 'hugo.almeida@unip.br', 'Funcionário', 'hugo.a', 'H8@uA1#lE', true),
('Isabela Fernandes', '90123456788', 'F', 'isabela.fernandes@unip.br', 'Gerente', 'isabela.f', 'I9#bE2%mN', true),
('João Lima', '01234567899', 'M', 'joao.lima@unip.br', 'Gerente', 'joao.l', 'J0!oL3&pA', true);

INSERT INTO `enderecofuncionario` (`logradouro_endfuncionario`, `numero_endfuncionario`, `complemento_endfuncionario`, `bairro_endfuncionario`, `cidade_endfuncionario`, `uf_endfuncionario`, `cep_endfuncionario`, `ativo_endfuncionario`, `id_funcionario`) VALUES
('Rua Sete de Setembro', '123', 'Apto 101', 'Centro', 'Ribeirão Preto', 'SP', '14010000', true, 1),
('Rua Mariana Junqueira', '456', 'Casa', 'Vila Tibério', 'Ribeirão Preto', 'SP', '14020000', true, 2),
('Rua Conde Afonso Celso', '789', 'Apto 202', 'Jardim Paulista', 'Ribeirão Preto', 'SP', '14090000', true, 3),
('Rua Amador Bueno', '101', NULL, 'Sumarezinho', 'Ribeirão Preto', 'SP', '14055000', true, 4),
('Rua General Osório', '102', 'Apto 303', 'Campos Elíseos', 'Ribeirão Preto', 'SP', '14080000', true, 5),
('Rua Lafaiete', '103', NULL, 'Vila Seixas', 'Ribeirão Preto', 'SP', '14020250', true, 6),
('Rua Chile', '104', 'Apto 404', 'Jardim Sumaré', 'Ribeirão Preto', 'SP', '14025220', true, 7),
('Rua São Sebastião', '105', NULL, 'Jardim São Luiz', 'Ribeirão Preto', 'SP', '14020420', true, 8),
('Rua Álvares Cabral', '106', 'Apto 505', 'Vila Virgínia', 'Ribeirão Preto', 'SP', '14030000', true, 9),
('Rua João Penteado', '107', 'Casa', 'Jardim América', 'Ribeirão Preto', 'SP', '14020110', true, 10);

INSERT INTO `telefonefuncionario` (`ddd_telfuncionario`, `numero_telfuncionario`, `ativo_telfuncionario`, `id_funcionario`) VALUES
('16', '987654321', true, 1),
('16', '976543210', true, 2),
('16', '965432109', true, 3),
('16', '954321098', true, 4),
('16', '943210987', true, 5),
('16', '932109876', true, 6),
('16', '921098765', true, 7),
('16', '910987654', true, 8),
('16', '909876543', true, 9),
('16', '898765432', true, 10);
commit;

## Cliente
begin;
INSERT INTO `cliente` (`nome_cliente`, `email_cliente`, `cnpj_cliente`, `ativo_cliente`) VALUES
('Mercado São João', 'contato@mercadosaojoao.com.br', '12345678000100', true),
('Hortifruti Bom Preço', 'contato@hortifrutibompreco.com.br', '23456789000111', true),
('Verduras & Cia', 'contato@verdurasecia.com.br', '34567890000122', true),
('Armazém do Campo', 'contato@armazemdocampo.com.br', '45678901000133', true),
('Quitanda do Interior', 'contato@quitandadointerior.com.br', '56789012000144', true),
('Feira Livre Natural', 'contato@feiralivrenatural.com.br', '67890123000155', true),
('Empório dos Sabores', 'contato@emporiodossabores.com.br', '78901234000166', true),
('Cantinho Verde', 'contato@cantinhoverde.com.br', '89012345000177', true),
('Supermercado Rural', 'contato@supermercadorural.com.br', '90123456000188', true),
('Frutas Frescas', 'contato@frutasfrescas.com.br', '01234567000199', true);

INSERT INTO `enderecocliente` (`logradouro_endcliente`, `numero_endcliente`, `complemento_endcliente`, `bairro_endcliente`, `cidade_endcliente`, `uf_endcliente`, `cep_endcliente`, `ativo_endcliente`, `id_cliente`) VALUES
('Rua Itacolomi', '1240', NULL, 'Centro', 'Ribeirão Preto', 'SP', '14010050', true, 1),
('Avenida Independência', '200', NULL, 'Jardim América', 'Sertãozinho', 'SP', '14160230', true, 2),
('Rua Bahia', '330', NULL, 'Vila Virgínia', 'Jaboticabal', 'SP', '14870550', true, 3),
('Rua Camilo de Mattos', '440', NULL, 'Jardim Panorama', 'Bebedouro', 'SP', '14701150', true, 4),
('Rua Afonso Pena', '550', 'Loja 2', 'Centro', 'Descalvado', 'SP', '13690000', true, 5),
('Rua Marília', '660', 'Loja 1', 'Centro', 'Pirassununga', 'SP', '13640000', true, 6),
('Rua Rio de Janeiro', '770', NULL, 'Jardim São Luís', 'São José do Rio Pardo', 'SP', '13720000', true, 7),
('Rua Tupi', '880', NULL, 'Jardim São Paulo', 'Mococa', 'SP', '13736300', true, 8),
('Rua Sergipe', '990', NULL, 'Centro', 'Batatais', 'SP', '14300000', true, 9),
('Rua Tocantins', '1000', 'Loja 3', 'Centro', 'Serrana', 'SP', '14150000', true, 10);

INSERT INTO `telefonecliente` (`ddd_telcliente`, `numero_telcliente`, `ativo_telcliente`, `id_cliente`) VALUES
('16', '912345678', true, 1),
('16', '923456789', true, 2),
('16', '934567890', true, 3),
('17', '945678901', true, 4),
('16', '956789012', true, 5),
('19', '967890123', true, 6),
('19', '978901234', true, 7),
('16', '989012345', true, 8),
('16', '990123456', true, 9),
('16', '901234567', true, 10);
commit;

## Fornecedor
begin;
INSERT INTO `fornecedor` (`nome_fornecedor`, `email_fornecedor`, `cnpj_fornecedor`, `ativo_fornecedor`) VALUES
('AgroFertilizantes S.A.', 'contato@agrofertilizantes.com.br', '12345678000100', true),
('Sementes de Qualidade Ltda.', 'contato@sementesdequalidade.com.br', '23456789000111', true),
('Adubo Forte Indústria e Comércio', 'contato@aduboforte.com.br', '34567890000122', true),
('Irrigação Eficiente Ltda.', 'contato@irrigacaoeficiente.com.br', '45678901000133', true),
('Ferramentas Agrícolas & Cia.', 'contato@ferramentasagricolas.com.br', '56789012000144', true),
('Plásticos para Estufas Ltda.', 'contato@plasticosestufas.com.br', '67890123000155', true),
('Pesticidas do Interior', 'contato@pesticidasdointerior.com.br', '78901234000166', true),
('Embalagens Verdes S.A.', 'contato@embalagensverdes.com.br', '89012345000177', true),
('Equipamentos para Agricultura Ltda.', 'contato@equipamentosagricultura.com.br', '90123456000188', true),
('Insumos Naturais do Brasil', 'contato@insumosnaturais.com.br', '01234567000199', true);

INSERT INTO `enderecofornecedor` (`logradouro_endfornecedor`, `numero_endfornecedor`, `complemento_endfornecedor`, `bairro_endfornecedor`, `cidade_endfornecedor`, `uf_endfornecedor`, `cep_endfornecedor`, `ativo_endfornecedor`, `id_fornecedor`) VALUES
('Rua Sebastião Sampaio', '1100', NULL, 'Distrito Industrial 2', 'Ribeirão Preto', 'SP', '14000000', true, 1),
('Rua Fioravante Tordin', '220', 'Galpão 2', 'Distrito Industrial', 'Sertãozinho', 'SP', '14177500', true, 2),
('Rua José Fregonese', '330', NULL, 'Parque Industrial 2', 'Jaboticabal', 'SP', '14870000', true, 3),
('Rua Olívio Franceschini', '440', NULL, 'Distrito Industrial', 'Bebedouro', 'SP', '14708700', true, 4),
('Rua Ernesto Navarro', '550', 'Loja 1', 'Parque Industrial', 'Araraquara', 'SP', '14801395', true, 5),
('Avenida 21 de Março', '660', NULL, 'Distrito Industrial 1', 'Pirassununga', 'SP', '13640000', true, 6),
('Rua das Pimenteiras', '770', 'Sala 3', 'Distrito Industrial 1', 'São José do Rio Pardo', 'SP', '13720000', true, 7),
('Rua dos Coqueiros', '880', NULL, 'Parque Industrial', 'Mococa', 'SP', '13736000', true, 8),
('Rua das Mangueiras', '990', 'Galpão 3', 'Distrito Industrial', 'Batatais', 'SP', '14300000', true, 9),
('Rua 13 de Maio', '1000', NULL, 'Distrito Industrial', 'Serrana', 'SP', '14150000', true, 10);

INSERT INTO `telefonefornecedor` (`ddd_telfornecedor`, `numero_telfornecedor`, `ativo_telfornecedor`, `id_fornecedor`) VALUES
('16', '912345678', true, 1),
('16', '923456789', true, 2),
('16', '934567890', true, 3),
('17', '945678901', true, 4),
('16', '956789012', true, 5),
('19', '967890123', true, 6),
('19', '978901234', true, 7),
('16', '989012345', true, 8),
('16', '990123456', true, 9),
('16', '901234567', true, 10);
commit;

## ESTOQUEINSUMO, SAIDAINSUMO, PEDIDOCOMPRA, COMPRAITEM
begin;
INSERT INTO `estoqueinsumo` (`nome_insumo`, `categoria_insumo`, `qtd_insumo`, `unidqtd_insumo`, `ativo_insumo`) VALUES
('Nitrato de amônio', 'Fertilizantes', 10, 'kg', true),
('Fosfato diamônico', 'Fertilizantes', 10, 'kg', true),
('Sulfato de potássio', 'Fertilizantes', 30, 'kg', true),
('Uréia', 'Fertilizantes', 0, 'kg', true),
('Superfosfato simples', 'Fertilizantes', 0, 'kg', true),
('Fertilizante líquido NPK 15-30-15', 'Fertilizantes', 50, 'l', true),
('Fertilizante granulado NPK 10-10-20', 'Fertilizantes', 40, 'kg', true),
('Abacaxi Pérola', 'Sementes', 10, 'kg', true),
('Abóbora Japonesa', 'Sementes', 30, 'kg', true),
('Abobrinha Menina Brasileira', 'Sementes', 0, 'kg', true),
('Acelga Verde de Verão', 'Sementes', 0, 'kg', true),
('Agrião de Água', 'Sementes', 0, 'kg', true),
('Alface Crespa', 'Sementes', 30, 'kg', true),
('Alface Americana', 'Sementes', 10, 'kg', true),
('Alho Roxo', 'Sementes', 10, 'kg', true),
('Alho-poró Porto Rico', 'Sementes', 0, 'kg', true),
('Banana Prata', 'Sementes', 40, 'kg', true),
('Batata-doce Beauregard', 'Sementes', 0, 'kg', true),
('Beterraba Early Wonder', 'Sementes', 30, 'kg', true),
('Brócolis Calabrês', 'Sementes', 10, 'kg', true),
('Cebolinha Verde Todo o Ano', 'Sementes', 0, 'kg', true),
('Cenoura Nantes', 'Sementes', 20, 'kg', true),
('Coentro Português', 'Sementes', 10, 'kg', true),
('Couve Manteiga', 'Sementes', 0, 'kg', true),
('Couve-flor de Inverno', 'Sementes', 0, 'kg', true),
('Ervilha Early Frosty', 'Sementes', 20, 'kg', true);
commit;

## INSERTS DE COMPRA
begin;
INSERT INTO pedidocompra (data_pedidocompra, id_fornecedor) 
VALUES
('2024-11-20', 1),
('2024-11-21', 2),
('2024-11-22', 3);

INSERT INTO compraitem (qtd_compraitem, unidqtd_compraitem, valor_compraitem, id_pedidocompra, id_insumo) 
VALUES
(10, 'kg', 500.000, 1, 1),
(50, 'unidade', 200.000, 1, 2),
(20, 'l', 300.000, 2, 3);
commit;

begin;
INSERT INTO saidainsumo (qtd_saidainsumo, unidqtd_saidainsumo, data_saidainsumo, id_insumo) 
VALUES
(10, 'kg', '2024-11-21', 1),
(15, 'unidade', '2024-11-22', 2),
(5, 'l', '2024-11-23', 3),
(5, 'l', '2024-11-24', 7);
commit;

BEGIN;
## Trigger que vai criar entradas na tabela estoqueproduto automaticamente logo após as entradas na tabela producao
DELIMITER //
CREATE TRIGGER insertsautomaticos_estoqueproduto
AFTER INSERT ON producao
FOR EACH ROW
BEGIN
    IF NEW.finalizado_producao = true THEN
        INSERT INTO estoqueproduto (qtd_estoqueproduto, unidqtd_estoqueproduto, dataentrada_estoqueproduto, id_producao)
        VALUES (NEW.qtd_producao, NEW.unidqtd_producao, NEW.datacolheita_producao, NEW.id_producao);
    END IF;
END //
DELIMITER ;
COMMIT;

BEGIN;
-- Produções de 2024
INSERT INTO `producao` (`qtd_producao`, `unidqtd_producao`, `data_producao`, `datacolheita_producao`, `ambientectrl_producao`, `finalizado_producao`, `id_cultivo`)
VALUES 
-- Janeiro
(20, 'kg', '2024-01-02', '2024-01-28', TRUE, TRUE, 1),  -- Abacaxi Pérola
(10, 'kg', '2024-01-05', '2024-01-30', FALSE, TRUE, 6), -- Alface Crespa
(15, 'kg', '2024-01-10', '2024-01-31', TRUE, TRUE, 13), -- Beterraba Detroit
(25, 'kg', '2024-01-12', '2024-01-29', TRUE, TRUE, 20), -- Cenoura Brasília
(30, 'kg', '2024-01-20', '2024-02-10', FALSE, TRUE, 44), -- Melão Orange Sherbet

-- Fevereiro
(25, 'kg', '2024-02-01', '2024-02-28', TRUE, TRUE, 2),  -- Abóbora Japonesa
(12, 'kg', '2024-02-03', '2024-02-27', TRUE, TRUE, 8),  -- Algodão BRS 368
(10, 'kg', '2024-02-05', '2024-02-28', FALSE, TRUE, 24), -- Couve Manteiga
(15, 'kg', '2024-02-08', '2024-02-29', TRUE, TRUE, 19), -- Cebolinha Verde
(18, 'kg', '2024-02-10', '2024-03-05', FALSE, TRUE, 17), -- Caju Anão Precoce

-- Março
(20, 'kg', '2024-03-01', '2024-03-30', TRUE, TRUE, 46), -- Morango Camarosa
(30, 'kg', '2024-03-05', '2024-03-28', TRUE, TRUE, 43), -- Melancia Crimson Sweet
(25, 'kg', '2024-03-10', '2024-03-31', FALSE, TRUE, 9),  -- Alho Roxo
(15, 'kg', '2024-03-15', '2024-03-31', TRUE, TRUE, 7),  -- Alface Americana
(10, 'kg', '2024-03-20', '2024-04-10', TRUE, TRUE, 57), -- Tomate Cereja

-- Abril
(20, 'kg', '2024-04-01', '2024-04-20', TRUE, TRUE, 42), -- Maxixe Paulista
(15, 'kg', '2024-04-03', '2024-04-22', FALSE, TRUE, 36), -- Feijão-vagem Macarrão
(12, 'kg', '2024-04-05', '2024-04-25', TRUE, TRUE, 23), -- Coentro Português
(18, 'kg', '2024-04-07', '2024-04-30', FALSE, TRUE, 55), -- Salsinha Crespa
(10, 'kg', '2024-04-10', '2024-05-01', TRUE, TRUE, 33), -- Feijão Carioca

-- Maio
(25, 'kg', '2024-05-01', '2024-05-20', TRUE, TRUE, 3),  -- Abobrinha Menina Brasileira
(18, 'kg', '2024-05-05', '2024-05-25', FALSE, TRUE, 16), -- Brócolis Calabrês
(20, 'kg', '2024-05-10', '2024-05-28', TRUE, TRUE, 11), -- Banana Prata
(15, 'kg', '2024-05-12', '2024-06-01', FALSE, TRUE, 41), -- Maracujá Azedo
(10, 'kg', '2024-05-15', '2024-06-05', TRUE, TRUE, 54), -- Salsa Gigante de Itália

-- Junho
(30, 'kg', '2024-06-01', '2024-06-30', TRUE, TRUE, 50), -- Quiabo Cristal
(12, 'kg', '2024-06-05', '2024-06-25', FALSE, TRUE, 15), -- Berinjela Roxa
(10, 'kg', '2024-06-07', '2024-06-27', TRUE, TRUE, 27), -- Cupuaçu
(15, 'kg', '2024-06-10', '2024-07-01', FALSE, TRUE, 39), -- Manjericão Genovês
(18, 'kg', '2024-06-15', '2024-07-05', TRUE, TRUE, 21), -- Cenoura Nantes

-- Julho
(20, 'kg', '2024-07-01', '2024-07-30', TRUE, TRUE, 14), -- Beterraba Early Wonder
(10, 'kg', '2024-07-05', '2024-07-28', FALSE, TRUE, 25), -- Couve-de-bruxelas
(15, 'kg', '2024-07-07', '2024-07-29', TRUE, TRUE, 52), -- Repolho Roxo de Inverno
(12, 'kg', '2024-07-10', '2024-07-31', TRUE, TRUE, 40), -- Mandioca Branca
(18, 'kg', '2024-07-12', '2024-08-01', FALSE, TRUE, 5), -- Agrião de Água

-- Agosto
(25, 'kg', '2024-08-01', '2024-08-25', TRUE, TRUE, 48), -- Pepino Caipira
(12, 'kg', '2024-08-05', '2024-08-27', FALSE, TRUE, 49), -- Pimentão Amarelo
(15, 'kg', '2024-08-07', '2024-08-28', TRUE, TRUE, 12), -- Batata-doce Beauregard
(18, 'kg', '2024-08-10', '2024-08-30', TRUE, TRUE, 38), -- Hortelã Comum
(10, 'kg', '2024-08-12', '2024-09-01', FALSE, TRUE, 4), -- Acelga Verde

-- Setembro
(20, 'kg', '2024-09-01', '2024-09-28', TRUE, TRUE, 26), -- Couve-flor de Inverno
(15, 'kg', '2024-09-05', '2024-09-29', FALSE, TRUE, 35), -- Feijão Carioca
(12, 'kg', '2024-09-07', '2024-09-30', TRUE, TRUE, 22), -- Chicória Catalonha
(18, 'kg', '2024-09-10', '2024-10-01', TRUE, TRUE, 31), -- Espinafre Gigante de Inverno
(10, 'kg', '2024-09-15', '2024-10-05', FALSE, TRUE, 53), -- Rúcula Cultivada

-- Outubro
(25, 'kg', '2024-10-01', '2024-10-25', TRUE, TRUE, 18), -- Cebola Baia Periforme
(15, 'kg', '2024-10-05', '2024-10-28', FALSE, TRUE, 37), -- Girassol Catissol 01
(20, 'kg', '2024-10-07', '2024-10-30', TRUE, TRUE, 45), -- Milho Doce
(12, 'kg', '2024-10-10', '2024-10-31', TRUE, TRUE, 47), -- Orégano Grego
(18, 'kg', '2024-10-15', '2024-11-01', FALSE, TRUE, 10), -- Alho-poró Porto Rico

-- Novembro
(20, 'kg', '2024-11-01', '2024-11-28', TRUE, TRUE, 28), -- Erva-doce de Mesa
(15, 'kg', '2024-11-05', '2024-11-30', FALSE, TRUE, 32), -- Fava Superaguadulce
(12, 'kg', '2024-11-07', '2024-12-01', TRUE, TRUE, 34), -- Feijão Elettra
(18, 'kg', '2024-11-10', '2024-12-05', TRUE, FALSE, 29), -- Ervilha Douce Provence
(10, 'kg', '2024-11-12', '2024-12-08', FALSE, FALSE, 30); -- Ervilha Early Frosty

COMMIT;

BEGIN;
-- Inserir Pedidos de Venda
INSERT INTO pedidovenda (data_pedidovenda, id_cliente)
VALUES
('2024-01-28', 1), 
('2024-01-30', 2), 
('2024-01-31', 3), 
('2024-01-29', 4), 
('2024-02-10', 5),
('2024-02-28', 6), 
('2024-02-27', 7),
('2024-02-28', 8), 
('2024-02-29', 9), 
('2024-03-05', 10), 
('2024-03-30', 1), 
('2024-03-28', 2), 
('2024-03-31', 3), 
('2024-03-31', 4), 
('2024-04-10', 5),
('2024-04-20', 6),
('2024-04-22', 7),
('2024-04-25', 8),
('2024-04-30', 9), 
('2024-05-01', 10), 
('2024-05-20', 1),
('2024-05-25', 2),
('2024-05-28', 3),
('2024-06-01', 4), 
('2024-06-05', 5),
('2024-06-30', 6),
('2024-06-25', 7),
('2024-06-27', 8),
('2024-07-01', 9),
('2024-07-05', 10),
('2024-07-30', 1),
('2024-07-28', 2),
('2024-07-29', 3),
('2024-07-31', 4),
('2024-08-01', 5),
('2024-08-25', 6),
('2024-08-27', 7),
('2024-08-28', 8),
('2024-08-30', 9),
('2024-09-01', 10),
('2024-09-28', 1),
('2024-09-29', 2),
('2024-09-30', 3),
('2024-10-01', 4),
('2024-10-05', 5),
('2024-10-25', 6),
('2024-10-28', 7),
('2024-10-30', 8),
('2024-10-31', 9),
('2024-11-01', 10);

-- Inserir Itens de Venda Correspondentes
INSERT INTO vendaitem (qtd_vendaitem, unidqtd_vendaitem, valor_vendaitem, desconto_vendaitem, id_pedidovenda, id_estoqueproduto)
SELECT 
    qtd_estoqueproduto, -- quantidade de venda corresponde à quantidade de estoque
    'kg' AS unidqtd_vendaitem, 
    ROUND(50 + (RAND() * 150), 3) AS valor_vendaitem, -- valor aleatório entre 50 e 200
    0 AS desconto_vendaitem, 
    id_pedidovenda, 
    id_estoqueproduto
FROM 
    estoqueproduto
JOIN 
    pedidovenda ON pedidovenda.id_pedidovenda = id_estoqueproduto
WHERE 
    ativo_estoqueproduto = 1; -- Apenas itens ativos
COMMIT;

BEGIN;
-- Atualizar o estoque produto (fora do trigger)
UPDATE estoqueproduto
SET qtd_estoqueproduto = qtd_estoqueproduto - (SELECT SUM(qtd_vendaitem) 
                                                FROM vendaitem 
                                                WHERE vendaitem.id_estoqueproduto = estoqueproduto.id_estoqueproduto)
WHERE id_estoqueproduto IN (SELECT id_estoqueproduto FROM vendaitem);

COMMIT;

## EXECUTAR DEPOIS DOS INSERTS DE VENDA:
begin;
-- Criar Trigger para atualizar estoqueproduto após cadastrar venda
DELIMITER //
CREATE TRIGGER after_insert_vendaitem
AFTER INSERT ON vendaitem
FOR EACH ROW
BEGIN
    UPDATE estoqueproduto
    SET qtd_estoqueproduto = qtd_estoqueproduto - NEW.qtd_vendaitem
    WHERE id_estoqueproduto = NEW.id_estoqueproduto;
END //
DELIMITER ;

commit;
