using CriptografiaSgpm;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Text;

namespace ariziolouzada.classes
{
    public class Usuario
    {

        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Cargo { get; set; }
        public string Funcao { get; set; }
        public string Gerencia { get; set; }
        public string Email { get; set; }
        public string Imagem { get; set; }
        public string Chave { get; set; }


        static Usuario()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        public Usuario() { }

        private Usuario(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            Login = leitor["login"].ToString();
            Senha = leitor["senha"].ToString();
            Nome = leitor["nome"].ToString();
            Matricula = leitor["matricula"].ToString();
            Cargo = leitor["cargo"].ToString();
            Funcao = leitor["funcao"].ToString();
            Gerencia = leitor["gerencia"].ToString();
            Email = leitor["email"].ToString();
            Chave = leitor["chave"].ToString();
            Imagem = leitor["imgUsuario"].ToString();
        }


        public static bool Inserir(Usuario user)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_ast_usuario ");
                strQuery.Append("(login, senha, nome, matricula, cargo, funcao, gerencia, email) ");
                strQuery.Append("VALUES (@login, @senha, @nome, @matricula, @cargo, @funcao, @gerencia, @email) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@login", user.Login);
                    comando.Parameters.AddWithValue("@nome", user.Nome);
                    comando.Parameters.AddWithValue("@matricula", user.Matricula);
                    comando.Parameters.AddWithValue("@cargo", user.Cargo);
                    comando.Parameters.AddWithValue("@funcao", user.Funcao);
                    comando.Parameters.AddWithValue("@gerencia", user.Gerencia);
                    comando.Parameters.AddWithValue("@email", user.Email);

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
                strQuery.Append("UPDATE  bdepcvrd.tbl_ast_usuario SET ");
                strQuery.Append("senha = @senha ");
                strQuery.Append("WHERE id = @id ");

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


        //public static bool EditarCliente(Cliente cliente)
        //{
        //    bool resultado;
        //    using (var conexao = new MySqlConnection(StringConnection))
        //    {
        //        var strQuery = new StringBuilder();
        //        strQuery.Append("UPDATE tbl_cliente SET ");
        //        strQuery.Append("NOME_CLIENTE = @NOME_CLIENTE, CPF = @CPF, DATA_NASCTO = @DATA_NASCTO, EMAIL = @EMAIL ");
        //        strQuery.Append(", TEL_FIXO = @TEL_FIXO ,TEL_CEL_1 = @TEL_CEL_1 ,TEL_CEL_2 = @TEL_CEL_2 ");
        //        strQuery.Append(", FACEBOOK = @FACEBOOK , END_LOGRADOURO = @END_LOGRADOURO, END_PTO_REF = @END_PTO_REF ");
        //        strQuery.Append(", END_BAIRRO = @END_BAIRRO, END_CIDADE = @END_CIDADE, UF = @UF ,CEP = @CEP  ");
        //        strQuery.Append(", ID_STATUS = @ID_STATUS WHERE ID_CLIENTE = @ID_Cliente ");

        //        using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
        //        {
        //            conexao.Open();//Abrir a conexão com o BD
        //            comando.Parameters.AddWithValue("@ID_CLIENTE", cliente.IdCliente);
        //            comando.Parameters.AddWithValue("@NOME_CLIENTE", cliente.Nome);
        //            comando.Parameters.AddWithValue("@CPF", cliente.Cpf);
        //            comando.Parameters.AddWithValue("@DATA_NASCTO", cliente.DataNescto);
        //            comando.Parameters.AddWithValue("@EMAIL", cliente.Email);
        //            comando.Parameters.AddWithValue("@TEL_FIXO", cliente.TelFixo);
        //            comando.Parameters.AddWithValue("@TEL_CEL_1", cliente.TelCel1);
        //            comando.Parameters.AddWithValue("@TEL_CEL_2", cliente.TelCel2);
        //            comando.Parameters.AddWithValue("@FACEBOOK", cliente.Facebook);
        //            comando.Parameters.AddWithValue("@END_LOGRADOURO", cliente.EndLogradouro);
        //            comando.Parameters.AddWithValue("@END_PTO_REF", cliente.EndPtoReferencia);
        //            comando.Parameters.AddWithValue("@END_BAIRRO", cliente.EndBairro);
        //            comando.Parameters.AddWithValue("@END_CIDADE", cliente.EndCidade);
        //            comando.Parameters.AddWithValue("@UF", cliente.Uf);
        //            comando.Parameters.AddWithValue("@CEP", cliente.Cep);
        //            comando.Parameters.AddWithValue("@ID_STATUS", cliente.IdStatus);
        //            var update = comando.ExecuteNonQuery();
        //            resultado = update == 1;//Convert.ToInt64(insert);
        //        }
        //    }
        //    return resultado;
        //}

        //public static bool ExcluirCliente(long idCliente)
        //{
        //    bool resultado;
        //    using (var conexao = new MySqlConnection(StringConnection))
        //    {
        //        var strQuery = new StringBuilder();
        //        strQuery.Append("DELETE FROM tbl_cliente ");
        //        strQuery.Append("WHERE ID_CLIENTE = @ID_CLIENTE ");

        //        using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
        //        {
        //            conexao.Open();//Abrir a conexão com o BD
        //            comando.Parameters.AddWithValue("@ID_CLIENTE", idCliente);
        //            var delete = comando.ExecuteNonQuery();
        //            resultado = delete == 1;//Convert.ToInt64(insert);
        //        }
        //    }
        //    return resultado;
        //}

        //public static List<Cliente> ListaDeClientes(string nome, string cpf, string email, string facebook, int idStatus)
        //{
        //    var lista = new List<Cliente>();
        //    using (var conexao = new MySqlConnection(StringConnection))
        //    {
        //        var strQuery = new StringBuilder();
        //        strQuery.Append("SELECT C.* ");
        //        strQuery.Append("FROM vw_cliente C ");
        //        strQuery.Append("WHERE NOME_CLIENTE LIKE '%" + nome + "%' ");
        //        strQuery.Append("AND CPF  LIKE '%" + cpf + "%' ");
        //        strQuery.Append("AND EMAIL  LIKE '%" + email + "%' ");
        //        strQuery.Append("AND FACEBOOK  LIKE '%" + facebook + "%' ");

        //        if (idStatus > 0)
        //            strQuery.Append("AND ID_STATUS  = @idStatus ");

        //        conexao.Open();//Abrir a conexão com o BD
        //        var comando = new MySqlCommand(strQuery.ToString(), conexao);

        //        if (idStatus > 0)
        //            comando.Parameters.AddWithValue("@idStatus", idStatus);

        //        var reader = comando.ExecuteReader();
        //        while (reader.Read())
        //            lista.Add(new Cliente(reader));
        //    }
        //    return lista;
        //}

        public static Usuario Pesquisar(int id)
        {
            Usuario usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM tbl_ast_usuario ");
                strQuery.Append("WHERE id = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Usuario(reader);
            }
            return usuario;
        }

        public static Usuario Pesquisar(string login)
        {
            Usuario usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM tbl_ast_usuario ");
                strQuery.Append("WHERE login = @login");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@login", login);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Usuario(reader);
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
                strQuery.Append("FROM bdepcvrd.tbl_ast_usuario ");
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

    }
}
