using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace sbessencial_cl
{
    public class Agenda
    {
        //Propriedades
        private static readonly string StringConnection;
        public long IdRegistroAgenda { get; set; }
        public int IdCliente { get; set; }
        public int IdStatus { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
        public int IdProfissional { get; set; }

        //Vw
        public string Cliente { get; set; }
        public string Status { get; set; }
        public string NomeProfissional { get; set; }

        static Agenda()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }


        public Agenda() { }


        public Agenda(MySqlDataReader leitor)
        {
            IdRegistroAgenda = Convert.ToInt64(leitor["idRegistroAgenda"]);
            IdCliente = Convert.ToInt32(leitor["idCliente"]);
            IdStatus = Convert.ToInt32(leitor["idStatus"]);
            IdProfissional = Convert.ToInt32(leitor["id_Profissional"]);
            DataHora = Convert.ToDateTime(leitor["dataHora"]);
            Observacao = leitor["observacao"].ToString();

            Cliente = leitor["nomeCleinte"].ToString();
            Status = leitor["descStatus"].ToString();
            NomeProfissional = leitor["nomeProfissional"].ToString();
        }


        public static bool Inserir(Agenda agd)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_agenda ");
                strQuery.Append("(idRegistroAgenda, dataHora, idCliente, idStatus, observacao, id_profissional) ");
                strQuery.Append("VALUES (@idRegistroAgenda, @dataHora, @idCliente, @idStatus, @observacao, @idProfissional ) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD                   
                    comando.Parameters.AddWithValue("@idRegistroAgenda", agd.IdRegistroAgenda);
                    comando.Parameters.AddWithValue("@idCliente", agd.IdCliente);
                    comando.Parameters.AddWithValue("@idProfissional", agd.IdProfissional);
                    comando.Parameters.AddWithValue("@idStatus", agd.IdStatus);
                    comando.Parameters.AddWithValue("@dataHora", agd.DataHora);
                    comando.Parameters.AddWithValue("@observacao", agd.Observacao);
                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Excluir(long idRegistroAgenda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_agenda ");
                strQuery.Append("WHERE idRegistroAgenda = @idRegistroAgenda ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idRegistroAgenda", idRegistroAgenda);

                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Excluir(DateTime data, int hora, int idProfissional)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_agenda ");
                strQuery.Append(string.Format("WHERE DATE(dataHora) = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND HOUR(dataHora) = @hora ");
                strQuery.Append("AND id_Profissional = @idProfissional ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@hora", hora);
                    comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool EditarStatus(long idRegistroAgenda, int idStatus)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_agenda SET ");
                strQuery.Append("idStatus = @idStatus ");
                strQuery.Append("WHERE idRegistroAgenda = @idRegistroAgenda ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idRegistroAgenda", idRegistroAgenda);
                    comando.Parameters.AddWithValue("@idStatus", idStatus);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool Editar(Agenda agd)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("UPDATE sbessencial.tbl_agenda SET ");
                strQuery.Append(" idCliente = @idCliente, dataHora = @dataHora ");
                strQuery.Append(" , idStatus = @idStatus, observacao = @observacao ");
                strQuery.Append("WHERE idRegistroAgenda = @idRegistroAgenda ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idRegistroAgenda", agd.IdRegistroAgenda);
                    comando.Parameters.AddWithValue("@idCliente", agd.IdCliente);
                    comando.Parameters.AddWithValue("@idStatus", agd.IdStatus);
                    comando.Parameters.AddWithValue("@observacao", agd.Observacao);
                    comando.Parameters.AddWithValue("@dataHora", agd.DataHora);

                    var editou = comando.ExecuteNonQuery();
                    resultado = editou == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static Agenda Pesquisar(long idRegistroAgenda)
        {
            Agenda agd = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_agenda ");
                strQuery.Append("WHERE idRegistroAgenda = @id");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@id", idRegistroAgenda);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    agd = new Agenda(reader);
            }
            return agd;
        }

        public static Agenda Pesquisa(DateTime data, int hora, int idProfissional)
        {
            Agenda agd = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_agenda ");
                strQuery.Append(string.Format("WHERE DATE(dataHora) = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND HOUR(dataHora) = @hora ");
                strQuery.Append("AND id_Profissional = @idProfissional ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@hora", hora);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                    agd = new Agenda(reader);
            }
            return agd;
        }


        public static bool PesquisaDataHora(DateTime data, int hora, int idProfissional)
        {
            var resultado = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.tbl_agenda ");
                strQuery.Append(string.Format("WHERE DATE(dataHora) = '{0}' ", data.ToString("yyyy-MM-dd")));
                strQuery.Append("AND HOUR(dataHora) = @hora ");
                strQuery.Append("AND id_Profissional = @idProfissional ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@hora", hora);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                var reader = comando.ExecuteReader();
                resultado = reader.Read();
            }
            return resultado;
        }


        public static List<Agenda> ListaDiaria(string diaHora)
        {
            var data = Convert.ToDateTime(diaHora);
            var lista = new List<Agenda>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_agenda ");
                strQuery.Append("WHERE DAY(dataHora) = @dia ");
                strQuery.Append("AND MONTH(dataHora) = @mes ");
                strQuery.Append("AND YEAR(dataHora) = @ano ");
                strQuery.Append("ORDER BY dataHora ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@dia", data.Day);
                comando.Parameters.AddWithValue("@mes", data.Month);
                comando.Parameters.AddWithValue("@ano", data.Year);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Agenda(reader));
            }
            return lista;
        }

        public static List<Agenda> ListaDiaria(string diaHora, int idProfisional)
        {
            var data = Convert.ToDateTime(diaHora);
            var lista = new List<Agenda>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM sbessencial.vw_agenda ");
                strQuery.Append("WHERE DAY(dataHora) = @dia ");
                strQuery.Append("AND MONTH(dataHora) = @mes ");
                strQuery.Append("AND YEAR(dataHora) = @ano ");

                if (idProfisional > 0)
                    strQuery.Append("AND id_profissional = @idProfissional  ");

                strQuery.Append("ORDER BY dataHora ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@dia", data.Day);
                comando.Parameters.AddWithValue("@mes", data.Month);
                comando.Parameters.AddWithValue("@ano", data.Year);

                if (idProfisional > 0)
                    comando.Parameters.AddWithValue("@idProfissional", idProfisional);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new Agenda(reader));
            }
            return lista;
        }


        //public static bool LimpaTabelaRegistrosInvalido()
        //{
        //    bool resultado;
        //    using (var conexao = new MySqlConnection(StringConnection))
        //    {
        //        var strQuery = new StringBuilder();
        //        strQuery.Append("DELETE FROM sbessencial.tbl_agenda_servicos ");
        //        strQuery.Append("WHERE idRegistroAgenda NOT IN ");
        //        strQuery.Append("(SELECT idRegistroAgenda FROM sbessencial.tbl_agenda) ");

        //        using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
        //        {
        //            conexao.Open();//Abrir a conexão com o BD
        //            //comando.Parameters.AddWithValue("@idFluxoCaixa", idFluxoCaixa);

        //            var excluiu = comando.ExecuteNonQuery();
        //            resultado = excluiu > 0;//Convert.ToInt64(insert);
        //        }
        //    }
        //    return resultado;
        //}


        public static DateTime DataHoraAgenda(string data, string hora, string minuto)
        {
            var dataStr = string.Format("{0} {1}:{2}:00", data, hora, minuto);
            return Convert.ToDateTime(dataStr);
        }

    }
}
