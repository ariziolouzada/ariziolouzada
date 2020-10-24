using CriptografiaSgpm;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbessencial_cl
{
    public class Cliente
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascto { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string TelCelular1 { get; set; }
        public string TelCelular2 { get; set; }
        public string TelFixo { get; set; }
        public string Observacao { get; set; }
        public string Senha { get; set; }

        public string LoginCadastro { get; set; }
        public DateTime DataCadastro { get; set; }


        static Cliente()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        // Metodos 
        public Cliente() { }


        private Cliente(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idCliente"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            Nome = leitor["Nome"].ToString();
            DataNascto = leitor["DataNascto"] != DBNull.Value ? Convert.ToDateTime(leitor["DataNascto"]) : DateTime.MinValue;
            //idCliente, idStatus, Nome, DataNascto, email, facebook, instagram, telCelular1, telCelular2, telFixo, observacao
            DataCadastro = Convert.ToDateTime(leitor["dataCadastro"]);
            LoginCadastro = leitor["loginCadastro"].ToString();

            Email = leitor["email"].ToString();
            Facebook = leitor["facebook"].ToString();
            Instagram = leitor["instagram"].ToString();
            TelCelular1 = leitor["telCelular1"].ToString();
            TelCelular2 = leitor["telCelular2"].ToString();
            TelFixo = leitor["telFixo"].ToString();
            Observacao = leitor["observacao"].ToString();
            Senha = leitor["senha"].ToString();
        }

        public Cliente(string nome)
        {
            Nome = nome;
        }

        public Cliente(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }


        public static List<Cliente> Lista(bool comSelecione, string nome)
        {
            var lista = new List<Cliente>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_cliente ");

                strQuery.Append("WHERE NOME LIKE '%" + nome + "%' ");

                strQuery.Append("ORDER BY NOME ");

                if (comSelecione)
                    lista.Add(new Cliente(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Cliente(reader));
            }
            return lista;
        }


        public static Cliente Pesquisar(int id)
        {
            Cliente usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_cliente ");
                strQuery.Append("WHERE idCliente = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Cliente(reader);
            }
            return usuario;
        }


        public static Cliente Pesquisar(string nome)
        {
            Cliente usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_cliente ");
                strQuery.Append("WHERE nome = @nome");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@nome", nome);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Cliente(reader);
            }
            return usuario;
        }


        public static Cliente PesquisarTelCel(string telCel)
        {
            Cliente usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_cliente ");
                strQuery.Append("WHERE telCelular1 = @telcel");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@telcel", telCel);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Cliente(reader);
            }
            return usuario;
        }

        public static Cliente PesquisarPeloEmail(string email)
        {
            Cliente usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_cliente ");
                strQuery.Append("WHERE email = @email");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@email", email);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new Cliente(reader);
            }
            return usuario;
        }


        public static bool Inserir(Cliente cliente)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_cliente ");
                strQuery.Append("(idStatus, Nome, DataNascto, email, facebook, instagram, telCelular1, telCelular2, telFixo, observacao, loginCadastro, dataCadastro ) ");
                strQuery.Append("VALUES ( 1, @Nome, @DataNascto, @email, @facebook, @instagram, @telCelular1, @telCelular2, @telFixo, @observacao, @loginCadastro, @dataCadastro ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@DataNascto", cliente.DataNascto.Date);
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@facebook", cliente.Facebook);
                    comando.Parameters.AddWithValue("@instagram", cliente.Instagram);
                    comando.Parameters.AddWithValue("@telCelular1", cliente.TelCelular1);
                    comando.Parameters.AddWithValue("@telCelular2", cliente.TelCelular2);
                    comando.Parameters.AddWithValue("@telFixo", cliente.TelFixo);
                    comando.Parameters.AddWithValue("@observacao", cliente.Observacao);
                    comando.Parameters.AddWithValue("@loginCadastro", cliente.LoginCadastro);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int InserirRetornandoId(Cliente cliente)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_cliente ");
                strQuery.Append("(idStatus, Nome, DataNascto, email, facebook, instagram, telCelular1, telCelular2, telFixo, observacao, loginCadastro, dataCadastro, senha ) ");
                strQuery.Append("VALUES ( 1, @Nome, @DataNascto, @email, @facebook, @instagram, @telCelular1, @telCelular2, @telFixo, @observacao, @loginCadastro, @dataCadastro, @senha ); ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@DataNascto", cliente.DataNascto.Date);
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@facebook", cliente.Facebook);
                    comando.Parameters.AddWithValue("@instagram", cliente.Instagram);
                    comando.Parameters.AddWithValue("@telCelular1", cliente.TelCelular1);
                    comando.Parameters.AddWithValue("@telCelular2", cliente.TelCelular2);
                    comando.Parameters.AddWithValue("@telFixo", cliente.TelFixo);
                    comando.Parameters.AddWithValue("@observacao", cliente.Observacao);
                    comando.Parameters.AddWithValue("@loginCadastro", cliente.LoginCadastro);
                    comando.Parameters.AddWithValue("@senha", cliente.Senha);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);

                    resultado = Convert.ToInt32( comando.ExecuteScalar() );

                    //var insert = comando.ExecuteNonQuery();
                    //resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int InserirNomeRetornndoId(Cliente cliente)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_cliente ");
                strQuery.Append("(idStatus, Nome, loginCadastro, dataCadastro ) ");
                strQuery.Append("VALUES ( 1, @Nome, @loginCadastro, @dataCadastro ); ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@loginCadastro", cliente.LoginCadastro);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);

                    resultado = Convert.ToInt32( comando.ExecuteScalar() );

                    //var insert = comando.ExecuteNonQuery();
                    //if(insert == 1)
                    //  resultado = comando.LastInsertedId;

                }
            }
            return resultado;
        }


        public static bool Editar(Cliente cliente)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_cliente SET ");
                strQuery.Append("  idCliente = @idCliente, Nome = @Nome ");

                if (cliente.DataNascto != DateTime.MinValue)
                    strQuery.Append(", DataNascto = @DataNascto ");

                strQuery.Append(", email = @email,facebook = @facebook, instagram = @instagram ");
                strQuery.Append(", telCelular1 = @telCelular1,telCelular2 = @telCelular2,telFixo = @telFixo ");
                strQuery.Append(", observacao = @observacao ");
                strQuery.Append("WHERE idCliente = @idCliente ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);


                    if (cliente.DataNascto != DateTime.MinValue)
                        comando.Parameters.AddWithValue("@DataNascto", cliente.DataNascto);

                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@facebook", cliente.Facebook);
                    comando.Parameters.AddWithValue("@instagram", cliente.Instagram);
                    comando.Parameters.AddWithValue("@telCelular1", cliente.TelCelular1);
                    comando.Parameters.AddWithValue("@telCelular2", cliente.TelCelular2);
                    comando.Parameters.AddWithValue("@telFixo", cliente.TelFixo);
                    comando.Parameters.AddWithValue("@observacao", cliente.Observacao);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarStatus(int idCliente, int idStatus)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_cliente SET ");
                strQuery.Append("idStatus = @idStatus ");
                strQuery.Append("WHERE idCliente = @idCliente ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idCliente", idCliente);
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarSenha(int idCliente, string senha)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_cliente SET ");
                strQuery.Append("senha = @senha ");
                strQuery.Append("WHERE idCliente = @idCliente ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idCliente", idCliente);
                    comando.Parameters.AddWithValue("@senha", senha);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static string PesquisarNumTelCel(int id)
        {
            var tel = "";
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_cliente ");
                strQuery.Append("WHERE idCliente = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    tel = reader["telCelular1"].ToString();
            }
            return tel;
        }


        public static bool ResetarSenha(int idCliente)
        {
            bool resultado;
            var numTel = PesquisarNumTelCel(idCliente);
            var senhaInicial = Criptografia.Encrypt(numTel);

            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_cliente SET ");
                strQuery.Append("senha = @senha ");
                strQuery.Append("WHERE idCliente = @idCliente ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idCliente", idCliente);
                    comando.Parameters.AddWithValue("@senha", senhaInicial);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


    }
}
