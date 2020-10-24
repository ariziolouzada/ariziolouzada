using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ariziolouzada.classes
{
    public class Servico
    {

        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public int IdTipo { get; set; }
        public int IdStatus { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

        public string Tipo { get; set; }
        public string Status { get; set; }

        static Servico()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        public Servico() { }

        public Servico(int id, string desc)
        {
            Id = id;
            Descricao = desc;
        }

        private Servico(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            IdTipo = Convert.ToInt32(leitor["id_tipo"]);
            IdStatus = Convert.ToInt32(leitor["id_Status"]);
            Valor = Convert.ToDecimal(leitor["valor"]);
            Descricao = leitor["descricao"].ToString();

            Tipo = leitor["descricao_tipo"].ToString();
            Status = leitor["descricao_status"].ToString();
        }


        public static bool Inserir(Servico serv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_ast_Servico ");
                strQuery.Append("(descricao, valor, id_tipo, id_Status) ");
                strQuery.Append("VALUES (@descricao, @valor, @id_tipo, 1) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))

                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@valor", serv.Valor);
                    comando.Parameters.AddWithValue("@descricao", serv.Descricao);
                    comando.Parameters.AddWithValue("@id_tipo", serv.IdTipo);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(Servico serv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdepcvrd.tbl_ast_Servico SET ");
                strQuery.Append("descricao = @descricao, valor = @valor, id_tipo = @id_tipo ");
                strQuery.Append(", id_status = @id_status  WHERE id = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", serv.Id);
                    comando.Parameters.AddWithValue("@valor", serv.Valor);
                    comando.Parameters.AddWithValue("@descricao", serv.Descricao);
                    comando.Parameters.AddWithValue("@id_tipo", serv.IdTipo);
                    comando.Parameters.AddWithValue("@id_status", serv.IdStatus);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert > 0;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Excluir(int id)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdepcvrd.tbl_ast_Servico ");
                strQuery.Append("WHERE id = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", id);
                    var delete = comando.ExecuteNonQuery();
                    resultado = delete > 0;
                }
            }
            return resultado;
        }


        public static List<Servico> ListaDeServicos(int idStatus)
        {
            var lista = new List<Servico>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM vw_servico ");

                if (idStatus > 0)
                    strQuery.Append("WHERE id_Status = @idStatus ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Servico(reader));
            }
            return lista;
        }


        public static List<Servico> ListaDeServicos(int idTipo, int idStatus, bool comSelecione)
        {
            var lista = new List<Servico>();

            if (comSelecione)
                lista.Add(new Servico(0, "Selecione..."));

            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdepcvrd.vw_servico ");
                strQuery.Append("WHERE id_tipo = @idtipo ");

                if (idStatus > 0)
                    strQuery.Append("AND id_Status = @idStatus ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idtipo", idTipo);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Servico(reader));
            }
            return lista;
        }

        public static Servico PesquisaServico(int id)
        {
            Servico  srv = null;            
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdepcvrd.vw_servico ");
                strQuery.Append("WHERE id = @id ");
                
                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);                
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    srv =new Servico(reader);
            }
            return srv;
        }



    }
}
