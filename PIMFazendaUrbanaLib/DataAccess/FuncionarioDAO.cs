using MySql.Data.MySqlClient;
using System.Data;

namespace PIMFazendaUrbanaLib
{
    public class FuncionarioDAO
    {
        private string connectionString;
        public FuncionarioDAO()
        {
            connectionString = ConnectionString.GetConnectionString();
        }


        public Funcionario AutenticarFuncionario(string funcionarioUsuario, string funcionarioSenha)
        {
            Funcionario funcionario = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT f.id_funcionario, f.nome_funcionario, f.sexo_funcionario, f.email_funcionario, f.cpf_funcionario, 
                                f.cargo_funcionario, f.usuario_funcionario, f.ativo_funcionario, 
                                t.ddd_telfuncionario, t.numero_telfuncionario, t.ativo_telfuncionario, 
                                e.logradouro_endfuncionario, e.numero_endfuncionario, e.complemento_endfuncionario, e.bairro_endfuncionario, e.cidade_endfuncionario, 
                                e.uf_endfuncionario, e.cep_endfuncionario, e.ativo_endfuncionario
                                FROM funcionario f
                                LEFT JOIN telefonefuncionario t ON f.id_funcionario = t.id_funcionario
                                LEFT JOIN enderecofuncionario e ON f.id_funcionario = e.id_funcionario
                                WHERE f.usuario_funcionario = @Usuario AND f.senha_funcionario = @Senha";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Usuario", funcionarioUsuario);
                command.Parameters.AddWithValue("@Senha", funcionarioSenha);

                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                {
                    if (reader.Read())
                    {
                        funcionario = new Funcionario
                        {
                            Id = reader.GetInt32("id_funcionario"),
                            Nome = reader.GetString("nome_funcionario"),
                            Sexo = reader.GetString("sexo_funcionario"),
                            Email = reader.GetString("email_funcionario"),
                            CPF = reader.GetString("cpf_funcionario"),
                            Cargo = reader.GetString("cargo_funcionario"),
                            Usuario = funcionarioUsuario,
                            StatusAtivo = reader.GetBoolean("ativo_funcionario"),
                            Telefone = new Telefone
                            {
                                DDD = reader.GetString("ddd_telfuncionario"),
                                Numero = reader.GetString("numero_telfuncionario"),
                                StatusAtivo = reader.GetBoolean("ativo_telfuncionario")
                            },
                            Endereco = new Endereco
                            {
                                Logradouro = reader.GetString("logradouro_endfuncionario"),
                                Numero = reader.GetString("numero_endfuncionario"),
                                Complemento = reader.IsDBNull("complemento_endfuncionario") ? null : reader.GetString("complemento_endfuncionario"),
                                Bairro = reader.GetString("bairro_endfuncionario"),
                                Cidade = reader.GetString("cidade_endfuncionario"),
                                UF = reader.GetString("uf_endfuncionario"),
                                CEP = reader.GetString("cep_endfuncionario"),
                                StatusAtivo = reader.GetBoolean("ativo_endfuncionario")
                            }
                        };
                    }
                    return funcionario;
                }
            }
        }
    }
}
