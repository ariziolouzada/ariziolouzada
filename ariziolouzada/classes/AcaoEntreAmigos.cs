using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace ariziolouzada.classes
{
    public class AcaoEntreAmigos
    {

        //Propriedades
        private static readonly string StringConnection;

        public int Numero { get; set; }
        public int IdStatus { get; set; }
        public int IdVendedor { get; set; }
        public string NomeComprador { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public DateTime DataVenda { get; set; }

        public string Status { get; set; }
        public string NumeroStr { get; set; }
        public string NomeVendedor { get; set; }

        static AcaoEntreAmigos()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        public AcaoEntreAmigos() { }

        //public AcaoEntreAmigos(int id, string desc)
        //{
        //    Id = id;
        //    Descricao = desc;
        //}

        private AcaoEntreAmigos(MySqlDataReader leitor)
        {
            Numero = Convert.ToInt32(leitor["numero"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            IdVendedor = leitor["idVendedor"] != DBNull.Value ? Convert.ToInt32(leitor["idVendedor"]) : 0;
            DataVenda = Convert.ToDateTime(leitor["dataVenda"]);
            NomeComprador = leitor["nomeComprador"].ToString();
            Telefone = leitor["telefoneComprador"].ToString();
            Email = leitor["emailComprador"].ToString();
            NumeroStr = string.Format("{0:00000}", Numero);
            NomeVendedor = leitor["nomeVendedor"].ToString();
            //Status = leitor["descricao_status"].ToString();
        }

        //INSERT INTO bdepcvrd.tbl_acao_entre_amigos 
        //(numero, idStatus, NomeComprador, TelefoneComprador, EmailComprador, idVendedor) 
        //VALUES (<VARCHAR(5)>, <INT(11)>, <VARCHAR(50)>, <VARCHAR(50)>, <VARCHAR(100)>, <INT(11)>);

        public static bool Inserir(string numero)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_acao_entre_amigos ");
                strQuery.Append("(numero, idStatus) ");
                strQuery.Append("VALUES (@numero, 0) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))

                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@numero", numero);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(AcaoEntreAmigos aea)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdepcvrd.tbl_acao_entre_amigos SET ");
                strQuery.Append("nomeComprador = @nomeComprador, telefoneComprador = @telefoneComprador, emailComprador = @emailComprador ");
                strQuery.Append(", idVendedor = @idVendedor, idStatus = @idStatus, dataVenda = @dataVenda ");
                strQuery.Append("WHERE numero = @numero ");
                //numero, idStatus, NomeComprador, TelefoneComprador, EmailComprador, idVendedor
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@nomeComprador", aea.NomeComprador.ToUpper());
                    comando.Parameters.AddWithValue("@telefoneComprador", aea.Telefone);
                    comando.Parameters.AddWithValue("@emailComprador", aea.Email);
                    comando.Parameters.AddWithValue("@idVendedor", aea.IdVendedor);
                    comando.Parameters.AddWithValue("@idStatus", aea.IdStatus);
                    comando.Parameters.AddWithValue("@numero", aea.Numero);
                    comando.Parameters.AddWithValue("@dataVenda", aea.DataVenda);
                    var update = comando.ExecuteNonQuery();
                    resultado = update == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        //public static bool Excluir(int id)
        //{
        //    bool resultado;
        //    using (var conexao = new MySqlConnection(StringConnection))
        //    {
        //        var strQuery = new StringBuilder();
        //        strQuery.Append("DELETE FROM bdepcvrd.tbl_ast_Servico ");
        //        strQuery.Append("WHERE id = @id ");

        //        using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
        //        {
        //            conexao.Open();//Abrir a conexão com o BD
        //            comando.Parameters.AddWithValue("@id", id);
        //            var delete = comando.ExecuteNonQuery();
        //            resultado = delete > 0;
        //        }
        //    }
        //    return resultado;
        //}


        public static List<AcaoEntreAmigos> Lista(int idStatus)
        {
            var lista = new List<AcaoEntreAmigos>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM vw_acao_entre_amigos ");

                if (idStatus > -1)
                    strQuery.Append("WHERE idStatus = @idStatus ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > -1)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AcaoEntreAmigos(reader));
            }
            return lista;
        }

        public static List<AcaoEntreAmigos> CarregaTabelaPaginaDefault(int idStatus)
        {
            var lista = new List<AcaoEntreAmigos>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM vw_acao_entre_amigos ");

                if (idStatus > -1)
                    strQuery.Append("WHERE idStatus = @idStatus ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > -1)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AcaoEntreAmigos(reader));
            }
            return lista;
        }

        public static int QtdeNumeros(int idStatus)
        {
            var qtde = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT  count(numero) AS qtde ");
                strQuery.Append("FROM tbl_acao_entre_amigos ");
                strQuery.Append("WHERE idStatus = @idStatus ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    qtde = Convert.ToInt32(reader["qtde"]);
            }
            return qtde;
        }


        public static List<AcaoEntreAmigos> ListaFilter(int numInicio, int numFim)
        {
            var lista = new List<AcaoEntreAmigos>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM vw_acao_entre_amigos ");

                strQuery.Append(string.Format("WHERE numero >= {0} AND numero <= {1} ", numInicio, numFim));

                //if (idStatus > -1)
                //{
                //    strQuery.Append("WHERE idStatus = @idStatus ");
                //}

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                //if (idStatus > -1)
                //    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AcaoEntreAmigos(reader));
            }
            return lista;
        }

        public static List<AcaoEntreAmigos> ListaNumerosValidos(string valuePesq)
        {
            var lista = new List<AcaoEntreAmigos>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM vw_acao_entre_amigos ");
                strQuery.Append("WHERE idStatus = 0 ");

                if (valuePesq != string.Empty)
                    strQuery.Append(string.Format("AND NomeVendedor like '%{0}%' ", valuePesq));


                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AcaoEntreAmigos(reader));
            }
            return lista;
        }

        public static AcaoEntreAmigos PesquisaNumero(int numero)
        {
            AcaoEntreAmigos aea = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM vw_acao_entre_amigos ");
                strQuery.Append("WHERE numero = @numero ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@numero", numero);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    aea = new AcaoEntreAmigos(reader);
            }
            return aea;
        }


        public static bool AutenticarUser(int idTipo, string senha)
        {
            var resultado = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM tbl_acao_entre_amigos_users ");
                strQuery.Append("WHERE idTipo = @idTipo ");
                strQuery.Append("AND senha = @senha ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idTipo", idTipo);
                comando.Parameters.AddWithValue("@senha", senha);

                var reader = comando.ExecuteReader();
                resultado = reader.Read();
            }
            return resultado;
        }


        public static DataTable CarregaGridView(int idStatus)
        {
            var dt = new DataTable();
            var strQuery = new StringBuilder();
            strQuery.Append("SELECT * ");
            strQuery.Append("FROM vw_acao_entre_amigos ");

            if (idStatus > -1)
                strQuery.Append("WHERE idStatus = @idStatus ");

            using (var conexao = new MySqlConnection(StringConnection))
            {
                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > -1)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                dt.Load(reader);

                //var adapter = new MySqlDataAdapter(comando);
                //adapter.Fill(dt);
            }

            return dt;

        }

    }
}