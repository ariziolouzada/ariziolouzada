using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbessencial_cl
{
    public class PermissaoSbe
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdPai { get; set; }
        public string Descricao { get; set; }
        public string Arvore { get; set; }


        static PermissaoSbe()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        // Metodos 
        public PermissaoSbe() { }

        private PermissaoSbe(MySqlDataReader leitor)
        {
            //id_permissao_sistema, desc_permissao_sistema, id_permissao_pai, arvore_permissao
            Id = Convert.ToInt32(leitor["id_permissao_sistema"]);
            IdPai = Convert.ToInt32(leitor["id_permissao_pai"]);
            Descricao = leitor["desc_permissao_sistema"].ToString();
            Arvore = leitor["arvore_permissao"].ToString();
        }


        public static List<PermissaoSbe> Lista(string arvore)
        {
            var lista = new List<PermissaoSbe>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_permissao_sistema ");
                strQuery.Append("WHERE arvore_permissao LIKE '" + arvore + "%' ");
                strQuery.Append("ORDER BY arvore_permissao ");
                
                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new PermissaoSbe(reader));
            }
            return lista;
        }


    }
}
