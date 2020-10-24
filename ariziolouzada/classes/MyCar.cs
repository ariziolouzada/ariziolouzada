using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace ariziolouzada.classes
{
    public class MyCar
    {
        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Placas { get; set; }
        public string MarcaModel { get; set; }
        public string AnoModeloFabricacao { get; set; }
        public string Chassi { get; set; }
        public string Cor { get; set; }
        public DateTime DataCompra { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenda { get; set; }

        //id_my_car, id_situacao, placas, marca_modelo, ano_mod_fab, chassi, cor, 
        //

        static MyCar()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        public MyCar() { }

        private MyCar(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id_my_car"]);
            IdStatus = Convert.ToInt32(leitor["id_situacao"]);
            Placas = leitor["placas"].ToString();
            MarcaModel = leitor["marca_modelo"].ToString();
            AnoModeloFabricacao = leitor["ano_mod_fab"].ToString();
            Chassi = leitor["chassi"].ToString();
            Cor = leitor["cor"].ToString();
            DataCompra = Convert.ToDateTime(leitor["data_compra"]);
            DataVenda = leitor["data_venda"] != DBNull.Value ? Convert.ToDateTime(leitor["data_venda"]) : DateTime.MinValue;
            ValorCompra = Convert.ToDecimal(leitor["valor_compra"]);
            ValorVenda = leitor["valor_venda"] != DBNull.Value ? Convert.ToDecimal(leitor["valor_venda"]) : 0;
        }


        public static bool Inserir(MyCar carro)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {//id_my_car, id_situacao, placas, marca_modelo, ano_mod_fab, chassi, cor, data_compra, valor_compra, data_venda, valor_venda
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_my_car ");
                strQuery.Append("(placas, marca_modelo, ano_mod_fab, chassi, cor, id_situacao, data_compra, valor_compra) ");
                strQuery.Append("VALUES (@placas, @marca_modelo, @ano_mod_fab, @chassi, @cor, 1, @data_compra, @valor_compra) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@", carro.);
                    comando.Parameters.AddWithValue("@placas", carro.Placas);
                    comando.Parameters.AddWithValue("@marca_modelo", carro.MarcaModel);
                    comando.Parameters.AddWithValue("@ano_mod_fab", carro.AnoModeloFabricacao);
                    comando.Parameters.AddWithValue("@chassi", carro.Chassi);
                    comando.Parameters.AddWithValue("@cor", carro.Cor);
                    comando.Parameters.AddWithValue("@data_compra", carro.DataCompra);
                    comando.Parameters.AddWithValue("@valor_compra", carro.ValorCompra);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Editar(MyCar carro)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdepcvrd.tbl_my_car SET ");
                strQuery.Append("placas = @placas, marca_modelo = @marca_modelo,"
                    + "ano_mod_fab = @ano_mod_fab, chassi = @chassi, cor = @cor "
                    + ", data_compra = @data_compra, valor_compra = @valor_compra "
                    + ", id_situacao = @id_situacao ");

                if (carro.DataVenda != DateTime.MinValue)
                    strQuery.Append(",data_venda = @data_venda ");

                if (carro.ValorVenda > 0)
                    strQuery.Append(",valor_venda = @valor_venda ");

                strQuery.Append("WHERE id_my_car = @id_my_car ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_my_car", carro.Id);
                    comando.Parameters.AddWithValue("@placas", carro.Placas);
                    comando.Parameters.AddWithValue("@marca_modelo", carro.MarcaModel);
                    comando.Parameters.AddWithValue("@ano_mod_fab", carro.AnoModeloFabricacao);
                    comando.Parameters.AddWithValue("@chassi", carro.Chassi);
                    comando.Parameters.AddWithValue("@cor", carro.Cor);
                    comando.Parameters.AddWithValue("@id_situacao", carro.IdStatus);
                    comando.Parameters.AddWithValue("@data_compra", carro.DataCompra);
                    comando.Parameters.AddWithValue("@valor_compra", carro.ValorCompra);

                    if (carro.DataVenda != DateTime.MinValue)
                        comando.Parameters.AddWithValue("@data_venda", carro.DataVenda);

                    if (carro.ValorVenda > 0)
                        comando.Parameters.AddWithValue("@valor_venda", carro.ValorVenda);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static MyCar Pesquisar(int id)
        {
            MyCar carro = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT id_my_car, id_situacao, placas, marca_modelo, ano_mod_fab, chassi, cor, data_compra, valor_compra, data_venda, valor_venda  ");
                strQuery.Append("FROM bdepcvrd.tbl_my_car ");
                strQuery.Append("WHERE id_my_car = @id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    carro = new MyCar(reader);
            }
            return carro;
        }


        public static List<MyCar> Listar()
        {
            var lista = new List<MyCar>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT id_my_car, id_situacao, placas, marca_modelo, ano_mod_fab, chassi, cor, data_compra, valor_compra, data_venda, valor_venda  ");
                strQuery.Append("FROM bdepcvrd.tbl_my_car ");
                strQuery.Append("ORDER BY id_situacao, id_my_car desc; ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new MyCar(reader));
            }
            return lista;
        }


    }
}