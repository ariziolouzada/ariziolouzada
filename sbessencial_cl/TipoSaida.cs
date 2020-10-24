using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbessencial_cl
{
    public class TipoSaida
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public string Descricao { get; set; }

        static TipoSaida()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        public TipoSaida(string desc)
        {
            Descricao = desc;
        }

        public TipoSaida(int id, string desc)
        {
            Id = id;
            Descricao = desc;
        }

        private TipoSaida(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idSaida"]);
            Descricao = leitor["descSaida"].ToString();
        }


        public static List<TipoSaida> Lista(bool comSelecione, string nome)
        {
            var lista = new List<TipoSaida>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_saida ");
                strQuery.Append("WHERE idSaida > 1 ");
                strQuery.Append("AND descSaida LIKE '%" + nome + "%' ");
                strQuery.Append("ORDER BY descSaida ");

                if (comSelecione)
                    lista.Add(new TipoSaida(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new TipoSaida(reader));
            }
            return lista;
        }


        public static TipoSaida Pesquisar(int id)
        {
            TipoSaida usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_saida ");
                strQuery.Append("WHERE idSaida = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new TipoSaida(reader);
            }
            return usuario;
        }

        public static TipoSaida Pesquisar(string desc)
        {
            TipoSaida ts = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_saida ");
                strQuery.Append("WHERE descSaida = @desc");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@desc", desc);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    ts = new TipoSaida(reader);
            }
            return ts;
        }


        public static bool Inserir(TipoSaida ts)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_financeiro_saida ");
                strQuery.Append("(descSaida ) ");
                strQuery.Append("VALUES (@descSaida ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idCliente", cliente.Id);
                    comando.Parameters.AddWithValue("@descSaida", ts.Descricao);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(TipoSaida tipoSaida)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_financeiro_saida SET ");
                strQuery.Append(" descSaida = @descSaida ");
                strQuery.Append("WHERE idSaida = @idSaida ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idSaida", tipoSaida.Id);
                    comando.Parameters.AddWithValue("@descSaida", tipoSaida.Descricao);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


    }
}
