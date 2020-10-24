using CriptografiaSgpm;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espacobeleza_cl
{
   public class ClienteEspBel
    {

        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public int IdEmpresaContratante { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
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


        static ClienteEspBel()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        // Metodos 
        public ClienteEspBel() { }


        private ClienteEspBel(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idCliente"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            IdEmpresaContratante = Convert.ToInt32(leitor["idEmpresaContratante"]);
            Nome = leitor["Nome"].ToString();
            Cpf = leitor["cpf"].ToString();
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

        public ClienteEspBel(string nome)
        {
            Nome = nome;
        }

        public ClienteEspBel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public ClienteEspBel(int id, string nome, string cpf, int idEmpContrat)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            IdEmpresaContratante = idEmpContrat;
        }

        public static List<ClienteEspBel> Lista(bool comSelecione, string nome, int idEmpContrat)
        {
            var lista = new List<ClienteEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_cliente ");
                strQuery.Append("WHERE idEmpresaContratante = @idEmpContrat ");

                if(nome != string.Empty)
                    strQuery.Append("AND NOME LIKE '%" + nome + "%' ");

                strQuery.Append("ORDER BY NOME ");

                if (comSelecione)
                    lista.Add(new ClienteEspBel(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ClienteEspBel(reader));
            }
            return lista;
        }


        public static ClienteEspBel Pesquisar(int id)
        {
            ClienteEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_cliente ");
                strQuery.Append("WHERE idCliente = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ClienteEspBel(reader);
            }
            return usuario;
        }


        public static ClienteEspBel Pesquisar(string nome, int idEmpContrat)
        {
            ClienteEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_cliente ");
                strQuery.Append("WHERE nome = @nome ");
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ClienteEspBel(reader);
            }
            return usuario;
        }


        public static ClienteEspBel PesquisarTelCel(string telCel, int idEmpContrat)
        {
            ClienteEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_cliente ");
                strQuery.Append("WHERE telCelular1 = @telcel");
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@telcel", telCel);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ClienteEspBel(reader);
            }
            return usuario;
        }

        public static ClienteEspBel PesquisarPeloEmail(string email, int idEmpContrat)
        {
            ClienteEspBel usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_cliente ");
                strQuery.Append("WHERE email = @email");
                strQuery.Append("AND idEmpresaContratante = @idEmpContrat ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@email", email);
                comando.Parameters.AddWithValue("@idEmpContrat", idEmpContrat);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new ClienteEspBel(reader);
            }
            return usuario;
        }


        public static bool Inserir(ClienteEspBel cliente)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_cliente ");
                strQuery.Append("(idStatus, idEmpresaContratante, Nome, DataNascto, email, facebook, instagram, telCelular1, telCelular2, telFixo, observacao, loginCadastro, dataCadastro, cpf ) ");
                strQuery.Append("VALUES ( 1, @idEmpresaContratante, @Nome, @DataNascto, @email, @facebook, @instagram, @telCelular1, @telCelular2, @telFixo, @observacao, @loginCadastro, @dataCadastro, @cpf ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@idEmpresaContratante", cliente.IdEmpresaContratante);
                    comando.Parameters.AddWithValue("@DataNascto", cliente.DataNascto.Date);
                    comando.Parameters.AddWithValue("@email", cliente.Email);
                    comando.Parameters.AddWithValue("@cpf", cliente.Cpf);
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


        public static int InserirRetornandoId(ClienteEspBel cliente)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_cliente ");
                strQuery.Append("(idStatus, Nome, idEmpresaContratante, DataNascto, email, facebook, instagram, telCelular1, telCelular2, telFixo, observacao, loginCadastro, dataCadastro, senha ) ");
                strQuery.Append("VALUES ( 1, @Nome, @idEmpresaContratante, @DataNascto, @email, @facebook, @instagram, @telCelular1, @telCelular2, @telFixo, @observacao, @loginCadastro, @dataCadastro, @senha ); ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@idEmpresaContratante", cliente.IdEmpresaContratante);
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

                    resultado = Convert.ToInt32(comando.ExecuteScalar());

                    //var insert = comando.ExecuteNonQuery();
                    //resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static int InserirNomeRetornndoId(ClienteEspBel cliente)
        {
            int resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_cliente ");
                strQuery.Append("(idStatus, Nome, loginCadastro, dataCadastro, idEmpresaContratante ) ");
                strQuery.Append("VALUES ( 1, @Nome, @loginCadastro, @dataCadastro, @idEmpresaContratante ); ");
                strQuery.Append("SELECT LAST_INSERT_ID(); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@idEmpresaContratante", cliente.IdEmpresaContratante);
                    comando.Parameters.AddWithValue("@loginCadastro", cliente.LoginCadastro);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);

                    resultado = Convert.ToInt32(comando.ExecuteScalar());

                    //var insert = comando.ExecuteNonQuery();
                    //if(insert == 1)
                    //  resultado = comando.LastInsertedId;

                }
            }
            return resultado;
        }


        public static bool Editar(ClienteEspBel cliente)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_cliente SET ");
                strQuery.Append("  idCliente = @idCliente, Nome = @Nome ");

                if (cliente.DataNascto != DateTime.MinValue)
                    strQuery.Append(", DataNascto = @DataNascto ");

                strQuery.Append(", email = @email,facebook = @facebook, instagram = @instagram ");
                strQuery.Append(", telCelular1 = @telCelular1,telCelular2 = @telCelular2,telFixo = @telFixo ");
                strQuery.Append(", observacao = @observacao, cpf = @cpf ");
                strQuery.Append("WHERE idCliente = @idCliente ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                    comando.Parameters.AddWithValue("@cpf", cliente.Cpf);


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
                strQuery.Append("UPDATE bdespacobeleza.tbl_cliente SET ");
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
                strQuery.Append("UPDATE bdespacobeleza.tbl_cliente SET ");
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
                strQuery.Append("FROM bdespacobeleza.tbl_cliente ");
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
                strQuery.Append("UPDATE bdespacobeleza.tbl_cliente SET ");
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
