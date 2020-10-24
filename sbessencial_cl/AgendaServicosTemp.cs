using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbessencial_cl
{
   public class AgendaServicosTemp
    { 
        //Propriedades
        private static readonly string StringConnection;

        public int IdAgendaTemp { get; set; }
        public int IdServicoTemp { get; set; }

        public decimal Valor { get; set; }
        public string DescricaoServico { get; set; }



        static AgendaServicosTemp()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdSbEssencial"].ConnectionString;
        }

        public AgendaServicosTemp(int idServico, int idAgenda)
        {
            IdAgendaTemp = idAgenda;
            IdServicoTemp = idServico;
        }

        public AgendaServicosTemp(MySqlDataReader leitor)
        {
            IdServicoTemp = Convert.ToInt32(leitor["idServicoTemp"]);
            IdAgendaTemp = Convert.ToInt32(leitor["idAgendaTemp"]);
            DescricaoServico = leitor["descricao"].ToString();
            Valor = Convert.ToDecimal(leitor["valor"]);
        }

        public static bool Inserir(AgendaServicosTemp fcs)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO sbessencial.tbl_agenda_servicos_temp ");
                strQuery.Append("(idAgendaTemp, idServicoTemp) ");
                strQuery.Append("VALUES (@idAgenda, @idServico) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idAgenda", fcs.IdAgendaTemp);
                    comando.Parameters.AddWithValue("@idServico", fcs.IdServicoTemp);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static bool AgagarPeloIdAgenda(int idAgenda )
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM sbessencial.tbl_agenda_servicos_temp ");
                strQuery.Append("WHERE idAgendaTemp = @idAgenda ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idAgenda", idAgenda);

                    var apagou = comando.ExecuteNonQuery();
                    resultado = apagou > 0;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }

        public static List<AgendaServicosTemp> Lista(int idAgdTmp)
        {
            var lista = new List<AgendaServicosTemp>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT  ast.*, s.* ");
                strQuery.Append("FROM sbessencial.tbl_agenda_servicos_temp AS ast ");
                strQuery.Append("JOIN sbessencial.tbl_servico AS s ON ast.idServicoTemp = s.idServico ");
                strQuery.Append("WHERE ast.idAgendaTemp = @idAgdTmp ");
                strQuery.Append("ORDER BY descricao ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);

                comando.Parameters.AddWithValue("@idAgdTmp", idAgdTmp);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AgendaServicosTemp(reader));
            }
            return lista;
        }



    }
}
