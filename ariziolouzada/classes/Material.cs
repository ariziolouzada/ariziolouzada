using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ariziolouzada.classes
{
    public class Material
    {
        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public int IdTipo { get; set; }
        public int IdStatus { get; set; }
        public decimal PrecoCusto { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int QtdeEstoque { get; set; }

        static Material()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        public Material() { }

        private Material(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            IdTipo = Convert.ToInt32(leitor["id_tipo_material"]);
            IdStatus = Convert.ToInt32(leitor["id_status"]);
            PrecoCusto = Convert.ToDecimal(leitor["preco_custo"]);
            Descricao = leitor["descricao"].ToString();
            QtdeEstoque = Convert.ToInt32(leitor["qtde_estoque"]);
            Tipo = leitor["tipo"].ToString();
        }


        public static bool Inserir(Material mater)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_ast_material ");
                strQuery.Append("(descricao, preco_custo, qtde_estoque, id_tipo_material, id_Status) ");
                strQuery.Append("VALUES (@descricao, @preco_custo, @qtde_estoque, @id_tipo_material, @id_Status) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))

                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@preco_custo", mater.PrecoCusto);
                    comando.Parameters.AddWithValue("@descricao", mater.Descricao);
                    comando.Parameters.AddWithValue("@qtde_estoque", mater.QtdeEstoque);
                    comando.Parameters.AddWithValue("@id_tipo_material", mater.IdTipo);
                    comando.Parameters.AddWithValue("@id_Status", mater.IdStatus);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(Material mater)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdepcvrd.tbl_ast_material SET ");
                strQuery.Append("preco_custo = @preco_custo, descricao = @descricao ");
                strQuery.Append(", qtde_estoque = @qtde_estoque, id_tipo_material = @id_tipo_material ");
                strQuery.Append("WHERE id = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id",                  mater.Id            );
                    comando.Parameters.AddWithValue("@preco_custo",         mater.PrecoCusto    );
                    comando.Parameters.AddWithValue("@descricao",           mater.Descricao     );
                    comando.Parameters.AddWithValue("@qtde_estoque",        mater.QtdeEstoque   );
                    comando.Parameters.AddWithValue("@id_tipo_material",    mater.IdTipo        );
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
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
                strQuery.Append("DELETE FROM bdepcvrd.tbl_ast_material ");
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


        public static List<Material> ListaDeMateriais(int idStatus)
        {
            var lista = new List<Material>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM vw_material  ");

                if (idStatus > 0)
                    strQuery.Append("WHERE id_status = @idStatus ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idStatus > 0)
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Material(reader));
            }
            return lista;
        }

        public static Material PesquisaMaterial(int id)
        {
            Material resultado = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM vw_material ");
                strQuery.Append("WHERE id = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    resultado = new Material(reader);
            }
            return resultado;
        }

    }
}
