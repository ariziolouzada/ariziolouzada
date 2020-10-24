using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espacobeleza_cl
{
   public class ProfissaoEspBel
    {

        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public string Descricao { get; set; }

        static ProfissaoEspBel()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }


        public ProfissaoEspBel(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id_profissao"]);
            Descricao = leitor["descricao_profissao"].ToString();
        }

        public ProfissaoEspBel(int id, string desc)
        {
            Id = id;
            Descricao = desc;
        }

        public static List<ProfissaoEspBel> Lista(bool comSelecione)
        {
            var lista = new List<ProfissaoEspBel>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_profissional_profissao ");
                //strQuery.Append("WHERE idStatus = 1 ");

                //strQuery.Append("ORDER BY nome ");

                if (comSelecione)
                    lista.Add(new ProfissaoEspBel(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ProfissaoEspBel(reader));
            }
            return lista;
        }


    }
}
