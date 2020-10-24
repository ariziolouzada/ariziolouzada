using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ferrita_Cl
{
    public class ComandaFormaPgto
    {

        private static readonly string stringConnection;
        public int Id { get; set; }
        public string Descricao { get; set; }


        static ComandaFormaPgto()
        {
            stringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        public ComandaFormaPgto(int id, string desc)
        {
            Id = id;
            Descricao = desc;
        }

        public ComandaFormaPgto(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id_forma_pgto"]);
            Descricao = leitor["desc_forma_pgto"].ToString();
        }


        public static List<ComandaFormaPgto> ListaDdl(bool comSelecione)
        {
            var resultado = new List<ComandaFormaPgto>();

            using (var conexao = new MySqlConnection(stringConnection))
            {
                StringBuilder sql = new StringBuilder();

                sql.Append("SELECT * ");
                sql.Append("FROM tbl_ferrita_cmda_forma_pgto ");

                if (comSelecione)
                    resultado.Add(new ComandaFormaPgto(0, "Selecione..."));

                using (var comando = new MySqlCommand(sql.ToString(), conexao))
                {
                    using (conexao)
                    {
                        conexao.Open();//Abrir a conexão com o BD
                        //comando.Parameters.AddWithValue(@"DESC_TIPO_SERV_PROD", pDescServProd);
                        var drEvento = comando.ExecuteReader();
                        while (drEvento.Read())
                        {
                            resultado.Add(new ComandaFormaPgto(drEvento));
                        }
                    }
                }
            }
            return resultado;
        }


    }
}
