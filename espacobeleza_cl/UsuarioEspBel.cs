using CriptografiaSgpm;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace espacobeleza_cl
{
    public class UsuarioEspBel
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public int IdEmpresaContratante { get; set; }
        public int IdProfissional { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Img { get; set; }

        public string Status { get; set; }
        public string EmpresaContratante { get; set; }

        static UsuarioEspBel()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        // Metodos 
        public UsuarioEspBel() { }

        private UsuarioEspBel(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idUsuario"]);
            IdEmpresaContratante = Convert.ToInt32(leitor["idEmpresaContratante"]);
            IdProfissional = Convert.ToInt32(leitor["idProfissional"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            Login = leitor["login"].ToString();
            Senha = leitor["senha"].ToString();
            Nome = leitor["nome"].ToString();
            Img = leitor["img"].ToString();

            Status = leitor["descStatus"].ToString();
            EmpresaContratante = leitor["nome_empresa_contratante"].ToString();
        }


        public static bool Inserir(UsuarioEspBel user)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_usuario ");
                strQuery.Append("(login, senha, nome, idEmpresaContratante, idProfissional) ");
                strQuery.Append("VALUES (@login, @senha, @nome, @idEmpresaContratante, @idProfissional) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@login", user.Login);
                    comando.Parameters.AddWithValue("@nome", user.Nome);
                    comando.Parameters.AddWithValue("@senha", user.Senha);
                    comando.Parameters.AddWithValue("@idUsuario", user.Id);
                    comando.Parameters.AddWithValue("@idProfissional", user.IdProfissional);
                    comando.Parameters.AddWithValue("@idEmpresaContratante", user.IdEmpresaContratante);

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
                strQuery.Append("UPDATE  bdespacobeleza.tbl_usuario SET ");
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


        public static bool Editar(UsuarioEspBel user)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_usuario SET ");
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


        public static UsuarioEspBel Pesquisar(int id)
        {
            UsuarioEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_usuario ");
                strQuery.Append("WHERE idUsuario = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new UsuarioEspBel(reader);
            }
            return usuario;
        }

        public static UsuarioEspBel Pesquisar(string login)
        {
            UsuarioEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_usuario ");
                strQuery.Append("WHERE login = @login");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@login", login);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new UsuarioEspBel(reader);
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
                strQuery.Append("FROM bdespacobeleza.tbl_usuario ");
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
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_usuario_erro_login ");
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
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_usuario_erro_login ");
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
                strQuery.Append("FROM bdespacobeleza.tbl_usuario_erro_login ");
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


        public static List<UsuarioEspBel> Lista(string nome)
        {
            var lista = new List<UsuarioEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.vw_usuario ");

                strQuery.Append("WHERE NOME LIKE '%" + nome + "%' ");

                strQuery.Append("ORDER BY NOME ");

                //if (comSelecione)
                //    lista.Add(new Cliente(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new UsuarioEspBel(reader));
            }
            return lista;
        }


    }
}
