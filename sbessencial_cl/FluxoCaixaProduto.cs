using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbessencial_cl
{
    public class FluxoCaixaProduto
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int Item { get; set; }
        public int Qtde { get; set; }
        public long IdFluxoCaixa { get; set; }
        public decimal Valor { get; set; }
        public decimal Total { get; set; }

        public string DescricaoProduto { get; set; }

        static FluxoCaixaProduto()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        public FluxoCaixaProduto(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idProdutoCaixa"]);
            IdProduto = Convert.ToInt32(leitor["idProduto"]);
            Item = Convert.ToInt32(leitor["item"]);
            Qtde = Convert.ToInt32(leitor["qtde"]);
            IdFluxoCaixa = Convert.ToInt64(leitor["idFluxoCaixa"]);
            DescricaoProduto = leitor["descricao"].ToString();
            Valor = Convert.ToDecimal(leitor["valor"]);
            Total = Convert.ToDecimal(leitor["total"]);
        }


        public FluxoCaixaProduto(long idFluxoCaixa, int idProduto, decimal valor, int qtde)
        {
            IdFluxoCaixa = idFluxoCaixa;
            IdProduto = idProduto;
            Valor = valor;
            Qtde = qtde;
        }


        public static List<FluxoCaixaProduto> Lista(long idFluxoCx)
        {
            var lista = new List<FluxoCaixaProduto>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_financeiro_caixa_produtos ");
                strQuery.Append("WHERE idFluxoCaixa = @idFluxoCaixa ");
                strQuery.Append("ORDER BY item ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idFluxoCaixa", idFluxoCx);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new FluxoCaixaProduto(reader));
            }
            return lista;
        }


        public static int QtdeItensLista(long idFluxoCx)
        {
            var lista = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT count(idProdutoCaixa) AS qtde ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa_produtos ");
                strQuery.Append("WHERE idFluxoCaixa = @idFluxoCaixa ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idFluxoCaixa", idFluxoCx);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista = Convert.ToInt32(reader["qtde"]);
            }
            return lista;
        }


        public static decimal SomaValorItensLista(long idFluxoCx)
        {
            decimal soma = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT sum(total) AS total ");
                strQuery.Append("FROM sbessencial.vw_financeiro_caixa_produtos ");
                strQuery.Append("WHERE idFluxoCaixa = @idFluxoCaixa ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idFluxoCaixa", idFluxoCx);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    soma = Convert.ToDecimal(reader["total"] != DBNull.Value ? reader["total"] : 0);
            }
            return soma;
        }


        public static FluxoCaixaProduto Pesquisar(int id)
        {
            FluxoCaixaProduto usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_financeiro_caixa_produtos ");
                strQuery.Append("WHERE idFluxoCaixaProduto = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new FluxoCaixaProduto(reader);
            }
            return usuario;
        }


        public static bool Inserir(FluxoCaixaProduto fcs)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_financeiro_caixa_produtos ");
                strQuery.Append("(item, idFluxoCaixa, idProduto, valor, qtde ) ");
                strQuery.Append("VALUES (@item, @idFluxoCaixa, @idProduto, @valor, @qtde ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    var item = QtdeItensLista(fcs.IdFluxoCaixa) + 1;
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@idFluxoCaixa", fcs.IdFluxoCaixa);
                    comando.Parameters.AddWithValue("@idProduto", fcs.IdProduto);
                    comando.Parameters.AddWithValue("@valor", fcs.Valor);
                    comando.Parameters.AddWithValue("@qtde", fcs.Qtde);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Excluir(int idProdutoCaixa)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_financeiro_caixa_produtos ");
                strQuery.Append("WHERE idProdutoCaixa = @idProdutoCaixa ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idProdutoCaixa", idProdutoCaixa);

                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool LimpaTabelaRegistrosInvalido()
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_financeiro_caixa_produtos ");
                strQuery.Append("WHERE idFluxoCaixa NOT IN ");
                strQuery.Append("(SELECT idServico FROM sbessencial.tbl_financeiro_caixa) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    //comando.Parameters.AddWithValue("@idFluxoCaixa", idFluxoCaixa);

                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu > 0;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static void AtualizaItens(long idFluxoCx)
        {
            var listaItens = Lista(idFluxoCx);
            //bool resultado;
            var index = 1;
            foreach (var item in listaItens)
            {
                EditarItem(item.Id, index);
                index++;
            }
            
        }


        public static bool EditarItem(int idProdutoCaixa, int item)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_financeiro_caixa_produtos SET ");
                strQuery.Append(" item = @item ");
                strQuery.Append("WHERE idProdutoCaixa = @idProdutoCaixa ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@idProdutoCaixa", idProdutoCaixa);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


    }
}
