using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class UsuarioSbePermissao
    {

        //Propriedades
        private static readonly string StringConnection;
        public int IdUsuario { get; set; }
        public int IdPermissao { get; set; }


        static UsuarioSbePermissao()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        // Metodos 
        public UsuarioSbePermissao(int idUsuario, int idPermissao)
        {
            IdUsuario = idUsuario;
            IdPermissao = idPermissao;
        }

        private UsuarioSbePermissao(MySqlDataReader leitor)
        {
            IdUsuario = Convert.ToInt32(leitor["id_usuario"]);
            IdPermissao = Convert.ToInt32(leitor["id_permissao"]);
        }


        public static bool Possui(int idUsuario, int idPermissao)
        {
            bool resulatdo = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT pu.* ");
                strQuery.Append("FROM sbessencial.tbl_permissao_usuario pu ");
                strQuery.Append("JOIN sbessencial.tbl_usuario u ON pu.id_usuario = u.idUsuario ");
                strQuery.Append("WHERE id_usuario = @idUsuario ");
                strQuery.Append("AND id_permissao = @idPermissao ");
                strQuery.Append("AND u.idStatus = 1 ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                comando.Parameters.AddWithValue("@idPermissao", idPermissao);

                var reader = comando.ExecuteReader();
                resulatdo = reader.Read();
            }
            return resulatdo;
        }

        public static bool Inserir(int idUsuario, int idPermissao)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_permissao_usuario ");
                strQuery.Append("(id_usuario, id_permissao) ");
                strQuery.Append("VALUES (@id_usuario, @id_permissao ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_usuario", idUsuario);
                    comando.Parameters.AddWithValue("@id_permissao", idPermissao);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Apagar(int idUsuario, int idPermissao)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_permissao_usuario ");
                strQuery.Append("WHERE id_usuario = @id_usuario ");
                strQuery.Append("AND id_permissao = @id_permissao ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_usuario", idUsuario);
                    comando.Parameters.AddWithValue("@id_permissao", idPermissao);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }



    }
}
