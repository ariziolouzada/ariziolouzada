using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace ariziolouzada.classes
{
    public class MyCarManutencao
    {

        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public int IdMyCar { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public int Km { get; set; }
        public decimal Valor { get; set; }
        public string MarcaModel { get; set; }

        static MyCarManutencao()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        public MyCarManutencao() { }

        private MyCarManutencao(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id_my_car_manutencao"]);
            IdMyCar = Convert.ToInt32(leitor["id_my_car"]);
            Data = Convert.ToDateTime(leitor["data"]);
            Descricao = leitor["descricao"].ToString();
            MarcaModel = leitor["marca_modelo"].ToString();
            Km = Convert.ToInt32(leitor["km"]);
            Valor = Convert.ToDecimal(leitor["valor"]);

        }
        public MyCarManutencao(int idMayCar, DateTime data, string descricao, int km, decimal valor)
        {
            IdMyCar = idMayCar;
            Data = data;
            Descricao = descricao;
            Km = km;
            Valor = valor;
        }
        //SELECT id_my_car_manutencao, `data`, descricao, KM, valor
        //FROM  bdepcvrd.tbl_my_car_manutencao;


        public static bool Inserir(MyCarManutencao item)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_my_car_manutencao ");
                strQuery.Append("(id_my_car, `data`, descricao, KM, valor) ");
                strQuery.Append("VALUES (@id_my_car, @data, @descricao, @KM, @valor) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_my_car", item.IdMyCar);
                    comando.Parameters.AddWithValue("@data", item.Data);
                    comando.Parameters.AddWithValue("@descricao", item.Descricao);
                    comando.Parameters.AddWithValue("@KM", item.Km);
                    comando.Parameters.AddWithValue("@valor", item.Valor);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Editar(MyCarManutencao item)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdepcvrd.tbl_my_car_manutencao SET ");
                strQuery.Append("data = @data, descricao = @descricao, KM = @KM, valor = @valor ");
                strQuery.Append("WHERE id_my_car_manutencao = @id_my_car_manutencao ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_my_car_manutencao", item.Id);
                    //comando.Parameters.AddWithValue("@id_my_car", item.IdMyCar);
                    comando.Parameters.AddWithValue("@data", item.Data);
                    comando.Parameters.AddWithValue("@descricao", item.Descricao);
                    comando.Parameters.AddWithValue("@KM", item.Km);
                    comando.Parameters.AddWithValue("@valor", item.Valor);


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
                strQuery.Append("DELETE FROM bdepcvrd.tbl_my_car_manutencao ");
                strQuery.Append("WHERE id_my_car_manutencao = @id ");

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

        public static MyCarManutencao Pesquisar(int id)
        {
            MyCarManutencao item = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT id_my_car_manutencao, id_my_car, `data`, descricao, KM, valor, marca_modelo ");
                strQuery.Append("FROM bdepcvrd.vw_my_car_manutencao ");
                strQuery.Append("WHERE id_my_car_manutencao = @id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    item = new MyCarManutencao(reader);
            }
            return item;
        }


        public static List<MyCarManutencao> Listar(int idMyCar)
        {
            var lista = new List<MyCarManutencao>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT id_my_car_manutencao, id_my_car, `data`, descricao, KM, valor, marca_modelo ");
                strQuery.Append("FROM bdepcvrd.vw_my_car_manutencao ");
                strQuery.Append("WHERE id_my_car = @idMyCar ");
                strQuery.Append("ORDER BY data desc; ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idMyCar", idMyCar);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new MyCarManutencao(reader));
            }
            return lista;
        }


        public static decimal SomaValor(int idMyCar)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT SUM(valor) AS soma_valor ");
                strQuery.Append("FROM bdepcvrd.tbl_my_car_manutencao ");
                strQuery.Append("WHERE id_my_car = @id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", idMyCar);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    soma = Convert.ToDecimal(reader["soma_valor"]);
                }
            }
            return soma;
        }




    }
}