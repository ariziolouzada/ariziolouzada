using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class AgendaServicos
    {
        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public long IdRegistroAgenda { get; set; }
        public int IdServico { get; set; }
        public int Item { get; set; }
        public decimal Valor { get; set; }

        public string DescricaoServico { get; set; }


        static AgendaServicos()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }


        public AgendaServicos(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["idAgendaServico"]);
            IdServico = Convert.ToInt32(leitor["idServico"]);
            Item = Convert.ToInt32(leitor["item"]);
            IdRegistroAgenda = Convert.ToInt64(leitor["idRegistroAgenda"]);
            DescricaoServico = leitor["descricao"].ToString();
            Valor = Convert.ToDecimal(leitor["valor"]);
        }


        public AgendaServicos(long idRegAgenda, int idServico, decimal valor)
        {
            IdRegistroAgenda = idRegAgenda;
            IdServico = idServico;
            Valor = valor;
        }


        public static List<AgendaServicos> Lista(long idRegistroAgenda)
        {
            var lista = new List<AgendaServicos>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_agenda_servico ");
                strQuery.Append("WHERE idRegistroAgenda = @idRegistroAgenda ");
                strQuery.Append("ORDER BY item ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idRegistroAgenda", idRegistroAgenda);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AgendaServicos(reader));
            }
            return lista;
        }


        public static int QtdeItensLista(long idFluxoCx)
        {
            var lista = 0;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT count(idAgendaServico) AS qtde ");
                strQuery.Append("FROM sbessencial.tbl_agenda_servicos ");
                strQuery.Append("WHERE idRegistroAgenda = @idRegistroAgenda ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idRegistroAgenda", idFluxoCx);

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
                strQuery.Append("FROM sbessencial.tbl_agenda_servicos ");
                strQuery.Append("WHERE idRegistroAgenda = @idRegistroAgenda ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idRegistroAgenda", idFluxoCx);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    soma = Convert.ToDecimal(reader["total"] != DBNull.Value ? reader["total"] : 0);
            }
            return soma;
        }


        public static AgendaServicos Pesquisar(int id)
        {
            AgendaServicos usuario = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_agenda_servicos ");
                strQuery.Append("WHERE idAgendaServicos = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    usuario = new AgendaServicos(reader);
            }
            return usuario;
        }


        public static bool Inserir(AgendaServicos fcs)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_agenda_servicos ");
                strQuery.Append("(item, idRegistroAgenda, idServico, valor ) ");
                strQuery.Append("VALUES (@item, @idRegistroAgenda, @idServico, @valor) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    var item = QtdeItensLista(fcs.IdRegistroAgenda) + 1;
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@idRegistroAgenda", fcs.IdRegistroAgenda);
                    comando.Parameters.AddWithValue("@idServico", fcs.IdServico);
                    comando.Parameters.AddWithValue("@valor", fcs.Valor);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Excluir(int idAgendaServico)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_agenda_servicos ");
                strQuery.Append("WHERE idAgendaServico = @idAgendaServico ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idAgendaServico", idAgendaServico);

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
                strQuery.Append("DELETE FROM sbessencial.tbl_agenda_servicos ");
                strQuery.Append("WHERE idRegistroAgenda NOT IN ");
                strQuery.Append("(SELECT idRegistroAgenda FROM sbessencial.tbl_agenda) ");

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


        public static void AtualizaItens(long idRegistroAgenda)
        {
            var listaItens = Lista(idRegistroAgenda);
            //bool resultado;
            var index = 1;
            foreach (var item in listaItens)
            {
                EditarItem(item.Id, index);
                index++;
            }

            // return resultado;
        }


        public static bool EditarItem(int idAgendaServico, int item)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_agenda_servicos SET ");
                strQuery.Append(" item = @item ");
                strQuery.Append("WHERE idAgendaServico = @idAgendaServico ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@item", item);
                    comando.Parameters.AddWithValue("@idAgendaServico", idAgendaServico);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }





    }
}
