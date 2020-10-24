using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class Profissional
    {

        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        static Profissional()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        public Profissional()
        {

        }

        public Profissional(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }


        public Profissional(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            Nome = leitor["nome"].ToString();
            Telefone = leitor["telefone"].ToString();
        }


        public static List<Profissional> Lista(bool comSelecione)
        {
            var lista = new List<Profissional>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_profissional ");
                strQuery.Append("WHERE idStatus = 1 ");

                strQuery.Append("ORDER BY nome ");

                if (comSelecione)
                    lista.Add(new Profissional(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Profissional(reader));
            }
            return lista;
        }


        public static List<Profissional> Lista(int idStatus)
        {
            var lista = new List<Profissional>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_profissional ");

                if (idStatus > 0)
                    strQuery.Append("WHERE idStatus = @idStatus ");

                strQuery.Append("ORDER BY nome ");


                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Profissional(reader));
            }
            return lista;
        }


        public static List<Profissional> Lista( string nome)
        {
            var lista = new List<Profissional>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_profissional ");

                strQuery.Append("WHERE NOME LIKE '%" + nome + "%' ");

                strQuery.Append("ORDER BY NOME ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Profissional(reader));
            }
            return lista;
        }


        public static Profissional Pesquisar(int id)
        {
            Profissional usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_profissional ");
                strQuery.Append("WHERE id = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Profissional(reader);
            }
            return usuario;
        }


        public static Profissional Pesquisar(string nome)
        {
            Profissional usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_profissional ");
                strQuery.Append("WHERE nome = @nome");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@nome", nome);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Profissional(reader);
            }
            return usuario;
        }


        public static string PesquisaNome(int id)
        {
            string usuario = string.Empty;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_profissional ");
                strQuery.Append("WHERE id = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = reader["nome"].ToString();
            }
            return usuario;
        }


        public static bool Inserir(Profissional profissional)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_profissional ");
                strQuery.Append("( idStatus, nome, telefone) ");
                strQuery.Append("VALUES ( 1, @Nome, @telefone) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@telefone", profissional.Telefone);
                    comando.Parameters.AddWithValue("@Nome", profissional.Nome);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int InserirNomeRetornndoId(string nome)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_profissional ");
                strQuery.Append("( idStatus, nome) ");
                strQuery.Append("VALUES ( 1, @Nome) ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@Nome", nome);

                    resultado = Convert.ToInt32(comando.ExecuteScalar());

                    //var insert = comando.ExecuteNonQuery();
                    //if(insert == 1)
                    //  resultado = comando.LastInsertedId;

                }
            }
            return resultado;
        }


        public static bool Editar(Profissional profissional)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_profissional SET ");
                strQuery.Append(" Nome = @Nome ");
                strQuery.Append(", idStatus = @idStatus ");
                strQuery.Append(", telefone = @telefone ");
                strQuery.Append("WHERE id = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", profissional.Id);
                    comando.Parameters.AddWithValue("@idStatus", profissional.IdStatus);
                    comando.Parameters.AddWithValue("@Nome", profissional.Nome);
                    comando.Parameters.AddWithValue("@telefone", profissional.Telefone);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }



    }


}
