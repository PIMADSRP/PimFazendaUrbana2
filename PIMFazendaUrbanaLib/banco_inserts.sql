use pimfazendaurbana;

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
SELECT f.id_funcionario, f.nome_funcionario, f.cpf_funcionario, f.sexo_funcionario, f.email_funcionario,
       f.cargo_funcionario, f.usuario_funcionario, f.senha_funcionario, f.ativo_funcionario,
       ef.logradouro_endfuncionario, ef.numero_endfuncionario, ef.complemento_endfuncionario, ef.bairro_endfuncionario,
       ef.cidade_endfuncionario, ef.uf_endfuncionario, ef.cep_endfuncionario,
       tf.ddd_telfuncionario, tf.numero_telfuncionario
FROM funcionario f
JOIN enderecofuncionario ef ON f.id_funcionario = ef.id_funcionario
JOIN telefonefuncionario tf ON f.id_funcionario = tf.id_funcionario;

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
SELECT c.id_cliente, c.nome_cliente, c.email_cliente, c.cnpj_cliente, c.ativo_cliente,
       ec.logradouro_endcliente, ec.numero_endcliente, ec.complemento_endcliente, ec.bairro_endcliente,
       ec.cidade_endcliente, ec.uf_endcliente, ec.cep_endcliente,
       tc.ddd_telcliente, tc.numero_telcliente
FROM cliente c
JOIN enderecocliente ec ON c.id_cliente = ec.id_cliente
JOIN telefonecliente tc ON c.id_cliente = tc.id_cliente;

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
('Rua das Mangueiras', '990', 'Galpão 3', 'Distrito Industrial', 'Batatais', 'SP', '14300-00', true, 9),
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

begin;
## Producao
INSERT INTO `producao` 
(`qtd_producao`, `unidqtd_producao`, `data_producao`, `datacolheita_producao`, `ambientectrl_producao`, `finalizado_producao`, `id_cultivo`) 
VALUES 
(10, 'Kg', '2024-11-20 08:00:00', '2024-12-15 10:00:00', true, false, 1),
(20, 'Kg', '2024-10-10 14:00:00', '2024-11-10 16:00:00', false, true, 2),
(100, 'Unid', '2024-09-05 09:30:00', '2024-10-05 11:45:00', true, false, 3);
commit;