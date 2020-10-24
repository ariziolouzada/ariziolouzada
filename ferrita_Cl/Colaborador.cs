using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ferrita_Cl
{
    public class Colaborador
    {
        private static readonly string stringConnection;

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int IdStatus { get; set; }
        public DateTime DataNascto { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Sexo { get; set; }

        public string EndLogradouro { get; set; }
        public string Endbairro { get; set; }
        public string EndComplemento { get; set; }
        public string EndCidade { get; set; }
        public string EndUf { get; set; }
        public string EndNumero { get; set; }
        public string EndCep { get; set; }


        static Colaborador()
        {
            stringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        public Colaborador() { }


        public Colaborador(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id_colaborador"]);
            IdStatus = Convert.ToInt32(leitor["ID_STATUS"]);
            Nome = leitor["nome_completo"].ToString();
            Telefone = leitor["telefone_contatos"].ToString();
            Email = leitor["email"].ToString();
            Facebook = leitor["facebook"].ToString();
            Sexo = leitor["sexo"].ToString();
            Cpf = leitor["cpf"] != DBNull.Value ? leitor["cpf"].ToString() : "";

            EndLogradouro = leitor["end_logradouro"].ToString();
            Endbairro = leitor["end_bairro"].ToString();
            EndComplemento = leitor["end_complemento"].ToString();
            EndCidade = leitor["end_cidade"].ToString();
            EndUf = leitor["end_estado"].ToString();
            EndNumero = leitor["end_numero"].ToString();
            EndCep = leitor["end_cep"].ToString();
        }

       
        public static List<Colaborador> Lista()
        {
            var resultado = new List<Colaborador>();
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_colaborador ");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new Colaborador(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }

        public static List<Colaborador> Lista(string nome)
        {
            var resultado = new List<Colaborador>();
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_colaborador ");
                sql.AppendLine(string.Format("WHERE nome_completo like '%{0}%' ", nome));

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new Colaborador(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }


        //public static List<Fornecedor> ListaFornecedores(bool comSelecione)
        //{
        //    var resultado = new List<Colaborador>();
        //    using (var conexao = new MySqlConnection(stringConnection))
        //    {
        //        StringBuilder sql = new StringBuilder();
        //        sql.AppendLine("SELECT * ");
        //        sql.AppendLine("FROM tbl_ferrita_colaborador ");

        //        if (comSelecione)
        //            resultado.Add(new Fornecedor(0, "Selecione..."));

        //        using (var comando = new MySqlCommand(sql.ToString(), conexao))
        //        {
        //            using (conexao)
        //            {
        //                conexao.Open();//Abrir a conexão com o BD
        //                //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
        //                MySqlDataReader drEvento = comando.ExecuteReader();
        //                while (drEvento.Read())
        //                {
        //                    resultado.Add(new Fornecedor(drEvento));
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}


        public static Colaborador PesquisaPeloId(int id)
        {
            Colaborador resultado = null;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * ");
                sql.AppendLine("FROM tbl_ferrita_colaborador ");
                sql.AppendLine("WHERE id_colaborador = @id_colaborador");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        comando.Parameters.AddWithValue("@id_colaborador", id);
                        MySqlDataReader drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado = new Colaborador(drEvento);
                        }
                    }
                }
            }
            return resultado;

        }


        public static bool Inserir(Colaborador colab)
        {
            bool ok = false;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                var sql = new StringBuilder();
                sql.Append("INSERT INTO tbl_ferrita_colaborador ");
                sql.Append("(id_status, nome_completo, cpf, email, facebook, end_logradouro, end_numero, end_cep" +
                    ", end_complemento, end_bairro, end_cidade, end_estado, telefone_contatos, data_nascto, sexo) ");
                sql.Append("VALUES (1, @nome_completo, @cpf, @email, @facebook, @end_logradouro, @end_numero, @end_cep" +
                    ", @end_complemento, @end_bairro, @end_cidade, @end_estado, @telefone_contatos, @data_nascto, @sexo)");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();
                        //comando.Parameters.AddWithValue("@id_status", colab.IdStatus);
                        comando.Parameters.AddWithValue("@nome_completo",       colab.Nome          );
                        comando.Parameters.AddWithValue("@cpf",                 colab.Cpf           );
                        comando.Parameters.AddWithValue("@telefone_contatos",   colab.Telefone      );
                        comando.Parameters.AddWithValue("@email",               colab.Email         );
                        comando.Parameters.AddWithValue("@facebook",            colab.Facebook      );
                        comando.Parameters.AddWithValue("@end_logradouro",      colab.EndLogradouro );
                        comando.Parameters.AddWithValue("@end_numero",          colab.EndNumero     );
                        comando.Parameters.AddWithValue("@end_complemento",     colab.EndComplemento);
                        comando.Parameters.AddWithValue("@end_bairro",          colab.Endbairro     );
                        comando.Parameters.AddWithValue("@end_cidade",          colab.EndCidade     );
                        comando.Parameters.AddWithValue("@end_estado",          colab.EndUf         );
                        comando.Parameters.AddWithValue("@end_cep",             colab.EndCep        );
                        comando.Parameters.AddWithValue("@data_nascto",         colab.DataNascto    );
                        comando.Parameters.AddWithValue("@sexo",                colab.Sexo          );

                        var inseriu = comando.ExecuteNonQuery();
                        if (inseriu == 1)
                            ok = true;
                    }
                }
            }
            return ok;
        }


        public static bool Update(Colaborador colab)
        {
            bool ok = false;
            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE tbl_ferrita_colaborador SET ");
                sql.AppendLine("nome_completo = @nome_completo, cpf = @cpf, email = @email, facebook = @facebook ");
                sql.AppendLine(", id_status = @id_status, telefone_contatos = @telefone_contatos, sexo = @sexo, end_cep = @end_cep ");
                sql.AppendLine(", end_logradouro = @end_logradouro, end_numero = @end_numero  , end_bairro = @end_bairro ");
                sql.AppendLine(", end_complemento = @end_complemento, end_cidade = @end_cidade , end_estado = @end_estado ");
                sql.AppendLine("WHERE id_colaborador = @id_colaborador");

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();

                        comando.Parameters.AddWithValue("@id_colaborador", colab.Id);
                        comando.Parameters.AddWithValue("@nome_completo", colab.Nome);
                        comando.Parameters.AddWithValue("@cpf", colab.Cpf);
                        comando.Parameters.AddWithValue("@id_status", colab.IdStatus);
                        comando.Parameters.AddWithValue("@telefone_contatos", colab.Telefone);
                        comando.Parameters.AddWithValue("@email", colab.Email);
                        comando.Parameters.AddWithValue("@facebook", colab.Facebook);
                        comando.Parameters.AddWithValue("@end_logradouro", colab.EndLogradouro);
                        comando.Parameters.AddWithValue("@end_numero", colab.EndNumero);
                        comando.Parameters.AddWithValue("@end_complemento", colab.EndComplemento);
                        comando.Parameters.AddWithValue("@end_bairro", colab.Endbairro);
                        comando.Parameters.AddWithValue("@end_cidade", colab.EndCidade);
                        comando.Parameters.AddWithValue("@end_estado", colab.EndUf);
                        comando.Parameters.AddWithValue("@end_cep", colab.EndCep);
                        comando.Parameters.AddWithValue("@data_nascto", colab.DataNascto);
                        comando.Parameters.AddWithValue("@sexo", colab.Sexo);
                        var update = comando.ExecuteNonQuery();
                        if (update == 1)
                            ok = true;
                    }
                }
            }
            return ok;
        }



    }
}
