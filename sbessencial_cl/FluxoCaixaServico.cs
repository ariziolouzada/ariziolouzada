using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class FluxoCaixaServico
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public int IdServico { get; set; }
        public int Item { get; set; }
        public long IdFluxoCaixa { get; set; }
        public decimal Valor { get; set; }

        public string DescricaoServico { get; set; }


        static FluxoCaixaServico()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }


        public FluxoCaixaServico(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idServicoCaixa"]);
            IdServico = Convert.ToInt32(leitor["idServico"]);
            Item = Convert.ToInt32(leitor["item"]);
            IdFluxoCaixa = Convert.ToInt64(leitor["idFluxoCaixa"]);
            DescricaoServico = leitor["descricao"].ToString();
            Valor = Convert.ToDecimal(leitor["valor"]);
        }


        public FluxoCaixaServico(long idFluxoCaixa, int idServico, decimal valor)
        {
            IdFluxoCaixa = idFluxoCaixa;
            IdServico = idServico;
            Valor = valor;
        }


        public static List<FluxoCaixaServico> Lista(long idFluxoCx)
        {
            var lista = new List<FluxoCaixaServico>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_financeiro_caixa_servicos ");
                strQuery.Append("WHERE idFluxoCaixa = @idFluxoCaixa ");
                strQuery.Append("ORDER BY item ");
                
                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idFluxoCaixa", idFluxoCx);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new FluxoCaixaServico(reader));
            }
            return lista;
        }


        public static int QtdeItensLista(long idFluxoCx)
        {
            var lista = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT count(idServicoCaixa) AS qtde ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa_servicos ");
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
                strQuery.Append("SELECT sum(valor) AS total ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa_servicos ");
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


        public static FluxoCaixaServico Pesquisar(int id)
        {
            FluxoCaixaServico usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa_servicos ");
                strQuery.Append("WHERE idFluxoCaixaServico = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new FluxoCaixaServico(reader);
            }
            return usuario;
        }

        public static bool Existe(long idFluxoCaixa, int idServico)
        {
            bool resultado = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa_servicos ");
                strQuery.Append("WHERE idFluxoCaixa = @id ");
                strQuery.Append("AND idServico = @idServico " );

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", idFluxoCaixa);
                comando.Parameters.AddWithValue("@idServico", idServico);
                var reader = comando.ExecuteReader();
                resultado = reader.Read();                    
            }
            return resultado;
        }


        public static bool Inserir(FluxoCaixaServico fcs)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_financeiro_caixa_servicos ");
                strQuery.Append("(item, idFluxoCaixa, idServico, valor ) ");
                strQuery.Append("VALUES (@item, @idFluxoCaixa, @idServico, @valor) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    var item = QtdeItensLista(fcs.IdFluxoCaixa) + 1;
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@idFluxoCaixa", fcs.IdFluxoCaixa);
                    comando.Parameters.AddWithValue("@idServico", fcs.IdServico);
                    comando.Parameters.AddWithValue("@valor", fcs.Valor);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Excluir(int idServicoCaixa)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_financeiro_caixa_servicos ");
                strQuery.Append("WHERE idServicoCaixa = @idServicoCaixa ");
                
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idServicoCaixa", idServicoCaixa);

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
                strQuery.Append("DELETE FROM sbessencial.tbl_financeiro_caixa_servicos ");
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

           // return resultado;
        }


        public static bool EditarItem(int idServicoCaixa, int item)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_financeiro_caixa_servicos SET ");
                strQuery.Append(" item = @item ");                
                strQuery.Append("WHERE idServicoCaixa = @idServicoCaixa ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@idServicoCaixa", idServicoCaixa);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


    }
}
