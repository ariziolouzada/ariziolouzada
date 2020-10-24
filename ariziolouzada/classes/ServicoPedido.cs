using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace ariziolouzada.classes
{
    public class ServicoPedido
    {



        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public int Qtde { get; set; }
        public int IdServico { get; set; }
        public int IdTipo { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

        static ServicoPedido()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEpcVrd"].ConnectionString;
        }

        // Metodos 
        public ServicoPedido() { }

        private ServicoPedido(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            Qtde = Convert.ToInt32(leitor["qtde"]);
            IdServico = Convert.ToInt32(leitor["id_servico"]);
            IdTipo = Convert.ToInt32(leitor["id_tipo"]);
            ValorUnitario = Convert.ToDecimal(leitor["valor_unitario"]);
            ValorTotal = Convert.ToDecimal(leitor["valor_total"]);
            Data = Convert.ToDateTime(leitor["data_servico"]);
            Descricao = leitor["descricao"].ToString();
        }


        public static bool Inserir(ServicoPedido serv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdepcvrd.tbl_ast_servico_pedido ");
                strQuery.Append("(`data`, id_servico, qtde, valor_unitario, valor_total) ");
                strQuery.Append("VALUES (@data, @id_servico, @qtde, @valor_unitario, @valor_total) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id_servico", serv.IdServico);
                    comando.Parameters.AddWithValue("@data", serv.Data);
                    comando.Parameters.AddWithValue("@qtde", serv.Qtde);
                    comando.Parameters.AddWithValue("@valor_unitario", serv.ValorUnitario);
                    comando.Parameters.AddWithValue("@valor_total", serv.ValorTotal);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(ServicoPedido serv)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE bdepcvrd.tbl_ast_servico_pedido SET ");
                strQuery.Append("data = @data , id_servico = @id_servico , qtde = @qtde ");
                strQuery.Append(", valor_total = @valor_total , valor_unitario = @valor_unitario  WHERE id = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", serv.Id);
                    comando.Parameters.AddWithValue("@id_servico", serv.IdServico);
                    comando.Parameters.AddWithValue("@data", serv.Data);
                    comando.Parameters.AddWithValue("@qtde", serv.Qtde);
                    comando.Parameters.AddWithValue("@valor_unitario", serv.ValorUnitario);
                    comando.Parameters.AddWithValue("@valor_total", serv.ValorTotal);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert > 0;//Convert.ToInt64(insert);
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
                strQuery.Append("DELETE FROM bdepcvrd.tbl_ast_servico_pedido ");
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


        public static List<ServicoPedido> ListaDeServicoPedidos(string dataInicial, string dataFinal)
        {
            var lista = new List<ServicoPedido>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdepcvrd.vw_servico_pedido ");

                if (dataInicial != string.Empty && dataFinal != string.Empty)
                {
                    var dtI = DateTime.Parse(dataInicial);
                    var dtF = DateTime.Parse(dataFinal);
                    strQuery.Append(string.Format("WHERE (data_servico BETWEEN '{0}' AND '{1}') ", dtI.ToString("yyyy-MM-dd"), dtF.ToString("yyyy-MM-dd")));
                }

                strQuery.Append(" order by data_servico DESC ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new ServicoPedido(reader));
            }
            return lista;
        }


        public static ServicoPedido PesquisaServicoPedido(int id)
        {
            ServicoPedido svc = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdepcvrd.vw_servico_pedido ");
                strQuery.Append("WHERE id = @id ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    svc = new ServicoPedido(reader);
            }
            return svc;
        }

    }
}
