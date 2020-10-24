using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espacobeleza_cl
{
    public class ServicoProfissionalEspBel
    {


        //Propriedades
        private static readonly string StringConnection;
        public int IdServico { get; set; }
        public int IdProfissional { get; set; }
        public int Comissao { get; set; }


        static ServicoProfissionalEspBel()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        public ServicoProfissionalEspBel(int idSvc, int idProf)
        {
            IdServico = idSvc;
            IdProfissional = idProf;
        }

        public ServicoProfissionalEspBel(int idSvc, int idProf, int comissao)
        {
            IdServico = idSvc;
            IdProfissional = idProf;
            Comissao = comissao;
        }

        private ServicoProfissionalEspBel(MySqlDataReader leitor)
        {
            IdProfissional = Convert.ToInt32(leitor["idProfissional"]);
            IdServico = Convert.ToInt32(leitor["idServico"]);
            Comissao = Convert.ToInt32(leitor["comissao"]);
        }

//
        public static bool Inserir(int idSvc, int idProf)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_profissional_servico ");
                strQuery.Append("(idProfissional, idServico, comissao ) ");
                strQuery.Append("VALUES ( @idProfissional, @idServico, 0 ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idServico", Servico.Id);
                    comando.Parameters.AddWithValue("@idProfissional", idProf);
                    comando.Parameters.AddWithValue("@idServico", idSvc);
                    //comando.Parameters.AddWithValue("@comissao", comissao);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool EditarComissao(int idSvc, int idProf, int comissao)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdespacobeleza.tbl_profissional_servico SET ");
                strQuery.Append("comissao = @comissao ");
                strQuery.Append("WHERE idProfissional = @idProfissional AND idServico = @idServico ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idServico", Servico.Id);
                    comando.Parameters.AddWithValue("@idProfissional", idProf);
                    comando.Parameters.AddWithValue("@idServico", idSvc);
                    comando.Parameters.AddWithValue("@comissao", comissao);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Deletar(int idSvc, int idProf)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_profissional_servico ");
                strQuery.Append("WHERE idProfissional = @idProfissional ");
                strQuery.Append("AND idServico = @idServico ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idServico", Servico.Id);
                    comando.Parameters.AddWithValue("@idProfissional", idProf);
                    comando.Parameters.AddWithValue("@idServico", idSvc);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Existe(int idSvc, int idProf)
        {
            var resultado = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_profissional_servico ");
                strQuery.Append("WHERE idProfissional = @idProfissional ");
                strQuery.Append("AND idServico = @idServico ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idProfissional", idProf);
                comando.Parameters.AddWithValue("@idServico", idSvc);

                var reader = comando.ExecuteReader();

                resultado = reader.Read();
                //while (reader.Read())
                //    usuario = new Servico(reader);
            }
            return resultado;
        }


        public static ServicoProfissionalEspBel Pesquisar(int idSvc, int idProf)
        {
            ServicoProfissionalEspBel resultado = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_profissional_servico ");
                strQuery.Append("WHERE idProfissional = @idProfissional ");
                strQuery.Append("AND idServico = @idServico ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idProfissional", idProf);
                comando.Parameters.AddWithValue("@idServico", idSvc);

                var reader = comando.ExecuteReader();

                while (reader.Read())
                    resultado = new ServicoProfissionalEspBel(reader);
            }
            return resultado;
        }

        public static int PesquisaComissao(int idSvc, int idProf)
        {
            var resultado = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_profissional_servico ");
                strQuery.Append("WHERE idProfissional = @idProfissional ");
                strQuery.Append("AND idServico = @idServico ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idProfissional", idProf);
                comando.Parameters.AddWithValue("@idServico", idSvc);

                var reader = comando.ExecuteReader();

                while (reader.Read())
                    resultado = Convert.ToInt32(reader["comissao"]);
            }
            return resultado;
        }


    }
}
