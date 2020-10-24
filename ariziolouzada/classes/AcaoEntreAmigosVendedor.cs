using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace ariziolouzada.classes
{
    public class AcaoEntreAmigosVendedor
    {
        //Propriedades
        private static readonly string StringConnection;

        public int IdVendedor { get; set; }
        public int IdStatus { get; set; }
        public string NomeVendedor { get; set; }


        static AcaoEntreAmigosVendedor()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        public AcaoEntreAmigosVendedor(int id, string nome)
        {
            IdVendedor = id;
            NomeVendedor = nome;
        }

        public AcaoEntreAmigosVendedor(int id, string nome, int idStatus)
        {
            IdVendedor = id;
            NomeVendedor = nome;
            IdStatus = idStatus;
        }

        private AcaoEntreAmigosVendedor(MySqlDataReader leitor)
        {
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            IdVendedor = Convert.ToInt32(leitor["idVendedor"]);
            NomeVendedor = leitor["NomeVendedor"].ToString();
        }

        //        SELECT idVendedor, NomeVendedor, idStatus
        //FROM bdepcvrd.tbl_acao_entre_amigos_vendedor;

        public static bool Inserir(string nome)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_acao_entre_amigos_vendedor ");
                strQuery.Append("(NomeVendedor, idStatus) ");
                strQuery.Append("VALUES (@nome, 1) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))

                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@nome", nome.ToUpper());
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Editar(AcaoEntreAmigosVendedor serv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdepcvrd.tbl_acao_entre_amigos_vendedor SET ");
                strQuery.Append("NomeVendedor = @NomeVendedor, idStatus = @idStatus ");
                strQuery.Append("WHERE idVendedor = @idVendedor ");
                //numero, idStatus, NomeComprador, TelefoneComprador, EmailComprador, idVendedor
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@NomeVendedor", serv.NomeVendedor.ToUpper());
                    comando.Parameters.AddWithValue("@idVendedor", serv.IdVendedor);
                    comando.Parameters.AddWithValue("@idStatus", serv.IdStatus);
                    var update = comando.ExecuteNonQuery();
                    resultado = update == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static List<AcaoEntreAmigosVendedor> Lista(int idStatus)
        {
            var lista = new List<AcaoEntreAmigosVendedor>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM tbl_acao_entre_amigos_vendedor ");
                strQuery.Append("WHERE idVendedor > 1 ");

                if (idStatus > -1)
                    strQuery.Append("AND idStatus = @idStatus ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > -1)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AcaoEntreAmigosVendedor(reader));
            }
            return lista;
        }

        public static List<AcaoEntreAmigosVendedor> CarregaDdl(bool comSelecine)
        {
            var lista = new List<AcaoEntreAmigosVendedor>();

            if (comSelecine)
                lista.Add(new AcaoEntreAmigosVendedor(0, "Selecione"));

            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM tbl_acao_entre_amigos_vendedor ");
                strQuery.Append("WHERE idStatus = 1 ");
                strQuery.Append("AND idVendedor > 1 ");

                //if(valuePesq != string.Empty)
                //    strQuery.Append(string.Format("AND NomeVendedor like '%{0}%' ", valuePesq));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AcaoEntreAmigosVendedor(reader));
            }
            return lista;
        }

        public static AcaoEntreAmigosVendedor Pesquisar(int id)
        {
            AcaoEntreAmigosVendedor vendedor = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM tbl_acao_entre_amigos_vendedor ");
                strQuery.Append("WHERE idVendedor = @id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    vendedor = new AcaoEntreAmigosVendedor(reader);
            }
            return vendedor;
        }

    }
}