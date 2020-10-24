using CriptografiaSgpm;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class UsuarioSbe
    {

        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Img { get; set; }

        public string Status { get; set; }

        static UsuarioSbe()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        // Metodos 
        public UsuarioSbe() { }

        private UsuarioSbe(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idUsuario"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            Login = leitor["login"].ToString();
            Senha = leitor["senha"].ToString();
            Nome = leitor["nome"].ToString();            
            Img = leitor["img"].ToString();           
            Status = leitor["desc_statsu_usuario"].ToString();
        }


        public static bool Inserir(UsuarioSbe user)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_usuario ");
                strQuery.Append("(login, senha, nome) ");
                strQuery.Append("VALUES (@login, @senha, @nome) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@login", user.Login);
                    comando.Parameters.AddWithValue("@nome", user.Nome);
                    comando.Parameters.AddWithValue("@senha", user.Senha);
                    comando.Parameters.AddWithValue("@idUsuario", user.Id);

                    var senhaCriptografada = Criptografia.Encrypt(user.Senha);
                    comando.Parameters.AddWithValue("@senha", senhaCriptografada);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool EditarSenha(long idUsuario, string senha)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE  sbessencial.tbl_usuario SET ");
                strQuery.Append("senha = @senha ");
                strQuery.Append("WHERE idUsuario = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", idUsuario);
                    comando.Parameters.AddWithValue("@senha", senha);
                    var update = comando.ExecuteNonQuery();
                    resultado = update == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(UsuarioSbe user)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_usuario SET ");
                strQuery.Append(" nome = @nome ");
                strQuery.Append(", login = @login ");
                strQuery.Append(", idStatus = @idStatus ");
                strQuery.Append("WHERE idUsuario = @idUsuario ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idUsuario", user.Id);
                    comando.Parameters.AddWithValue("@nome", user.Nome);
                    comando.Parameters.AddWithValue("@login", user.Login);
                    comando.Parameters.AddWithValue("@idStatus", user.IdStatus);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static UsuarioSbe Pesquisar(int id)
        {
            UsuarioSbe usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_usuario ");
                strQuery.Append("WHERE idUsuario = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new UsuarioSbe(reader);
            }
            return usuario;
        }

        public static UsuarioSbe Pesquisar(string login)
        {
            UsuarioSbe usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_usuario ");
                strQuery.Append("WHERE login = @login");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@login", login);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new UsuarioSbe(reader);
            }
            return usuario;
        }

        public static bool AutenticacaoUsuario(string login, string senha)
        {
            bool resultado = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_usuario ");
                strQuery.Append("WHERE login = @login ");
                strQuery.Append("AND senha = @senha ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@login", login);
                comando.Parameters.AddWithValue("@senha", senha);
                var reader = comando.ExecuteReader();
                resultado = reader.Read();
            }
            return resultado;
        }



        // Tabela usuario erro login

        public static bool InserirErroLogin(int idUsuario)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_usuario_erro_login ");
                strQuery.Append("(id_usuario, dataHora) ");
                strQuery.Append("VALUES (@id_usuario, @dataHora) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_usuario", idUsuario);
                    comando.Parameters.AddWithValue("@dataHora", DateTime.Now);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool ApagarErroLogin(int idUsuario)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_usuario_erro_login ");
                strQuery.Append("where id_usuario = @id_usuario ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_usuario", idUsuario);
                    var apagou = comando.ExecuteNonQuery();
                    resultado = apagou > 0;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int QtdeErroLogin(int idUsuario)
        {
           var qtde = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT  COUNT(id_usuario) AS qtde ");
                strQuery.Append("FROM sbessencial.tbl_usuario_erro_login ");
                strQuery.Append("where id_usuario = @id_usuario ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id_usuario", idUsuario);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    qtde = Convert.ToInt32(reader["qtde"]);
            }
            return qtde;
        }


        public static List<UsuarioSbe> Lista(string nome)
        {
            var lista = new List<UsuarioSbe>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_usuario ");

                strQuery.Append("WHERE NOME LIKE '%" + nome + "%' ");

                strQuery.Append("ORDER BY NOME ");

                //if (comSelecione)
                //    lista.Add(new Cliente(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new UsuarioSbe(reader));
            }
            return lista;
        }

    }
}
