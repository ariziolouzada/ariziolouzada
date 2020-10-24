using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ariziolouzada.classes
{
    public class MaterialTipo
    {

        //Propriedades
        private static readonly string StringConnection;
        
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Descricao { get; set; }

        static MaterialTipo()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        public MaterialTipo() { }

        private MaterialTipo(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            //IdStatus = Convert.ToInt32(leitor["id_status"]);
            Descricao = leitor["descricao"].ToString();
        }

        public MaterialTipo(int id, string desc)
        {
            Id = id;
            Descricao = desc;
        }

        public static List<MaterialTipo> ListaTipoMaterial(bool comSelecione)
        {
            var lista = new List<MaterialTipo>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdepcvrd.tbl_ast_material_tipo  ");

                if (comSelecione)
                    lista.Add(new MaterialTipo(0, "Selecione..."));

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);               

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new MaterialTipo(reader));
            }
            return lista;
        }


    }
}
