using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace ariziolouzada.classes
{
    public class AcaoEntreAmigosTemp
    {
        //Propriedades
        private static readonly string StringConnection;

        public int Numero { get; set; }
        public long IdVenda { get; set; }

        public string NumeroStr { get; set; }

        static AcaoEntreAmigosTemp()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        //public AcaoEntreAmigosTemp() { }

        public AcaoEntreAmigosTemp(int num, long idVenda)
        {
            Numero = num;
            IdVenda = idVenda;
        }

        private AcaoEntreAmigosTemp(MySqlDataReader leitor)
        {
            Numero = Convert.ToInt32(leitor["numero"]);
            IdVenda = Convert.ToInt64(leitor["idVenda"]); 
            NumeroStr = string.Format("{0:00000}", Numero);
        }


        public static bool Inserir(AcaoEntreAmigosTemp aeaTemp)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_acao_entre_amigos_temp ");
                strQuery.Append("(numero, idVenda) ");
                strQuery.Append("VALUES (@numero, @idVenda) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))

                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@numero", aeaTemp.Numero);
                    comando.Parameters.AddWithValue("@idVenda", aeaTemp.IdVenda);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Excluir(AcaoEntreAmigosTemp aeaTemp)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdepcvrd.tbl_acao_entre_amigos_temp ");
                strQuery.Append("WHERE numero = @numero  AND idVenda = @idVenda ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@numero", aeaTemp.Numero);
                    comando.Parameters.AddWithValue("@idVenda", aeaTemp.IdVenda);
                    var delete = comando.ExecuteNonQuery();
                    resultado = delete > 0;
                }
            }
            return resultado;
        }

        public static bool ExcluirVenda(long idVenda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdepcvrd.tbl_acao_entre_amigos_temp ");
                strQuery.Append("WHERE idVenda = @idVenda ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idVenda", idVenda);
                    var delete = comando.ExecuteNonQuery();
                    resultado = delete > 0;
                }
            }
            return resultado;
        }

        public static void ExcluirRegistrosNaoUtilizados()
        {            
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdepcvrd.tbl_acao_entre_amigos_temp ");
                strQuery.Append("WHERE numero IN ( ");
                strQuery.Append("   SELECT numero FROM bdepcvrd.tbl_acao_entre_amigos ");
                strQuery.Append("   WHERE idStatus = 1 AND dataVenda < current_date() ");
                strQuery.Append("); ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    var delete = comando.ExecuteNonQuery();
                    //resultado = delete > 0;
                }
            }
        }


        public static List<AcaoEntreAmigosTemp> Lista(long idVenda)
        {
            var lista = new List<AcaoEntreAmigosTemp>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM tbl_acao_entre_amigos_temp ");
                strQuery.Append("WHERE idVenda = @idVenda ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idVenda", idVenda);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AcaoEntreAmigosTemp(reader));
            }
            return lista;
        }

        public static AcaoEntreAmigosTemp Pesquisar(int numero, long idVenda)
        {
            AcaoEntreAmigosTemp  aeat = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM tbl_acao_entre_amigos_temp ");
                strQuery.Append("WHERE idVenda = @idVenda ");
                strQuery.Append("AND numero = @numero ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idVenda", idVenda);
                comando.Parameters.AddWithValue("@numero", numero);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    aeat = new AcaoEntreAmigosTemp(reader);
            }
            return aeat;
        }

    }
}