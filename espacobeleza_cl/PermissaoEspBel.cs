using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace espacobeleza_cl
{
    public class PermissaoEspBel
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdPai { get; set; }
        public string Descricao { get; set; }
        public string Arvore { get; set; }


        static PermissaoEspBel()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        // Metodos 
        public PermissaoEspBel() { }

        private PermissaoEspBel(MySqlDataReader leitor)
        {
            //id_permissao_sistema, desc_permissao_sistema, id_permissao_pai, arvore_permissao
            Id = Convert.ToInt32(leitor["id_permissao_sistema"]);
            IdPai = Convert.ToInt32(leitor["id_permissao_pai"]);
            Descricao = leitor["desc_permissao_sistema"].ToString();
            Arvore = leitor["arvore_permissao"].ToString();
        }


        public static List<PermissaoEspBel> Lista(string arvore)
        {
            var lista = new List<PermissaoEspBel>();
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
                    lista.Add(new PermissaoEspBel(reader));
            }
            return lista;
        }


    }
}
