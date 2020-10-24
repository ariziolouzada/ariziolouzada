using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ariziolouzada.classes
{
    public class SolicitacaoEpi
    {
        
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdEpi { get; set; }
        public DateTime Data { get; set; }
        public string Hash { get; set; }
        public string Deferido { get; set; }
        public DateTime DataDeferido { get; set; }

        static SolicitacaoEpi()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        public SolicitacaoEpi() { }

        private SolicitacaoEpi(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            IdUsuario = Convert.ToInt32(leitor["id_usuario"]);
            IdEpi = Convert.ToInt32(leitor["id_epi"]);
            Data = Convert.ToDateTime(leitor["data"]);
            Hash = leitor["hash"].ToString();
            Deferido = leitor["deferido"].ToString();
            DataDeferido = leitor["data_deferido"] != DBNull.Value ? Convert.ToDateTime(leitor["data_deferido"]) : DateTime.MinValue;
        }
        

        public static bool Inserir(SolicitacaoEpi solEpi)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_ast_solicitacao_epi ");
                strQuery.Append("(id_usuario, id_epi, data, hash) ");
                strQuery.Append("VALUES (@id_usuario, @id_epi, @data, @hash) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_usuario",  solEpi.IdUsuario    );
                    comando.Parameters.AddWithValue("@id_epi",      solEpi.IdEpi        );
                    comando.Parameters.AddWithValue("@hash",        solEpi.Hash         );
                    comando.Parameters.AddWithValue("@data",        DateTime.Now        );

                    //var senhaCriptografada = Criptografia.Encrypt(solEpi.Senha);
                    //comando.Parameters.AddWithValue("@senha", senhaCriptografada);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarResultado(string hash, string result)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdepcvrd.tbl_ast_solicitacao_epi SET ");
                strQuery.Append("deferido = @result, data_deferido = @data ");
                strQuery.Append("WHERE hash = @hash ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@result", result);
                    comando.Parameters.AddWithValue("@hash", hash);
                    comando.Parameters.AddWithValue("@data", DateTime.Now);

                    //var senhaCriptografada = Criptografia.Encrypt(solEpi.Senha);
                    //comando.Parameters.AddWithValue("@senha", senhaCriptografada);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert > 0;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Excluir(string hash)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdepcvrd.tbl_ast_solicitacao_epi ");
                strQuery.Append("WHERE hash = @hash ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@hash", hash);
                    var delete = comando.ExecuteNonQuery();
                    resultado = delete > 0;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        //public static bool Inserir(int idUsuario, int idEpi)
        //{
        //    bool resultado;
        //    using (var conexao = new MySqlConnection(StringConnection))
        //    {
        //        var strQuery = new StringBuilder();
        //        strQuery.Append("INSERT INTO bdepcvrd.tbl_ast_usuario ");
        //        strQuery.Append("(id_usuario, id_epi, data, hash) ");
        //        strQuery.Append("VALUES (@id_usuario, @id_epi, @data, @hash) ");
        //        using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
        //        {
        //            conexao.Open();//Abrir a conexão com o BD
        //            comando.Parameters.AddWithValue("@id_usuario", idUsuario);
        //            comando.Parameters.AddWithValue("@id_epi", idEpi);

        //            comando.Parameters.AddWithValue("@data", DateTime.Now);

        //            //var senhaCriptografada = Criptografia.Encrypt(solEpi.Senha);
        //            //comando.Parameters.AddWithValue("@senha", senhaCriptografada);

        //            comando.Parameters.AddWithValue("@hash", solEpi.Hash);
        //            var insert = comando.ExecuteNonQuery();
        //            resultado = insert == 1;//Convert.ToInt64(insert);
        //        }
        //    }
        //    return resultado;
        //}

    }
}
