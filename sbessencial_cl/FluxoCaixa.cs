using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class FluxoCaixa
    {
        //Propriedades
        private static readonly string StringConnection;
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int IdFormaPgto { get; set; }
        public int IdTipo { get; set; }
        public int IdCliente { get; set; }
        public int IdStatus { get; set; }
        public int IdSaida { get; set; }
        public long IdServico { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public string LoginCadastro { get; set; }
        public string Obsercacao { get; set; }

        //Aux
        public string Cliente { get; set; }
        public string FormaPgto { get; set; }
        public string Tipo { get; set; }
        public string TipoSaida { get; set; }
        //public string Servico { get; set; }


        static FluxoCaixa()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        // Metodos 
        public FluxoCaixa() { }


        private FluxoCaixa(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idItemCaixa"]);
            IdFormaPgto = Convert.ToInt32(leitor["idFormaPgto"]);
            IdTipo = Convert.ToInt32(leitor["idTipo"]);
            IdCliente = Convert.ToInt32(leitor["idCliente"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            IdSaida = Convert.ToInt32(leitor["idSaida"]);
            IdServico = leitor["idServico"] != DBNull.Value ? Convert.ToInt64(leitor["idServico"]) : 0;
            Data = Convert.ToDateTime(leitor["data"]);
            DataCadastro = Convert.ToDateTime(leitor["dataCadastro"]);
            Valor = Convert.ToDecimal(leitor["valor"]);
            LoginCadastro = leitor["loginCadastro"].ToString();
            Obsercacao = leitor["observacao"].ToString();

            //vw_fluxo_caixa
            Cliente = leitor["nome"].ToString();
            Tipo = leitor["descTipo"].ToString();
            FormaPgto = leitor["descFormaPgto"].ToString();
            TipoSaida = leitor["descSaida"].ToString();
            //Servico = leitor["descServico"].ToString();
        }


        public static bool Inserir(FluxoCaixa fc)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_financeiro_caixa ");
                strQuery.Append("(data, dataCadastro, loginCadastro, idFormaPgto, idTipo, idCliente, observacao, idStatus, valor, idServico, idSaida ) ");
                strQuery.Append("VALUES (@data, @dataCadastro, @loginCadastro, @idFormaPgto, @idTipo, @idCliente, @observacao, 1, @valor, @idServico, @idSaida ) ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@data", fc.Data);
                    comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                    comando.Parameters.AddWithValue("@loginCadastro", fc.LoginCadastro);
                    comando.Parameters.AddWithValue("@idFormaPgto", fc.IdFormaPgto);
                    comando.Parameters.AddWithValue("@idTipo", fc.IdTipo);
                    comando.Parameters.AddWithValue("@idCliente", fc.IdCliente);
                    comando.Parameters.AddWithValue("@observacao", fc.Obsercacao);
                    comando.Parameters.AddWithValue("@valor", fc.Valor);
                    comando.Parameters.AddWithValue("@idServico", fc.IdServico);
                    comando.Parameters.AddWithValue("@idSaida", fc.IdSaida);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Editar(FluxoCaixa fc)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE  sbessencial.tbl_financeiro_caixa SET ");
                strQuery.Append("data = @data, idFormaPgto = @idFormaPgto, ");
                strQuery.Append("idTipo = @idTipo, idCliente = @idCliente ,");
                strQuery.Append("observacao = @observacao, idStatus = @idStatus, ");
                strQuery.Append("valor = @valor, idServico = @idServico, idSaida = @idSaida ");
                strQuery.Append("WHERE idItemCaixa = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@data", fc.Data);
                    //comando.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                    comando.Parameters.AddWithValue("@valor", fc.Valor);
                    comando.Parameters.AddWithValue("@idFormaPgto", fc.IdFormaPgto);
                    comando.Parameters.AddWithValue("@idTipo", fc.IdTipo);
                    comando.Parameters.AddWithValue("@idCliente", fc.IdCliente);
                    comando.Parameters.AddWithValue("@observacao", fc.Obsercacao);
                    comando.Parameters.AddWithValue("@idStatus", fc.IdStatus);
                    comando.Parameters.AddWithValue("@id", fc.Id);
                    comando.Parameters.AddWithValue("@idServico", fc.IdServico);
                    comando.Parameters.AddWithValue("@idSaida", fc.IdSaida);

                    var update = comando.ExecuteNonQuery();
                    resultado = update == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarValor(long idServico, decimal valor)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE  sbessencial.tbl_financeiro_caixa SET ");
                strQuery.Append("valor = @valor ");
                strQuery.Append("WHERE idServico = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@valor", valor);
                    comando.Parameters.AddWithValue("@id", idServico);

                    var update = comando.ExecuteNonQuery();
                    resultado = update == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool AtualizaValor(long idServico)
        {
            var valorAtualizadoSrv = FluxoCaixaServico.SomaValorItensLista(idServico);
            var valorAtualizadoProd = FluxoCaixaProduto.SomaValorItensLista(idServico);
            return EditarValor(idServico, valorAtualizadoSrv + valorAtualizadoProd);
        }

        public static string TotalCaixa(long idServico)
        {
            var valorAtualizadoSrv = FluxoCaixaServico.SomaValorItensLista(idServico);
            var valorAtualizadoProd = FluxoCaixaProduto.SomaValorItensLista(idServico);
            var total =   (valorAtualizadoSrv + valorAtualizadoProd);
            return string.Format("{0:C}", total);
        }


        public static bool Apagar(int id)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM  sbessencial.tbl_financeiro_caixa ");
                strQuery.Append("WHERE idItemCaixa = @id ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@id", id);

                    var deletou = comando.ExecuteNonQuery();
                    resultado = deletou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static FluxoCaixa Pesquisar(int id)
        {
            FluxoCaixa usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_fluxo_caixa ");
                strQuery.Append("WHERE idItemCaixa = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new FluxoCaixa(reader);
            }
            return usuario;
        }

        public static bool Existe(string idServico)
        {
           bool resultado = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa ");
                strQuery.Append("WHERE idServico = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", idServico);
                var reader = comando.ExecuteReader();
                resultado = reader.Read();
            }
            return resultado;
        }


        public static List<FluxoCaixa> Lista(int mes, int ano, int idTipo)
        {
            var lista = new List<FluxoCaixa>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_fluxo_caixa ");
                strQuery.Append("WHERE MONTH(data) = @mes ");
                strQuery.Append("AND YEAR(data) = @ano ");

                if (idTipo > 0)
                    strQuery.Append("AND idTipo = @idTipo ");

                strQuery.Append("ORDER BY data DESC ");


                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@mes", mes);
                comando.Parameters.AddWithValue("@ano", ano);

                if (idTipo > 0)
                    comando.Parameters.AddWithValue("@idTipo", idTipo);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new FluxoCaixa(reader));
            }
            return lista;
        }

        public static List<FluxoCaixa> ListaDiaria(string dia, int idTipo)
        {
            var data = Convert.ToDateTime(dia);
            var lista = new List<FluxoCaixa>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_fluxo_caixa ");
                strQuery.Append("WHERE  `data` = '" + data.ToString("yyyy-MM-dd") + "' ");

                if (idTipo > 0)
                    strQuery.Append("AND idTipo = @idTipo ");

                strQuery.Append("ORDER BY idTipo ");


                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idTipo > 0)
                    comando.Parameters.AddWithValue("@idTipo", idTipo);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new FluxoCaixa(reader));
            }
            return lista;
        }


        public static List<string> ListaAno(bool comSelecione)
        {
            var lista = new List<string>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT DISTINCT YEAR(data) as ANO ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa ");
                strQuery.Append("ORDER BY ANO  ");

                if (comSelecione)
                    lista.Add("Selecione...");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(reader["ANO"].ToString());
                }

            }
            return lista;
        }


        public static decimal Total(int mes, int ano, int idTipo)
        {
            decimal total = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT  SUM(valor) AS total ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa ");
                strQuery.Append("WHERE MONTH(data) = @mes ");
                strQuery.Append("AND YEAR(data) = @ano ");

                if (idTipo > 0)
                    strQuery.Append("AND idTipo = @idTipo ");
                
                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@mes", mes);
                comando.Parameters.AddWithValue("@ano", ano);

                if (idTipo > 0)
                    comando.Parameters.AddWithValue("@idTipo", idTipo);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    total = Convert.ToDecimal(reader["total"] != DBNull.Value ? reader["total"] : 0);
            }
            return total;
        }

        public static decimal TotalDiario(string dia, int idTipo)
        {
            decimal total = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT  SUM(valor) AS total ");
                strQuery.Append("FROM sbessencial.tbl_financeiro_caixa ");
                strQuery.Append("WHERE  `data` = '" + dia + "' ");

                if (idTipo > 0)
                    strQuery.Append("AND idTipo = @idTipo ");
                
                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                if (idTipo > 0)
                    comando.Parameters.AddWithValue("@idTipo", idTipo);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    total = Convert.ToDecimal(reader["total"] != DBNull.Value ? reader["total"] : 0);
            }
            return total;
        }


        public static string GerarID()
        {
            var mil = DateTime.Now.Millisecond;
            var seg = DateTime.Now.Second;
            var min = DateTime.Now.Minute;
            var hor = DateTime.Now.Hour;
            var dia = DateTime.Now.Day;
            var mes = DateTime.Now.Month;
            var ano = DateTime.Now.Year;

            var id = string.Format("{0}{1}{2}{3}{4}{5}{6}", ano
                                        , string.Format("{0:00}", mes)
                                        , string.Format("{0:00}", dia)
                                        , string.Format("{0:00}", hor)
                                        , string.Format("{0:00}", min)
                                        , string.Format("{0:00}", seg)
                                        , string.Format("{0:000}", mil)
                                    );

            return id;
        }

    }
}
