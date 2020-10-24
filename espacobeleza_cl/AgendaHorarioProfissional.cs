using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace espacobeleza_cl
{
    public class AgendaHorarioProfissional
    {

        //Propriedades
        private static readonly string StringConnection;

        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int IdAgenda { get; set; }
        public int IdHorario { get; set; }
        public int IdProfissional { get; set; }
        public int IdServico { get; set; }

        public string Cliente { get; set; }
        public long IdComanda { get; set; }


        static AgendaHorarioProfissional()
        {
            StringConnection = ConfigurationManager.ConnectionStrings["ConectionStringBdEspacoDeBeleza"].ConnectionString;
        }

        public AgendaHorarioProfissional(string dataAgenda, string idHora, string idProf, string idAgenda, string idSvc)
        {
            Data = Convert.ToDateTime(dataAgenda);
            //IdAgenda = Convert.ToInt32();
            IdHorario = Convert.ToInt32(idHora);
            IdAgenda = Convert.ToInt32(idAgenda);
            IdProfissional = Convert.ToInt32(idProf);
            IdServico = Convert.ToInt32(idSvc);
        }

        public AgendaHorarioProfissional(MySqlDataReader leitor)
        {
            Id = Convert.ToInt32(leitor["id"]);
            Data = Convert.ToDateTime(leitor["data"]);
            IdAgenda = Convert.ToInt32(leitor["idAgenda"]);
            IdHorario = Convert.ToInt32(leitor["idHorario"]);
            IdProfissional = Convert.ToInt32(leitor["idProfissional"]);
            IdServico = Convert.ToInt32(leitor["idServico"]);

            Cliente = leitor["Nome"].ToString();

            IdComanda = leitor["idComanda"] != DBNull.Value ? Convert.ToInt64(leitor["idComanda"]) : 0;
        }


        public static bool Inserir(AgendaHorarioProfissional agenda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("INSERT INTO bdespacobeleza.tbl_agenda_horario_profisonal ");
                strQuery.Append("(data ,idHorario, idAgenda, idProfissional, idServico) ");
                strQuery.Append("VALUES (@data, @idHorario, @idAgenda, @idProfissional, @idServico) ");

                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@data", agenda.Data);
                    comando.Parameters.AddWithValue("@idHorario", agenda.IdHorario);
                    comando.Parameters.AddWithValue("@idAgenda", agenda.IdAgenda);
                    comando.Parameters.AddWithValue("@idProfissional", agenda.IdProfissional);
                    comando.Parameters.AddWithValue("@idServico", agenda.IdServico);

                    var insert = comando.ExecuteNonQuery();
                    resultado = insert == 1;//Convert.ToInt64(insert);
                }
            }
            return resultado;
        }


        public static bool Apagar(int idAgenda)
        {
            bool resultado;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("DELETE FROM bdespacobeleza.tbl_agenda_horario_profisonal ");
                strQuery.Append("WHERE idAgenda = @idAgenda ");
                using (var comando = new MySqlCommand(strQuery.ToString(), conexao))
                {
                    conexao.Open();//Abrir a conexão com o BD
                    comando.Parameters.AddWithValue("@idAgenda", idAgenda);
                    var excluiu = comando.ExecuteNonQuery();
                    resultado = excluiu > 0;
                }
            }
            return resultado;
        }

        public static bool ExisteHoraAgenda(string data, int idHora, int idProfissional)
        {
            var retorno = false;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                strQuery.Append("SELECT * ");
                strQuery.Append("FROM bdespacobeleza.tbl_agenda_horario_profisonal ");
                strQuery.Append("WHERE idProfissional = @idProfissional AND idHorario = @idHora ");
                strQuery.Append("AND data = @data  ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                comando.Parameters.AddWithValue("@data", Convert.ToDateTime(data));
                comando.Parameters.AddWithValue("@idHorario", idHora);

                var reader = comando.ExecuteReader();
                retorno = reader.Read();
            }
            return retorno;
        }

        public static AgendaHorarioProfissional Pesquisar(string data, int idHora, int idProfissional)
        {
            AgendaHorarioProfissional retorno = null;
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                //strQuery.Append("SELECT * ");
                //strQuery.Append("FROM bdespacobeleza.tbl_agenda_horario_profisonal ");

                strQuery.Append("SELECT ahp.*, clt.Nome, clt.idCliente, ag.idComanda ");
                strQuery.Append("FROM bdespacobeleza.tbl_agenda_horario_profisonal AS ahp ");
                strQuery.Append("JOIN bdespacobeleza.tbl_agenda AS ag ON ahp.idAgenda = ag.idAgenda ");
                strQuery.Append("JOIN bdespacobeleza.tbl_cliente AS clt ON ag.idCliente = clt.idCliente ");

                strQuery.Append("WHERE ahp.idProfissional = @idProfissional AND ahp.idHorario = @idHora ");
                strQuery.Append("AND ahp.data = @data  ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idProfissional", idProfissional);
                comando.Parameters.AddWithValue("@idHora", idHora);
                comando.Parameters.AddWithValue("@data", Convert.ToDateTime(data));

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    retorno = new AgendaHorarioProfissional(reader);
            }
            return retorno;
        }


        public static List<AgendaHorarioProfissional> Lista(int idAgenda)
        {
            var lista = new List<AgendaHorarioProfissional>();
            using (var conexao = new MySqlConnection(StringConnection))
            {
                var strQuery = new StringBuilder();
                //strQuery.Append("SELECT * ");
                //strQuery.Append("FROM bdespacobeleza.tbl_agenda_horario_profisonal ");

                strQuery.Append("SELECT ahp.*, clt.Nome, clt.idCliente, ag.idComanda ");
                strQuery.Append("FROM bdespacobeleza.tbl_agenda_horario_profisonal AS ahp ");
                strQuery.Append("JOIN bdespacobeleza.tbl_agenda AS ag ON ahp.idAgenda = ag.idAgenda ");
                strQuery.Append("JOIN bdespacobeleza.tbl_cliente AS clt ON ag.idCliente = clt.idCliente ");
                               
                strQuery.Append("WHERE ahp.idAgenda = @idAgenda ");

                conexao.Open();//Abrir a conexão com o BD
                var comando = new MySqlCommand(strQuery.ToString(), conexao);
                comando.Parameters.AddWithValue("@idAgenda", idAgenda);

                var reader = comando.ExecuteReader();
                while (reader.Read())
                    lista.Add(new AgendaHorarioProfissional(reader));
            }
            return lista;
        }


    }
}
