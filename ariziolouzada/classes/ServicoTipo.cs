using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ariziolouzada.classes
{
    public class ServicoTipo
    {


        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Descricao { get; set; }

        static ServicoTipo()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        public ServicoTipo() { }

        private ServicoTipo(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id_tipo"]);
            //IdStatus = Convert.ToInt32(leitor["id_status"]);
            Descricao = leitor["descricao_tipo"].ToString();
        }

        public ServicoTipo(int id, string desc)
        {
            Id = id;
            Descricao = desc;
        }

        public static List<ServicoTipo> ListaTipoServico(bool comSelecione)
        {
            var lista = new List<ServicoTipo>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdepcvrd.tbl_ast_servico_tipo  ");

                if (comSelecione)
                    lista.Add(new ServicoTipo(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ServicoTipo(reader));
            }
            return lista;
        }


    }
}
