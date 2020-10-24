using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbessencial_cl
{
    public class ServicoProfissional
    {

        //Propriedades
        private static readonly string StringConnection;
        public int IdServico { get; set; }
        public int IdProfissional { get; set; }

        //public int IdStatus { get; set; }
        //public string Descricao { get; set; }
        //public decimal Valor { get; set; }

        //Auxiliar
        //public string NomeProfissional { get; set; }


        static ServicoProfissional()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        public ServicoProfissional(int idSvc, int idProf)
        {
            IdServico = idSvc;
            IdProfissional = idProf;
        }


        public static bool Inserir(int idSvc, int idProf)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_profissional_servico ");
                strQuery.Append("(idProfissional, idServico ) ");
                strQuery.Append("VALUES ( @idProfissional, @idServico ) ");

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

        public static bool Deletar(int idSvc, int idProf)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_profissional_servico ");
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
                strQuery.Append("FROM sbessencial.tbl_profissional_servico ");
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

    }
}
